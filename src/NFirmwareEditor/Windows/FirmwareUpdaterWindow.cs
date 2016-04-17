using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class FirmwareUpdaterWindow : Form
	{
		private readonly FirmwareUpdater m_updater = new FirmwareUpdater();

		private readonly Firmware m_firmware;
		private readonly FirmwareLoader m_loader;
		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };

		private string m_connectedDeviceProductId;

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
				var dataflash = m_updater.ReadDataFlash();
				m_connectedDeviceProductId = dataflash.ProductId;
				var deviceName = FirmwareUpdater.GetDeviceName(m_connectedDeviceProductId);

				UpdateUI(() =>
				{
					DeviceNameTextBox.Text = deviceName;
					HardwareVersionTextBox.Text = dataflash.HardwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
					FirmwareVersionTextBox.Text = dataflash.FirmwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
					SetUpdaterButtonsState(true);
				});
			}
			else
			{
				UpdateUI(() =>
				{
					DeviceNameTextBox.Clear();
					HardwareVersionTextBox.Clear();
					FirmwareVersionTextBox.Clear();
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

			DeviceNameTextBox.ReadOnly = true;
			HardwareVersionTextBox.ReadOnly = true;
			FirmwareVersionTextBox.ReadOnly = true;

			m_updater.DeviceConnected += DeviceConnected;
			m_updater.StartMonitoring();
			Closing += (s, e) => m_updater.StopMonitoring();

			ResetDataFlashButton.Click += ResetDataFlashButton_Click;
			UpdateButton.Click += UpdateButton_Click;
			UpdateFromFileButton.Click += UpdateFromFileButton_Click;
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
				m_worker.RunWorkerAsync(firmware);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during firmware update.\n" + ex.Message);
			}
		}

		private void SetUpdaterButtonsState(bool enabled)
		{
			UpdateButton.Enabled = enabled && m_firmware != null;
			UpdateFromFileButton.Enabled = enabled;
			ResetDataFlashButton.Enabled = enabled;
		}

		private void SetAllButtonsState(bool enabled)
		{
			SetUpdaterButtonsState(enabled);
			CancelButton.Enabled = enabled;
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

		private void ResetDataFlashButton_Click(object sender, EventArgs e)
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

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			UpdateProgressBar.Value = e.ProgressPercentage;
		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = (BackgroundWorker)sender;
			var firmware = (byte[])e.Argument;

			try
			{
				UpdateUI(() => SetAllButtonsState(false));
				m_updater.StopMonitoring();

				UpdateUI(() => UpdateStatusLabel.Text = @"Reading dataflash...");
				var dataflash = m_updater.ReadDataFlash(worker);
				if (dataflash.LoadFromLdrom == false && dataflash.FirmwareVersion > 0)
				{
					dataflash.LoadFromLdrom = true;
					UpdateUI(() => UpdateStatusLabel.Text = @"Writing dataflash...");
					m_updater.WriteDataFlash(dataflash, worker);
					m_updater.RestartDevice();
					UpdateUI(() => UpdateStatusLabel.Text = @"Waiting for device after reset...");
					Thread.Sleep(2000);
				}
				UpdateUI(() => UpdateStatusLabel.Text = @"Uploading firmware...");
				m_updater.WriteFirmware(firmware, worker);

				Thread.Sleep(500);
				UpdateUI(() => UpdateStatusLabel.Text = string.Empty);
				InfoBox.Show("Firmware successfully updated.");
			}
			catch (Exception ex)
			{
				InfoBox.Show("An exception occured during firmware update.\n" + ex.Message);
			}
			finally
			{
				UpdateUI(() => SetAllButtonsState(true));
				m_updater.StartMonitoring();
			}
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
	}
}
