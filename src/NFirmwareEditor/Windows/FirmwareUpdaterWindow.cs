using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
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

		private DeviceInfo m_deviceInfo = FirmwareUpdater.UnknownDevice;
		private string m_connectedDeviceProductId;
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
				SimpleDataflash simpleDataflash;

				try
				{
					simpleDataflash = m_updater.ReadDataflash();
				}
				catch
				{
					return;
				}

				m_connectedDeviceProductId = simpleDataflash.ProductId;
				m_deviceInfo = FirmwareUpdater.GetDeviceInfo(m_connectedDeviceProductId);
				m_hardwareVersion = simpleDataflash.HardwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
				m_firmwareVersion = simpleDataflash.FirmwareVersion.ToString("0.00", CultureInfo.InvariantCulture);

				UpdateUI(() =>
				{
					DeviceNameTextBox.Text = m_deviceInfo.Name;
					HardwareVersionTextBox.Text = m_hardwareVersion;
					FirmwareVersionTextBox.Text = m_firmwareVersion;
					BootModeTextBox.Text = simpleDataflash.LoadFromLdrom ? "LDROM" : "APROM";
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

		private void UpdateLogoAsyncWorker(BackgroundWorker worker, byte[] block1ImageBytes, byte[] block2ImageBytes)
		{
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
				UpdateUI(() => UpdateStatusLabel.Text = @"Uploading logo...");
				m_updater.WriteLogo(block1ImageBytes, block2ImageBytes, worker);

				UpdateUI(() =>
				{
					UpdateStatusLabel.Text = string.Empty;
					worker.ReportProgress(0);
				});

				Thread.Sleep(500);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An exception occured during logo update.\n" + ex.Message);
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

		private void WriteDataflashAsyncWorker(BackgroundWorker worker, SimpleDataflash simpleDataflash)
		{
			try
			{
				UpdateUI(() => UpdateStatusLabel.Text = @"Writing dataflash...");
				m_updater.WriteDataflash(simpleDataflash, worker);
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
			LogoButton.Enabled = enabled && m_deviceInfo.CanUploadLogo;
			UpdateButton.Enabled = enabled && m_firmware != null;
			UpdateFromFileButton.Enabled = enabled;

			ResetDataflashButton.Enabled = enabled;
			ReadDataflashButton.Enabled = enabled;
			WriteDataflashButton.Enabled = enabled;
			ScreenshotButton.Enabled = enabled;
		}

		private void SetAllButtonsState(bool enabled)
		{
			SetUpdaterButtonsState(enabled);
			CancelButton.Enabled = enabled;
		}

		private string GetDataflashFileName()
		{
			return string.Format("{0} HW v{1} FW v{2}", m_deviceInfo.Name, m_hardwareVersion, m_firmwareVersion);
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
			if (!m_deviceInfo.CanUploadLogo)
			{
				InfoBox.Show("Logo uploading for this device is not supported.");
				return;
			}

			Bitmap logoBitmap;
			using (var imageConverterWindow = new ImageConverterWindow(true, m_deviceInfo.LogoWidth, m_deviceInfo.LogoHeight))
			{
				if (imageConverterWindow.ShowDialog() != DialogResult.OK) return;

				logoBitmap = imageConverterWindow.GetConvertedImage();
				if (logoBitmap == null) return;
			}

			using (logoBitmap)
			{
				var imageData = BitmapProcessor.CreateRawFromBitmap(logoBitmap);

				var block1ImageMetadata = new FirmwareImage1Metadata { Width = m_deviceInfo.LogoWidth, Height = m_deviceInfo.LogoHeight };
				var block2ImageMetadata = new FirmwareImage2Metadata { Width = m_deviceInfo.LogoWidth, Height = m_deviceInfo.LogoHeight };

				var block1ImageBytes = block1ImageMetadata.Save(imageData);
				var block2ImageBytes = block2ImageMetadata.Save(imageData);

				m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker => UpdateLogoAsyncWorker(worker, block1ImageBytes, block2ImageBytes)));
			}
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
				m_updater.ResetDataflash();
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

				var dataflash = new SimpleDataflash { Data = data };
				m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker => WriteDataflashAsyncWorker(worker, dataflash)));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during dataflash reading.\n" + ex.Message);
			}
		}

		private void ScreenshotButton_Click(object sender, EventArgs e)
		{
			var data = m_updater.Screenshot();
			if (data == null || data.All(x => x == 0x00))
			{
				InfoBox.Show("Unable to get screenshot. Activate your device first.");
				return;
			}

			using (var bitmap = BitmapProcessor.CreateBitmapFromBytesArray(64, 128, data))
			{
				using (var sf = new SaveFileDialog { Filter = Consts.BitmapExportFilter })
				{
					if (sf.ShowDialog() != DialogResult.OK) return;

					bitmap.Save(sf.FileName, ImageFormat.Bmp);
				}
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
