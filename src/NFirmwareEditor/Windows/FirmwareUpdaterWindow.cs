using System;
using System.Collections.Generic;
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
		private static readonly IDictionary<string, string> s_deviceNames = new Dictionary<string, string>
		{
			{ "E052", "Joyetech eVic-VTC Mini" },
			{ "E060", "Joyetech Cuboid" },
			{ "M011", "Eleaf iStick TC100W" },
			{ "W007", "Wismec Presa TC75W" },
			{ "W010", "Vaporflask Classic" },
			{ "W011", "Vaporflask Lite" },
			{ "W013", "Vaporflask Stout" },
			{ "W014", "Wismec Reuleaux RX200" }
		};

		private readonly FirmwareUpdater m_updater = new FirmwareUpdater();
		private readonly FirmwareLoader m_loader;
		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };

		private string m_connectedDeviceProductId;

		public FirmwareUpdaterWindow(FirmwareLoader loader)
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
			InitializeControls();

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
				var deviceName = s_deviceNames.ContainsKey(m_connectedDeviceProductId) ? s_deviceNames[m_connectedDeviceProductId] : "Unknown device";

				UpdateUI(() =>
				{
					DeviceNameTextBox.Text = deviceName;
					HardwareVersionTextBox.Text = dataflash.HardwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
					FirmwareVersionTextBox.Text = dataflash.FirmwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
					OkButton.Enabled = true;
				});
			}
			else
			{
				UpdateUI(() =>
				{
					DeviceNameTextBox.Clear();
					HardwareVersionTextBox.Clear();
					FirmwareVersionTextBox.Clear();
					OkButton.Enabled = false;
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

			OkButton.Click += OkButton_Click;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			var skipValidation = ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Alt);
			string fileName;
			using (var op = new OpenFileDialog { Title = @"Select encrypted or decrypted firmware file ...", Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			try
			{
				var firmware = m_loader.LoadFile(fileName);
				if (!skipValidation && FirmwareLoader.FindByteArray(firmware, Encoding.UTF8.GetBytes(m_connectedDeviceProductId)) == -1)
				{
					InfoBox.Show("Selected firmware file is not suitable for the connected device.");
					return;
				}
				m_worker.RunWorkerAsync(firmware);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An exception occured during firmware update.\n" + ex.Message);
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
				UpdateUI(() => OkButton.Enabled = CancelButton.Enabled = false);
				m_updater.StopMonitoring();

				UpdateUI(() => UpdateStatusLabel.Text = @"Reading dataflash...");
				var dataflash = m_updater.ReadDataFlash(worker);
				if (dataflash.LoadFromLdrom == false && dataflash.FirmwareVersion > 0)
				{
					dataflash.LoadFromLdrom = true;
					UpdateUI(() => UpdateStatusLabel.Text = @"Writing dataflash...");
					m_updater.WriteDataFlash(dataflash, worker);
					m_updater.Reset();
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
				UpdateUI(() => OkButton.Enabled = CancelButton.Enabled = true);
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
