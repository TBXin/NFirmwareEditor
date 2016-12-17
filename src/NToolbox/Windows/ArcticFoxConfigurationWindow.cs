using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;
using NToolbox.Properties;

namespace NToolbox.Windows
{
	public partial class ArcticFoxConfigurationWindow : WindowBase
	{
		private const ushort MaxPower = 2500;
		private const int MinimumSupportedBuildNumber = 161215;
		private const int MaximumSupportedSettingsVersion = 4;

		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };

		private readonly Func<BackgroundWorker, byte[]> m_deviceConfigurationProvider = worker => HidConnector.Instance.ReadConfiguration(worker);

		private Label[] m_powerCurveLabels;
		private Button[] m_powerCurveButtons;
		private Label[] m_tfrLabels;
		private Button[] m_tfrButtons;

		private bool m_isWorkspaceOpen;
		private bool m_isDeviceConnected;
		private ArcticFoxConfiguration m_configuration;

		public ArcticFoxConfigurationWindow()
		{
			InitializeComponent();
			Initialize();
			InitializeControls();
		}

		private void Initialize()
		{
			m_worker.DoWork += Worker_DoWork;
			m_worker.ProgressChanged += (s, e) => ProgressLabel.Text = e.ProgressPercentage + @"%";
			m_worker.RunWorkerCompleted += (s, e) => ProgressLabel.Text = @"Operation completed";

			HidConnector.Instance.DeviceConnected += isConnected => DeviceConnected(isConnected, false);
			Shown += (s, e) =>
			{
				new Thread(() =>
				{
					DeviceConnected(HidConnector.Instance.IsDeviceConnected, true);
					UpdateUI(() => NativeMethods.SetForegroundWindow(Handle));
				}).Start();
			};
		}

		private void InitializeControls()
		{
			MainContainer.SelectedPage = WelcomePage;
			ConnectLinkLabel.LinkClicked += ConnectLinkLabel_LinkClicked;
			CreateConfigurationLinkLabel.LinkClicked += CreateConfigurationLinkLabel_LinkClicked;
			OpenConfigurationLinkLabel.LinkClicked += OpenConfigurationLinkLabel_LinkClicked;

			ProfilesTabControl.TabPages.Clear();

			FirmwareVersionTextBox.ReadOnly = true;
			FirmwareVersionTextBox.BackColor = Color.White;

			BuildTextBox.ReadOnly = true;
			BuildTextBox.BackColor = Color.White;

			HardwareVersionTextBox.ReadOnly = true;
			HardwareVersionTextBox.BackColor = Color.White;

			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = (int)(BrightnessTrackBar.Value * 100m / 255) + @"%";

			m_powerCurveLabels = new[] { PowerCurve1Label, PowerCurve2Label, PowerCurve3Label, PowerCurve4Label, PowerCurve5Label, PowerCurve6Label, PowerCurve7Label, PowerCurve8Label };
			m_powerCurveButtons = new[] { PowerCurve1EditButton, PowerCurve2EditButton, PowerCurve3EditButton, PowerCurve4EditButton, PowerCurve5EditButton, PowerCurve6EditButton, PowerCurve7EditButton, PowerCurve8EditButton };

			m_tfrLabels = new[] { TFR1Label, TFR2Label, TFR3Label, TFR4Label, TFR5Label, TFR6Label, TFR7Label, TFR8Label };
			m_tfrButtons = new[] { TFR1EditButton, TFR2EditButton, TFR3EditButton, TFR4EditButton, TFR5EditButton, TFR6EditButton, TFR7EditButton, TFR8EditButton };

			for (var i = 0; i < m_tfrButtons.Length; i++)
			{
				m_powerCurveButtons[i].Tag = i;
				m_powerCurveButtons[i].Click += PowerCurveEditButton_Click;
			}

			for (var i = 0; i < m_tfrButtons.Length; i++)
			{
				m_tfrButtons[i].Tag = i;
				m_tfrButtons[i].Click += TFREditButton_Click;
			}

			BatteryEditButton.Click += BatteryEditButton_Click;

			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;

			InitializeComboBoxes();
			InitializeMenu();
		}

		private void InitializeMenu()
		{
			NewMenuItem.Click += NewMenuItem_Click;
			OpenMenuItem.Click += OpenMenuItem_Click;
			SaveAsMenuItem.Click += SaveAsMenuItem_Click;
		}

