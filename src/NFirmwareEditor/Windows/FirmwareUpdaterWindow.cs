using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class FirmwareUpdaterWindow : Form
	{
		private class AsyncProcessWrapper
		{
			public AsyncProcessWrapper([NotNull] Action<BackgroundWorker> processor)
			{
				if (processor == null) throw new ArgumentNullException("processor");
				Processor = processor;
			}

			[NotNull]
			public Action<BackgroundWorker> Processor { get; private set; }
		}

		private readonly FirmwareUpdater m_updater = new FirmwareUpdater();
		private readonly Firmware m_firmware;
		private readonly FirmwareLoader m_loader;
		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };

		private string m_connectedDeviceProductId;
		private string m_deviceName;
		private string m_hardwareVersion;
		private string m_firmwareVersion;

		public FirmwareUpdaterWindow(Firmware firmware, FirmwareLoader loader)
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
			InitializeControls();

			m_firmware = firmware;
			m_loader = loader;
			m_worker.DoWork += BackgroundWorker_DoWork;
			m_worker.ProgressChanged += BackgroundWorker_ProgressChanged;
		}

		private void DeviceConnected(bool isConnected)
		{
			if (isConnected)
			{
				System.Diagnostics.Trace.WriteLine("Connected " + DateTime.Now);
				Dataflash dataflash;

				try
				{
					dataflash = m_updater.ReadDataflash();
				}
				catch
				{
					return;
				}

				m_connectedDeviceProductId = dataflash.ProductId;
				m_deviceName = FirmwareUpdater.GetDeviceName(m_connectedDeviceProductId);
				m_hardwareVersion = dataflash.HardwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
				m_firmwareVersion = dataflash.FirmwareVersion.ToString("0.00", CultureInfo.InvariantCulture);

				UpdateUI(() =>
				{
					DeviceNameTextBox.Text = m_deviceName;
					HardwareVersionTextBox.Text = m_hardwareVersion;
					FirmwareVersionTextBox.Text = m_firmwareVersion;
					BootModeTextBox.Text = dataflash.LoadFromLdrom ? "LDROM" : "APROM";
					UpdateStatusLabel.Text = @"Device is ready.";
					SetUpdaterButtonsState(true);
				});
			}
			else
			{
				System.Diagnostics.Trace.WriteLine("Disconnected " + DateTime.Now);
				UpdateUI(() =>
				{
					DeviceNameTextBox.Clear();
					HardwareVersionTextBox.Clear();
					FirmwareVersionTextBox.Clear();
					BootModeTextBox.Clear();
					UpdateStatusLabel.Text = @"Waiting for device...";
					SetUpdaterButtonsState(false);
				});
				m_connectedDeviceProductId = null;
			}
		}

		private void InitializeControls()
		{
			DeviceNameTextBox.BackColor = Color.White;
			HardwareVersionTextBox.BackColor = Color.White;
			FirmwareVersionTextBox.BackColor = Color.White;
			BootModeTextBox.BackColor = Color.White;

			DeviceNameTextBox.ReadOnly = true;
			HardwareVersionTextBox.ReadOnly = true;
			FirmwareVersionTextBox.ReadOnly = true;
			BootModeTextBox.ReadOnly = true;

			m_updater.DeviceConnected += DeviceConnected;
			m_updater.StartMonitoring();
			Closing += (s, e) => m_updater.StopMonitoring();

			LogoButton.Click += LogoButton_Click;
			UpdateButton.Click += UpdateButton_Click;
			UpdateFromFileButton.Click += UpdateFromFileButton_Click;

			ResetDataflashButton.Click += ResetDataflashButton_Click;
			ReadDataflashButton.Click += ReadDataflashButton_Click;
			WriteDataflashButton.Click += WriteDataflashButton_Click;
		}

		private void UpdateFirmware(Func<byte[]> firmwareFunc)
		{
			var skipValidation = ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Alt);
			try
			{
				var firmware = firmwareFunc();
				if (!skipValidation && FirmwareLoader.FindByteArray(firmware, Encoding.UTF8.GetBytes(m_connectedDeviceProductId)) == -1)
				{
					InfoBox.Show("Opened firmware file is not suitable for the connected device.");
					return;
				}
				m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker => UpdateFirmwareAsyncWorker(worker, firmware)));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during firmware update.\n" + ex.Message);
			}
		}

		private void UpdateFirmwareAsyncWorker(BackgroundWorker worker, byte[] firmware)
		{
			var isSuccess = false;
			try
			{
				UpdateUI(() => UpdateStatusLabel.Text = @"Reading dataflash...");
				var dataflash = m_updater.ReadDataflash(worker);
				if (dataflash.LoadFromLdrom == false && dataflash.FirmwareVersion > 0)
				{
					dataflash.LoadFromLdrom = true;
					UpdateUI(() => UpdateStatusLabel.Text = @"Writing dataflash...");
					m_updater.WriteDataflash(dataflash, worker);
					m_updater.RestartDevice();
					UpdateUI(() => UpdateStatusLabel.Text = @"Waiting for device after reset...");
					Thread.Sleep(2000);
				}
				UpdateUI(() => UpdateStatusLabel.Text = @"Uploading firmware...");
				m_updater.WriteFirmware(firmware, worker);

				UpdateUI(() =>
				{
					UpdateStatusLabel.Text = string.Empty;
					worker.ReportProgress(0);
				});
				isSuccess = true;

				Thread.Sleep(500);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An exception occured during firmware update.\n" + ex.Message);
			}
			finally
			{
				if (isSuccess)
				{
					InfoBox.Show("Firmware successfully updated.");
				}
			}
		}

		private void ReadDataflashAsyncWorker(BackgroundWorker worker, string fileName)
		{
			try
			{
				UpdateUI(() => UpdateStatusLabel.Text = @"Reading dataflash...");
				var dataflash = m_updater.ReadDataflash(worker);
				File.WriteAllBytes(fileName, dataflash.Data);
				UpdateUI(() => UpdateStatusLabel.Text = @"Dataflash was successfully read and saved to the file.");
			}
			catch (Exception ex)
			{
				InfoBox.Show("An exception occured during dataflash reading.\n" + ex.Message);
			}
		}

		private void WriteDataflashAsyncWorker(BackgroundWorker worker, Dataflash dataflash)
		{
			try
			{
				UpdateUI(() => UpdateStatusLabel.Text = @"Writing dataflash...");
				m_updater.WriteDataflash(dataflash, worker);
				UpdateUI(() =>
				{
					UpdateStatusLabel.Text = @"Dataflash was successfully written.";
					worker.ReportProgress(0);
				});
			}
			catch (Exception ex)
			{
				InfoBox.Show("An exception occured during dataflash reading.\n" + ex.Message);
			}
		}

		private void SetUpdaterButtonsState(bool enabled)
		{
			LogoButton.Enabled = false;
			UpdateButton.Enabled = enabled && m_firmware != null;
			UpdateFromFileButton.Enabled = enabled;

			ResetDataflashButton.Enabled = enabled;
			ReadDataflashButton.Enabled = enabled;
			WriteDataflashButton.Enabled = enabled;
		}

		private void SetAllButtonsState(bool enabled)
		{
			SetUpdaterButtonsState(enabled);
			CancelButton.Enabled = enabled;
		}

		private string GetDataflashFileName()
		{
			return string.Format("{0} HW v{1} FW v{2}", m_deviceName, m_hardwareVersion, m_firmwareVersion);
		}

		// ReSharper disable once InconsistentNaming
		private void UpdateUI(Action action)
		{
			try
			{
				Invoke(action);
			}
			catch (Exception)
			{
				// Ignore
			}
		}

		private void LogoButton_Click(object sender, EventArgs e)
		{

		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			if (m_firmware == null) return;
			UpdateFirmware(() => m_firmware.GetBody());
		}

		private void UpdateFromFileButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var op = new OpenFileDialog { Title = @"Select encrypted or decrypted firmware file ...", Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}
			UpdateFirmware(() => m_loader.LoadFile(fileName));
		}

		private void ResetDataflashButton_Click(object sender, EventArgs e)
		{
			try
			{
				m_updater.ResetDataFlash();
				UpdateStatusLabel.Text = @"Dataflash has been reseted.";
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during dataflash reset.\n" + ex.Message);
			}
		}

		private void ReadDataflashButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var sf = new SaveFileDialog { Title = @"Select dataflash file location ...", Filter = Consts.DataFlashFilter, FileName = GetDataflashFileName() })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			try
			{
				File.WriteAllBytes(fileName, new byte[1]);
				m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker => ReadDataflashAsyncWorker(worker, fileName)));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during dataflash reading.\n" + ex.Message);
			}
		}

		private void WriteDataflashButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var op = new OpenFileDialog { Title = @"Select dataflash file ...", Filter = Consts.DataFlashFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			try
			{
				var data = File.ReadAllBytes(fileName);
				if (data.Length != 2044)
				{
					InfoBox.Show("Seems that the dataflash file has the wrong format.");
					return;
				}

				var dataflash = new Dataflash { Data = data };
				m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker => WriteDataflashAsyncWorker(worker, dataflash)));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during dataflash reading.\n" + ex.Message);
			}
		}

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			UpdateProgressBar.Value = e.ProgressPercentage;
		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = (BackgroundWorker)sender;
			var wrapper = (AsyncProcessWrapper)e.Argument;

			try
			{
				UpdateUI(() => SetAllButtonsState(false));
				m_updater.StopMonitoring();
				wrapper.Processor(worker);
			}
			finally
			{
				UpdateUI(() => SetAllButtonsState(true));
				m_updater.StartMonitoring();
			}
		}
	}
}
