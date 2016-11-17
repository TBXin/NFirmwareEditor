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
		private const int MinimumSupportedBuildNumber = 161116;

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
			ProfilesTabControl.TabPages.Clear();

			FirmwareVersionTextBox.ReadOnly = true;
			FirmwareVersionTextBox.BackColor = Color.White;

			BuildTextBox.ReadOnly = true;
			BuildTextBox.BackColor = Color.White;

			HardwareVersionTextBox.ReadOnly = true;
			HardwareVersionTextBox.BackColor = Color.White;

			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = (int)(BrightnessTrackBar.Value * 100m / 255) + @"%";

			var lineContentItems = new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Non dominant (Pwr / Temp)", ArcticFoxConfiguration.LineContent.NonDominant),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Volts", ArcticFoxConfiguration.LineContent.Volt),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Output Volts", ArcticFoxConfiguration.LineContent.Vout),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Output Amps", ArcticFoxConfiguration.LineContent.Amps),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Resistance", ArcticFoxConfiguration.LineContent.Resistance),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Live Resistance", ArcticFoxConfiguration.LineContent.RealResistance),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Puffs", ArcticFoxConfiguration.LineContent.Puffs),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Puffs Time", ArcticFoxConfiguration.LineContent.Time),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery(s) Volts", ArcticFoxConfiguration.LineContent.BatteryVolts),
				
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Date/Time", ArcticFoxConfiguration.LineContent.DateTime),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Board Temperature", ArcticFoxConfiguration.LineContent.BoardTemperature),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery", ArcticFoxConfiguration.LineContent.Battery),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery + %", ArcticFoxConfiguration.LineContent.BatteryWithPercents),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery + V", ArcticFoxConfiguration.LineContent.BatteryWithVolts)
			};

			var linesComboBoxes = new[]
			{
				VWLine1ComboBox, VWLine2ComboBox, VWLine3ComboBox, VWLine4ComboBox,
				TCLine1ComboBox, TCLine2ComboBox, TCLine3ComboBox, TCLine4ComboBox,
			};

			foreach (var lineComboBox in linesComboBoxes)
			{
				lineComboBox.Items.Clear();
				lineComboBox.Items.AddRange(lineContentItems);
			}

			ClockTypeComboBox.Items.Clear();
			ClockTypeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.ClockType>("Analog", ArcticFoxConfiguration.ClockType.Analog),
				new NamedItemContainer<ArcticFoxConfiguration.ClockType>("Digital", ArcticFoxConfiguration.ClockType.Digital)
			});

			ScreensaverTimeComboBox.Items.Clear();
			ScreensaverTimeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("Off", ArcticFoxConfiguration.ScreenProtectionTime.Off),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("1 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min1),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("2 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min2),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("5 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min5),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("10 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min10),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("15 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min15),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("20 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min20),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("30 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min30)
			});

			var clickItems = new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("None", ArcticFoxConfiguration.ClickAction.None),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Edit", ArcticFoxConfiguration.ClickAction.Edit),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Main Menu", ArcticFoxConfiguration.ClickAction.MainMenu),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Preheat Menu", ArcticFoxConfiguration.ClickAction.Preheat),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Select Profile", ArcticFoxConfiguration.ClickAction.ProfileSelector),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Edit Profile", ArcticFoxConfiguration.ClickAction.ProfileEdit),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("TDom", ArcticFoxConfiguration.ClickAction.TemperatureDominant),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Show Clock", ArcticFoxConfiguration.ClickAction.MainScreenClock),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("LSL On / Off", ArcticFoxConfiguration.ClickAction.Lsl),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("On / Off", ArcticFoxConfiguration.ClickAction.OnOff)
			};

			var clickComboBoxes = new[] { Clicks2ComboBox, Clicks3ComboBox, Clicks4ComboBox };
			foreach (var clickComboBox in clickComboBoxes)
			{
				clickComboBox.Items.Clear();
				clickComboBox.Items.AddRange(clickItems);
			}

			m_connector.DeviceConnected += DeviceConnected;
			Load += (s, e) => m_connector.StartUSBConnectionMonitoring();
			Closing += (s, e) => m_connector.StopUSBConnectionMonitoring();

			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_configuration = ReadConfiguration();
			InitializeWorkspace();
		}

		private void UploadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			SaveWorkspace();
			WriteConfiguration();
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_connector.ResetDataflash();
			DownloadButton_Click(null, EventArgs.Empty);
		}

		private void InitializeWorkspace()
		{
			//MainContainer.
			var deviceInfo = m_configuration.Info;
			{
				DeviceNameLabel.Text = HidDeviceInfo.Get(deviceInfo.ProductId).Name;
				FirmwareVersionTextBox.Text = (deviceInfo.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
				BuildTextBox.Text = deviceInfo.FirmwareBuild.ToString();
				HardwareVersionTextBox.Text = (deviceInfo.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			}

			var general = m_configuration.General;
			{
				for (var i = 0; i < general.Profiles.Length; i++)
				{
					var tabName = "P" + (i + 1);
					ProfileTabContent tabContent;

					if (ProfilesTabControl.TabPages.Count <= i)
					{
						var tabPage = new TabPage(tabName);
						tabContent = new ProfileTabContent(deviceInfo.MaxPower / 10) { Dock = DockStyle.Fill };
						tabPage.Controls.Add(tabContent);
						ProfilesTabControl.TabPages.Add(tabPage);
					}
					else
					{
						tabContent = (ProfileTabContent)ProfilesTabControl.TabPages[i].Controls[0];
					}

					var profile = general.Profiles[i];
					tabContent.Initialize(profile);
				}
			}

			var ui = m_configuration.Interface;
			{
				BrightnessTrackBar.Value = ui.Brightness;
				IdleTimeUpDow.Value = ui.DimTimeout;
				StealthModeCheckBox.Checked = ui.IsStealthMode;
				FlippedModeCheckBox.Checked = ui.IsFlipped;
				BatteryPercentsCheckBox.Checked = ui.IsBatteryPercents;
				UseClassicMenuCheckBox.Checked = ui.IsClassicMenu;
				ShowLogoCheckBox.Checked = ui.IsLogoEnabled;
				//ClockTypeComboBox.SelectItem(ui.ClockType);
				ScreensaverTimeComboBox.SelectItem(ui.ScreensaveDuration);

				InitializeLineContentEditor(ui.VWLines.Line1, VWLine1ComboBox, VWLine1FireCheckBox);
				InitializeLineContentEditor(ui.VWLines.Line2, VWLine2ComboBox, VWLine2FireCheckBox);
				InitializeLineContentEditor(ui.VWLines.Line3, VWLine3ComboBox, VWLine3FireCheckBox);
				InitializeLineContentEditor(ui.VWLines.Line4, VWLine4ComboBox, VWLine4FireCheckBox);

				InitializeLineContentEditor(ui.TCLines.Line1, TCLine1ComboBox, TCLine1FireCheckBox);
				InitializeLineContentEditor(ui.TCLines.Line2, TCLine2ComboBox, TCLine2FireCheckBox);
				InitializeLineContentEditor(ui.TCLines.Line3, TCLine3ComboBox, TCLine3FireCheckBox);
				InitializeLineContentEditor(ui.TCLines.Line4, TCLine4ComboBox, TCLine4FireCheckBox);

				Clicks2ComboBox.SelectItem(ui.Clicks[0]);
				Clicks3ComboBox.SelectItem(ui.Clicks[1]);
				Clicks4ComboBox.SelectItem(ui.Clicks[2]);

				WakeUpByPlusMinusCheckBox.Checked = ui.WakeUpByPlusMinus;
				Step1WCheckBox.Checked = ui.IsPowerStep1W;
			}

			var stats = m_configuration.Counters;
			{
				PuffsUpDown.Value = Math.Max(0, Math.Min(stats.PuffsCount, 99999));
				PuffsTimeUpDown.Value = Math.Max(0, Math.Min(stats.PuffsTime / 10m, 99999));
			}
		}

		private void SaveWorkspace()
		{
			for (var i = 0; i < m_configuration.General.Profiles.Length; i++)
			{
				var tabContent = (ProfileTabContent)ProfilesTabControl.TabPages[i].Controls[0];
				tabContent.Save(m_configuration.General.Profiles[i]);
			}
		}

		private void InitializeLineContentEditor(ArcticFoxConfiguration.LineContent content, ComboBox comboBox, CheckBox checkBox)
		{
			var contentCopy = content;
			checkBox.Checked = contentCopy.HasFlag(ArcticFoxConfiguration.LineContent.FireTimeMask);
			contentCopy &= ~ArcticFoxConfiguration.LineContent.FireTimeMask;
			comboBox.SelectItem(contentCopy);
		}

		private bool ValidateConnectionStatus()
		{
			while (!m_isDeviceConnected)
			{
				var result = MessageBox.Show
				(
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose.",
					"Information",
					MessageBoxButtons.OKCancel
				);
				if (result == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
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

		private void WriteConfiguration()
		{
			var data = BinaryStructure.Write(m_configuration, new byte[512]);
			try
			{
				m_connector.WriteConfiguration(data);
			}
			catch (TimeoutException)
			{
				MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void DeviceConnected(bool isConnected)
		{
			m_isDeviceConnected = isConnected;
			if (m_isDeviceWasConnectedOnce) return;

			if (!m_isDeviceConnected)
			{
				UpdateUI(() =>
				{
					UpdateUI(() => WelcomeLabel.Text = string.Format("Connect device with\n\nArctic Fox\n[{0}]\n\nfirmware or newer\nto continue...", MinimumSupportedBuildNumber));
					MainContainer.SelectedPage = WelcomePage;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				m_configuration = ReadConfiguration();
				if (m_configuration == null || m_configuration.Info.FirmwareBuild < MinimumSupportedBuildNumber)
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