		private void NewMenuItem_Click(object sender, EventArgs e)
		{
			ReadConfigurationAndShowResult(w => PrepairConfiguration(Resources.new_configuration, m_configuration));
		}

		private void OpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenConfigurationFile(m_configuration);
		}

		private void SaveAsMenuItem_Click(object sender, EventArgs e)
		{
			if (m_configuration == null) return;

			using (var sf = new SaveFileDialog { Filter = FileFilters.ArcticFoxConfigFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;

				try
				{
					var cfgCopy = BinaryStructure.Copy(m_configuration);
					{
						cfgCopy.Info.FirmwareVersion = 0;
						cfgCopy.Info.HardwareVersion = 0;
						cfgCopy.Info.MaxPower = 0;
						cfgCopy.Info.NumberOfBatteries = 0;
						cfgCopy.Info.ProductId = string.Empty;
					}
					var bytes = BinaryStructure.Write(cfgCopy);
					File.WriteAllBytes(sf.FileName, bytes);
				}
				catch (Exception ex)
				{
					Trace.ErrorException("An error occurred during save arctic fox configuration.", ex);
				}
			}
		}

		private void ConnectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (!ValidateConnectionStatus()) return;
			ReadConfigurationAndShowResult(m_deviceConfigurationProvider);
		}

		private void CreateConfigurationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ReadConfigurationAndShowResult(w => PrepairConfiguration(Resources.new_configuration));
		}

		private void OpenConfigurationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenConfigurationFile(m_configuration);
		}

		private void InitializeComboBoxes()
		{
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

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Last Puff Time", ArcticFoxConfiguration.LineContent.LastPuff),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Last Power", ArcticFoxConfiguration.LineContent.LastPower),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Last Temperature", ArcticFoxConfiguration.LineContent.LastTemperature),

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

			ChargeScreenComboBox.Items.Clear();
			ChargeScreenComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.ChargeScreenType>("Classic", ArcticFoxConfiguration.ChargeScreenType.Classic),
				new NamedItemContainer<ArcticFoxConfiguration.ChargeScreenType>("Extended", ArcticFoxConfiguration.ChargeScreenType.Extended)
			});

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

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Smart On / Off", ArcticFoxConfiguration.ClickAction.SmartOnOff),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("LSL On / Off", ArcticFoxConfiguration.ClickAction.LslOnOff),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("On / Off", ArcticFoxConfiguration.ClickAction.OnOff)
			};

			var clickComboBoxes = new[] { Clicks2ComboBox, Clicks3ComboBox, Clicks4ComboBox };
			foreach (var clickComboBox in clickComboBoxes)
			{
				clickComboBox.Items.Clear();
				clickComboBox.Items.AddRange(clickItems);
			}

			BatteryModelComboBox.Items.Clear();
			BatteryModelComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Generic Battery", ArcticFoxConfiguration.BatteryModel.Generic),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Samsung 25R", ArcticFoxConfiguration.BatteryModel.Samsung25R),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Samsung 30Q", ArcticFoxConfiguration.BatteryModel.Samsung30Q),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("LG HG2", ArcticFoxConfiguration.BatteryModel.LGHG2),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("LG HE4", ArcticFoxConfiguration.BatteryModel.LGHE4),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Sony VTC4", ArcticFoxConfiguration.BatteryModel.SonyVTC4),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Sony VTC5", ArcticFoxConfiguration.BatteryModel.SonyVTC5),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Custom", ArcticFoxConfiguration.BatteryModel.Custom)
			});
			BatteryModelComboBox.SelectedValueChanged += (s, e) =>
			{
				var batteryModel = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
				BatteryEditButton.Visible = batteryModel == ArcticFoxConfiguration.BatteryModel.Custom;
			};
		}

		private bool ValidateConnectionStatus()
		{
			while (!m_isDeviceConnected)
			{
				var result = InfoBox.Show
				(
					// ReSharper disable once LocalizableElement
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose.",
					MessageBoxButtons.OKCancel
				);
				if (result == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
		}

		[NotNull]
		private ConfigurationReadResult ReadConfiguration([NotNull] Func<BackgroundWorker, byte[]> configurationProvider, bool useWorker = true)
		{
			if (configurationProvider == null) throw new ArgumentNullException("configurationProvider");

			try
			{
				var data = configurationProvider(m_worker);
				if (data == null) return new ConfigurationReadResult(null, ReadResult.UnableToRead);

				var info = BinaryStructure.Read<ArcticFoxConfiguration.DeviceInfo>(data);
				if (info.FirmwareBuild < MinimumSupportedBuildNumber)
				{
					return new ConfigurationReadResult(null, ReadResult.OutdatedFirmware);
				}
				if (info.SettingsVersion > MaximumSupportedSettingsVersion)
				{
					return new ConfigurationReadResult(null, ReadResult.OutdatedToolbox);
				}

				var configuration = BinaryStructure.Read<ArcticFoxConfiguration>(data);
				return new ConfigurationReadResult(configuration, ReadResult.Success);
			}
			catch (TimeoutException)
			{
				return new ConfigurationReadResult(null, ReadResult.UnableToRead);
			}
		}

		private void WriteConfiguration()
		{
			var data = BinaryStructure.Write(m_configuration);
			try
			{
				HidConnector.Instance.WriteConfiguration(data, m_worker);
			}
			catch (TimeoutException)
			{
				InfoBox.Show("Unable to write configuration.");
			}
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
				for (var i = 0; i < general.Profiles.Length; i++)
				{
					var tabName = "P" + (i + 1);
					ProfileTabContent tabContent;

					if (ProfilesTabControl.TabPages.Count <= i)
					{
						SelectedProfleComboBox.Items.Add(new NamedItemContainer<byte>(tabName, (byte)i));

						var tabPage = new TabPage(tabName);
						tabContent = new ProfileTabContent(this) { Dock = DockStyle.Fill };
						tabPage.Controls.Add(tabContent);
						ProfilesTabControl.TabPages.Add(tabPage);
					}
					else
					{
						tabContent = (ProfileTabContent)ProfilesTabControl.TabPages[i].Controls[0];
					}

					tabContent.Initialize(m_configuration, i);
					tabContent.UpdatePowerCurveNames(m_configuration.Advanced.PowerCurves);
					tabContent.UpdateTFRNames(m_configuration.Advanced.TFRTables);
				}

				ProfilesTabControl.SelectedIndex = Math.Max(0, Math.Min(general.SelectedProfile, ProfilesTabControl.TabCount));
				SelectedProfleComboBox.SelectItem(general.SelectedProfile);
				SmartCheckBox.Checked = general.IsSmartEnabled;
			}

			var ui = m_configuration.Interface;
			{
				BrightnessTrackBar.Value = ui.Brightness;
				IdleTimeUpDow.SetValue(ui.DimTimeout);
				PuffScreenDelayUpDown.SetValue(ui.PuffScreenDelay / 10m);
				StealthModeCheckBox.Checked = ui.IsStealthMode;
				FlippedModeCheckBox.Checked = ui.IsFlipped;
				ChargeScreenComboBox.SelectItem(ui.ChargeScreenType);
				UseClassicMenuCheckBox.Checked = ui.IsClassicMenu;
				ShowLogoCheckBox.Checked = ui.IsLogoEnabled;
				ShowClockCheckBox.Checked = ui.IsClockOnMainScreen;
				ClockTypeComboBox.SelectItem(ui.ClockType);
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

			var advanced = m_configuration.Advanced;
			{
				PuffCutOffUpDown.SetValue(advanced.PuffCutOff / 10m);
				ShuntCorrectionUpDown.SetValue(advanced.ShuntCorrection);
				BatteryModelComboBox.SelectItem(advanced.BatteryModel);
				X32CheckBox.Checked = advanced.IsX32;
				LightSleepCheckBox.Checked = advanced.IsLightSleepMode;
				UsbChargeCheckBox.Checked = advanced.IsUsbCharge;
				ResetCountersCheckBox.Checked = advanced.ResetCountersOnStartup;

				UpdatePowerCurveLabels(advanced.PowerCurves);
				UpdateTFRLables(advanced.TFRTables);
			}
		}

		private void InitializeLineContentEditor(ArcticFoxConfiguration.LineContent content, ComboBox comboBox, CheckBox checkBox)
		{
			var contentCopy = content;
			checkBox.Checked = contentCopy.HasFlag(ArcticFoxConfiguration.LineContent.FireTimeMask);
			contentCopy &= ~ArcticFoxConfiguration.LineContent.FireTimeMask;
			comboBox.SelectItem(contentCopy);
		}

		private void SaveWorkspace()
		{
			var general = m_configuration.General;
			{
				// Profiles Tab
				for (var i = 0; i < general.Profiles.Length; i++)
				{
					var tabContent = (ProfileTabContent)ProfilesTabControl.TabPages[i].Controls[0];
					tabContent.Save(general.Profiles[i]);
				}

				general.SelectedProfile = SelectedProfleComboBox.GetSelectedItem<byte>();
				general.IsSmartEnabled = SmartCheckBox.Checked;
			}

			var ui = m_configuration.Interface;
			{
				// General -> Screen Tab
				ui.Brightness = (byte)BrightnessTrackBar.Value;
				ui.DimTimeout = (byte)IdleTimeUpDow.Value;
				ui.PuffScreenDelay = (byte)(PuffScreenDelayUpDown.Value * 10);
				ui.IsStealthMode = StealthModeCheckBox.Checked;
				ui.IsFlipped = FlippedModeCheckBox.Checked;
				ui.ChargeScreenType = ChargeScreenComboBox.GetSelectedItem<ArcticFoxConfiguration.ChargeScreenType>();
				ui.IsClassicMenu = UseClassicMenuCheckBox.Checked;
				ui.IsLogoEnabled = ShowLogoCheckBox.Checked;
				ui.IsClockOnMainScreen = ShowClockCheckBox.Checked;
				ui.ClockType = ClockTypeComboBox.GetSelectedItem<ArcticFoxConfiguration.ClockType>();
				ui.ScreensaveDuration = ScreensaverTimeComboBox.GetSelectedItem<ArcticFoxConfiguration.ScreenProtectionTime>();

				// General -> Layout Tab
				ui.VWLines.Line1 = SaveLineContent(VWLine1ComboBox, VWLine1FireCheckBox);
				ui.VWLines.Line2 = SaveLineContent(VWLine2ComboBox, VWLine2FireCheckBox);
				ui.VWLines.Line3 = SaveLineContent(VWLine3ComboBox, VWLine3FireCheckBox);
				ui.VWLines.Line4 = SaveLineContent(VWLine4ComboBox, VWLine4FireCheckBox);

				ui.TCLines.Line1 = SaveLineContent(TCLine1ComboBox, TCLine1FireCheckBox);
				ui.TCLines.Line2 = SaveLineContent(TCLine2ComboBox, TCLine2FireCheckBox);
				ui.TCLines.Line3 = SaveLineContent(TCLine3ComboBox, TCLine3FireCheckBox);
				ui.TCLines.Line4 = SaveLineContent(TCLine4ComboBox, TCLine4FireCheckBox);

				// General -> Controls Tab
				ui.Clicks[0] = Clicks2ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.Clicks[1] = Clicks3ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.Clicks[2] = Clicks4ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.WakeUpByPlusMinus = WakeUpByPlusMinusCheckBox.Checked;
				ui.IsPowerStep1W = Step1WCheckBox.Checked;
			}

			var stats = m_configuration.Counters;
			{
				var now = DateTime.Now;

				// General -> Stats Tab
				stats.PuffsCount = (ushort)PuffsUpDown.Value;
				stats.PuffsTime = (ushort)(PuffsTimeUpDown.Value * 10);

				// Time sync
				stats.DateTime.Year = (ushort)now.Year;
				stats.DateTime.Month = (byte)now.Month;
				stats.DateTime.Day = (byte)now.Day;
				stats.DateTime.Hour = (byte)now.Hour;
				stats.DateTime.Minute = (byte)now.Minute;
				stats.DateTime.Second = (byte)now.Second;
			}

			var advanced = m_configuration.Advanced;
			{
				advanced.PuffCutOff = (byte)(PuffCutOffUpDown.Value * 10);
				advanced.ShuntCorrection = (byte)ShuntCorrectionUpDown.Value;
				advanced.BatteryModel = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
				advanced.IsX32 = X32CheckBox.Checked;
				advanced.IsLightSleepMode = LightSleepCheckBox.Checked;
				advanced.IsUsbCharge = UsbChargeCheckBox.Checked;
				advanced.ResetCountersOnStartup = ResetCountersCheckBox.Checked;
			}
		}

		private ArcticFoxConfiguration.LineContent SaveLineContent(ComboBox comboBox, CheckBox checkBox)
		{
			var result = comboBox.GetSelectedItem<ArcticFoxConfiguration.LineContent>();
			if (checkBox.Checked)
			{
				result |= ArcticFoxConfiguration.LineContent.FireTimeMask;
			}
			return result;
		}

		private void SetControlButtonsState(bool enabled)
		{
			DownloadButton.Enabled = UploadButton.Enabled = ResetButton.Enabled = enabled;
		}

		private void UpdatePowerCurveLabels(ArcticFoxConfiguration.PowerCurve[] curves)
		{
			for (var i = 0; i < m_tfrLabels.Length; i++)
			{
				m_powerCurveLabels[i].Text = curves[i].Name + @":";
			}
		}

		private void UpdateTFRLables(ArcticFoxConfiguration.TFRTable[] tfrTables)
		{
			for (var i = 0; i < m_tfrLabels.Length; i++)
			{
				m_tfrLabels[i].Text = @"[TFR] " + tfrTables[i].Name + @":";
			}
		}

		private string GetErrorMessage(string operationName)
		{
			return "An error occurred during " + operationName + "...\n\n" + "To continue, please activate or reconnect your device.";
		}

		private void DownloadSettings()
		{
			try
			{
				var readResult = ReadConfiguration(m_deviceConfigurationProvider);
				if (readResult.Result != ReadResult.Success)
				{
					InfoBox.Show("Something strange happened! Please restart application.");
					return;
				}
				m_configuration = readResult.Configuration;
				UpdateUI(InitializeWorkspace);
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
				InfoBox.Show(GetErrorMessage("downloading settings"));
			}
		}

		internal void UpdatePowerCurveNames()
		{
			UpdatePowerCurveLabels(m_configuration.Advanced.PowerCurves);
			foreach (TabPage tabPage in ProfilesTabControl.TabPages)
			{
				var tabContent = tabPage.Controls[0] as ProfileTabContent;
				if (tabContent == null) continue;

				tabContent.UpdatePowerCurveNames(m_configuration.Advanced.PowerCurves);
			}
		}

		internal void UpdateTFRCurveNames()
		{
			UpdateTFRLables(m_configuration.Advanced.TFRTables);
			foreach (TabPage tabPage in ProfilesTabControl.TabPages)
			{
				var tabContent = tabPage.Controls[0] as ProfileTabContent;
				if (tabContent == null) continue;

				tabContent.UpdateTFRNames(m_configuration.Advanced.TFRTables);
			}
		}

		private byte[] PrepairConfiguration(byte[] source, ArcticFoxConfiguration existedConfiguration = null)
		{
			var result = BinaryStructure.Read<ArcticFoxConfiguration>(source);
			if (existedConfiguration == null)
			{
				result.Info.MaxPower = MaxPower;
			}
			else
			{
				result.Info = existedConfiguration.Info;
			}
			return BinaryStructure.Write(result);
		}

		private void OpenConfigurationFile(ArcticFoxConfiguration existedConfiguration)
		{
			string fileName;
			using (var op = new OpenFileDialog { Filter = FileFilters.ArcticFoxConfigFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			var result = ReadConfiguration(w => File.ReadAllBytes(fileName));
			if (result.Result == ReadResult.Success)
			{
				if (existedConfiguration == null)
				{
					result.Configuration.Info.MaxPower = MaxPower;
				}
				else
				{
					result.Configuration.Info = existedConfiguration.Info;
				}
				OpenWorkspace(result.Configuration);
			}
			else if (result.Result == ReadResult.OutdatedFirmware)
			{
				InfoBox.Show("You are trying to open the configuration file from a legacy ArcticFox firmware versions. This operation is not supported.");
			}
			else if (result.Result == ReadResult.OutdatedToolbox)
			{
				InfoBox.Show("You are trying to open the configuration file from a future ArcticFox firmware versions. This operation is not supported.");
			}
			else if (result.Result == ReadResult.UnableToRead)
			{
				InfoBox.Show("Invalid configuration file!");
			}
			else
			{
				InfoBox.Show("Shit happens!");
			}
		}

		private void BatteryEditButton_Click(object sender, EventArgs e)
		{
			using (var editor = new DischargeProfileWindow(m_configuration.Advanced.CustomBatteryProfile))
			{
				editor.ShowDialog();
			}
		}

		private void PowerCurveEditButton_Click(object sender, EventArgs e)
		{
			var button = sender as Button;
			if (button == null) return;

			var curveIndex = (int)button.Tag;
			var curve = m_configuration.Advanced.PowerCurves[curveIndex];

			using (var editor = new PowerCurveProfileWindow(curve))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;

				UpdatePowerCurveNames();
			}
		}

		private void TFREditButton_Click(object sender, EventArgs e)
		{
			var button = sender as Button;
			if (button == null) return;

			var tfrIndex = (int)button.Tag;
			var tfrTable = m_configuration.Advanced.TFRTables[tfrIndex];
			using (var editor = new TFRProfileWindow(tfrTable))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;

				UpdateTFRCurveNames();
			}
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker => DownloadSettings()));
		}

		private void UploadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					UpdateUI(SaveWorkspace);
					WriteConfiguration();
				}
				catch (Exception ex)
				{
					Trace.Warn(ex);
					InfoBox.Show(GetErrorMessage("uploading settings"));
				}
			}));
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					HidConnector.Instance.ResetDataflash();
					DownloadSettings();
				}
				catch (Exception ex)
				{
					Trace.Warn(ex);
					InfoBox.Show(GetErrorMessage("resetting settings"));
				}
			}));
		}

		private void DeviceConnected(bool isConnected, bool onStartup)
		{
			m_isDeviceConnected = isConnected;
			UpdateUI(() =>
			{
				DownloadButton.Enabled = UploadButton.Enabled = ResetButton.Enabled = m_isDeviceConnected;
				StatusLabel.Text = @"Device is " + (m_isDeviceConnected ? "connected" : "disconnected");
			});

			if (m_isWorkspaceOpen || !onStartup) return;
			if (!m_isDeviceConnected)
			{
				ShowWelcomeScreen(string.Format("Connect device with\n\nArcticFox\n[{0}]\n\nfirmware or newer", MinimumSupportedBuildNumber));
				return;
			}
			ReadConfigurationAndShowResult(m_deviceConfigurationProvider);
		}

		private void ReadConfigurationAndShowResult(Func<BackgroundWorker, byte[]> configurationProvider)
		{
			ShowWelcomeScreen("Downloading settings...");
			try
			{
				var readResult = ReadConfiguration(configurationProvider, false);
				m_configuration = readResult.Configuration;
				if (readResult.Result == ReadResult.Success)
				{
					OpenWorkspace(readResult.Configuration);
				}
				else if (readResult.Result == ReadResult.OutdatedFirmware)
				{
					ShowWelcomeScreen(string.Format("Connect device with\n\nArcticFox\n[{0}]\n\nfirmware or newer", MinimumSupportedBuildNumber));
				}
				else if (readResult.Result == ReadResult.OutdatedToolbox)
				{
					ShowWelcomeScreen("NFE Toolbox is outdated.\n\nTo continue, please download\n\nlatest available release.");
				}
				else if (readResult.Result == ReadResult.UnableToRead)
				{
					ShowWelcomeScreen("Unable to download device settings. Reconnect your device.");
				}
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
				ShowWelcomeScreen("Unable to download device settings. Reconnect your device.");
			}
		}

		private void OpenWorkspace(ArcticFoxConfiguration configuration)
		{
			m_configuration = configuration;
			UpdateUI(() =>
			{
				InitializeWorkspace();
				MainContainer.SelectedPage = WorkspacePage;
				m_isWorkspaceOpen = true;
			}, false);
		}

		private void ShowWelcomeScreen(string message)
		{
			UpdateUI(() =>
			{
				WelcomeLabel.Text = message;
				MainContainer.SelectedPage = WelcomePage;
			});
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = (BackgroundWorker)sender;
			var wrapper = (AsyncProcessWrapper)e.Argument;

			try
			{
				UpdateUI(() => SetControlButtonsState(false));
				wrapper.Processor(worker);
			}
			finally
			{
				UpdateUI(() => SetControlButtonsState(true));
			}
		}

		private class ConfigurationReadResult
		{
			public ConfigurationReadResult(ArcticFoxConfiguration configuration, ReadResult result)
			{
				Configuration = configuration;
				Result = result;
			}

			public ArcticFoxConfiguration Configuration { get; private set; }

			public ReadResult Result { get; private set; }
		}

		private enum ReadResult
		{
			Success,
			UnableToRead,
			OutdatedFirmware,
			OutdatedToolbox
		}
	}
}
