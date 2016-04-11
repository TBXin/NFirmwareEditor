using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class FirmwareUpdaterWindow : EditorDialogWindow
	{
		private static readonly IDictionary<string, string> s_deviceNames = new Dictionary<string, string>
		{
			{ "E052", "Joyetech eVic-VTC Mini"},
			{ "E060", "Joyetech Cuboid"},
			{ "M011", "Eleaf iStick TC100W"},
			{ "W007", "Wismec Presa TC75W"},
			{ "W010", "Vaporflask Classic"},
			{ "W011", "Vaporflask Lite"},
			{ "W013", "Vaporflask Stout"},
			{ "W014", "Wismec Reuleaux RX200"}
		};

		private readonly FirmwareUpdater m_updater = new FirmwareUpdater();

		public FirmwareUpdaterWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			InitializeControls();
		}

		private void DeviceConnected(bool isConnected)
		{
			System.Diagnostics.Trace.WriteLine(isConnected);
			if (isConnected)
			{
				var dataflash = m_updater.ReadDataFlash();
				var productId = dataflash.ProductId;
				var deviceName = s_deviceNames.ContainsKey(productId) ? s_deviceNames[productId] : "Unknown device";

				Invoke(new Action(() =>
				{
					DeviceNameTextBox.Text = deviceName;
					HardwareVersionTextBox.Text = dataflash.HardwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
					FirmwareVersionTextBox.Text = dataflash.FirmwareVersion.ToString("0.00", CultureInfo.InvariantCulture);
				}));
			}
			else
			{
				Invoke(new Action(() =>
				{
					DeviceNameTextBox.Clear();
					HardwareVersionTextBox.Clear();
					FirmwareVersionTextBox.Clear();
				}));
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
		}
	}
}
