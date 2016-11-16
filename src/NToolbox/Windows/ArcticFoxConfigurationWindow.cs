using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NCore;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;

namespace NToolbox.Windows
{
	public partial class ArcticFoxConfigurationWindow : WindowBase
	{
		private const int MinimumSupportedBuildNumber = 161115;

		private readonly HidConnector m_connector = new HidConnector();

		private bool m_isDeviceWasConnectedOnce;
		private bool m_isDeviceConnected;
		private ArcticFoxConfiguration m_configuration;

		public ArcticFoxConfigurationWindow()
		{
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
		{
			MainContainer.SelectedPage = WelcomePage;

			FirmwareVersionTextBox.ReadOnly = true;
			FirmwareVersionTextBox.BackColor = Color.White;

			BuildTextBox.ReadOnly = true;
			BuildTextBox.BackColor = Color.White;

			HardwareVersionTextBox.ReadOnly = true;
			HardwareVersionTextBox.BackColor = Color.White;

			m_connector.DeviceConnected += DeviceConnected;
			Load += (s, e) => m_connector.StartUSBConnectionMonitoring();
			Closing += (s, e) => m_connector.StopUSBConnectionMonitoring();
		}

		private void InitializeWorkspace()
		{
			var deviceInfo = m_configuration.Info;
			{
				DeviceNameLabel.Text = HidDeviceInfo.Get(deviceInfo.ProductId).Name;
				FirmwareVersionTextBox.Text = (deviceInfo.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
				BuildTextBox.Text = deviceInfo.FirmwareBuild.ToString();
				HardwareVersionTextBox.Text = (deviceInfo.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			}

			var general = m_configuration.General;
			{
				ProfilesTabControl.TabPages.Clear();
				for (var i = 0; i < general.Profiles.Length; i++)
				{
					var profile = general.Profiles[i];
					var tabPage = new TabPage("P" + (i + 1));
					var tab = new ProfileTabContent { Dock = DockStyle.Fill };
					{
						tab.ProfileNameTextBox.Text = profile.Name;
						tab.PowerUpDown.Value = Math.Max(tab.PowerUpDown.Minimum, Math.Min(profile.Power / 10m, tab.PowerUpDown.Maximum));
						tab.PreheatTypeComboBox.SelectItem(profile.Flags.IsPreheatInPercents);
						tab.PreheatPowerUpDown.Value = profile.Flags.IsPreheatInPercents 
							? profile.PreheatPower : Math.Max(tab.PreheatPowerUpDown.Minimum, Math.Min(profile.PreheatPower / 10m, tab.PreheatPowerUpDown.Maximum));
						tab.PreheatTimeUpDown.Value = profile.PreheatTime / 100m;

						tab.TemperatureTypeComboBox.SelectItem(profile.Flags.IsCelcius);
						tab.TemperatureUpDown.Value =profile.Temperature;
						tab.TemperatureDominantCheckBox.Checked = profile.Flags.IsTemperatureDominant;

						tab.MaterialComboBox.SelectItem(profile.Flags.Material);
						tab.TCRUpDown.Value = profile.TCR;
						tab.ResistanceUpDown.Value = profile.Resistance / 1000m;
						tab.ResistanceLockedCheckBox.Checked = profile.Flags.IsResistanceLocked;
					}
					tabPage.Controls.Add(tab);
					ProfilesTabControl.TabPages.Add(tabPage);
				}
			}
		}

		private ArcticFoxConfiguration ReadConfiguration()
		{
			byte[] data = null;
			try
			{
				data = m_connector.ReadConfiguration();
			}
			catch (TimeoutException)
			{
			}
			return data != null ? BinaryStructure.Read<ArcticFoxConfiguration>(data) : null;
		}

		private void DeviceConnected(bool isConnected)
		{
			m_isDeviceConnected = isConnected;
			if (m_isDeviceWasConnectedOnce) return;

			if (!m_isDeviceConnected)
			{
				UpdateUI(() =>
				{
					UpdateUI(() => WelcomeLabel.Text = string.Format("Waiting for device with\n\nArctic Fox firmware\n\n{0} or newer", MinimumSupportedBuildNumber));
					MainContainer.SelectedPage = WelcomePage;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				m_configuration = ReadConfiguration();
				if (m_configuration == null)
				{
					DeviceConnected(false);
					return;
				}

				UpdateUI(() =>
				{
					InitializeWorkspace();
					MainContainer.SelectedPage = WorkspacePage;
					m_isDeviceWasConnectedOnce = true;
				}, false);
			}
			catch (Exception ex)
			{
				//s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}
	}
}
