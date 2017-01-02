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
		private const byte MaxBatteries = 3;
		private const int MinimumSupportedBuildNumber = 170102;
		private const int SupportedSettingsVersion = 6;

		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };
		private readonly IEncryption m_encryption = new ArcticFoxEncryption();
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
			var menu = new ContextMenu(new[]
			{
				new MenuItem("New", NewMenuItem_Click),
				new MenuItem("Open", OpenMenuItem_Click),
				new MenuItem("Save As", SaveAsMenuItem_Click)
			});
			ConfigurationMenuButton.Click += (s, e) => menu.Show(ConfigurationMenuButton, new Point(0, ConfigurationMenuButton.Height));
		}

		private void InitializeComboBoxes()
		{
			// Fill Classic Skin ComboBoxes
			foreach (var comboBox in new[]
			{
				ClassicVWLine1ComboBox, ClassicVWLine2ComboBox, ClassicVWLine3ComboBox, ClassicVWLine4ComboBox,
				ClassicTCLine1ComboBox, ClassicTCLine2ComboBox, ClassicTCLine3ComboBox, ClassicTCLine4ComboBox,
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.ClassicSkinLineContentItems);
			}

			// Fill Circle Skin ComboBoxes
			foreach (var comboBox in new[]
			{
				CircleVWLine1ComboBox, CircleVWLine2ComboBox,
				CircleTCLine1ComboBox, CircleTCLine2ComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.CircleSkinLineContentItems);
			}
			CircleVWLine3ComboBox.Fill(PredefinedData.ArcticFox.CircleSkin3RdLineContentItems);
			CircleTCLine3ComboBox.Fill(PredefinedData.ArcticFox.CircleSkin3RdLineContentItems);

			// Fill Small Skin ComboBoxes
			foreach (var comboBox in new[] { SmallVWLine1ComboBox, SmallVWLine2ComboBox, SmallTCLine1ComboBox, SmallTCLine2ComboBox, })
			{
				comboBox.Fill(PredefinedData.ArcticFox.SmallScreenLineContentItems);
			}

			ChargeScreenComboBox.Fill(PredefinedData.ArcticFox.ChargeScreenTypes);
			ClockTypeComboBox.Fill(PredefinedData.ArcticFox.ClockTypes);
			ScreensaverTimeComboBox.Fill(PredefinedData.ArcticFox.ScreenSaverTimes);

			foreach (var clickComboBox in new[] { Clicks2ComboBox, Clicks3ComboBox, Clicks4ComboBox })
			{
				clickComboBox.Fill(PredefinedData.ArcticFox.ClickActions);
			}

			PuffsTimeFormatComboBox.Fill(PredefinedData.ArcticFox.PuffTimeFormats);
			BatteryModelComboBox.Fill(PredefinedData.ArcticFox.BatteryModels);
			BatteryModelComboBox.SelectedValueChanged += (s, e) =>
			{
				var batteryModel = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
				BatteryEditButton.Visible = batteryModel == ArcticFoxConfiguration.BatteryModel.Custom;
			};

			SelectedProfleComboBox.SelectedValueChanged += (s, e) =>
			{
				var profileIndex = SelectedProfleComboBox.GetSelectedItem<byte>();
				if (profileIndex >= ProfilesTabControl.TabCount) return;

				for (var i = 0; i < ProfilesTabControl.TabCount; i++)
				{
					var tabPage = ProfilesTabControl.TabPages[i];
					var tabContent = tabPage.Controls[0] as ProfileTabContent;
					if (tabContent == null) return;

					if (i == profileIndex)
					{
						tabContent.CanDeactive = false;
						tabContent.IsProfileActivated = true;
					}
					else
					{
						tabContent.CanDeactive = true;
					}
				}
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
				var data = configurationProvider(useWorker ? m_worker : null);
				if (data == null) return new ConfigurationReadResult(null, ReadResult.UnableToRead);

				var info = BinaryStructure.Read<ArcticFoxConfiguration.DeviceInfo>(data);
				if (info.FirmwareBuild < MinimumSupportedBuildNumber || info.SettingsVersion < SupportedSettingsVersion)
				{
					return new ConfigurationReadResult(null, ReadResult.OutdatedFirmware);
				}
				if (info.SettingsVersion > SupportedSettingsVersion)
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

				Battery2OffsetLabel.Visible = Battery2OffsetUpDown.Visible = Battery2OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 1;
				Battery3OffsetLabel.Visible = Battery3OffsetUpDown.Visible = Battery3OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 2;
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
						tabContent = new ProfileTabContent(this) { Dock = DockStyle.Fill };
						tabPage.Controls.Add(tabContent);
						ProfilesTabControl.TabPages.Add(tabPage);

						SelectedProfleComboBox.Items.Add(new NamedItemContainer<byte>(tabName, (byte)i));
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

				// Classic Screen
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line1, ClassicVWLine1ComboBox, ClassicVWLine1FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line2, ClassicVWLine2ComboBox, ClassicVWLine2FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line3, ClassicVWLine3ComboBox, ClassicVWLine3FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line4, ClassicVWLine4ComboBox, ClassicVWLine4FireCheckBox);

				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line1, ClassicTCLine1ComboBox, ClassicTCLine1FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line2, ClassicTCLine2ComboBox, ClassicTCLine2FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line3, ClassicTCLine3ComboBox, ClassicTCLine3FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line4, ClassicTCLine4ComboBox, ClassicTCLine4FireCheckBox);

				// Circle Screen
				InitializeLineContentEditor(ui.CircleSkinVWLines.Line1, CircleVWLine1ComboBox);
				InitializeLineContentEditor(ui.CircleSkinVWLines.Line2, CircleVWLine2ComboBox);
				InitializeLineContentEditor(ui.CircleSkinVWLines.Line3, CircleVWLine3ComboBox, CircleVWLine3FireCheckBox);

				InitializeLineContentEditor(ui.CircleSkinTCLines.Line1, CircleTCLine1ComboBox);
				InitializeLineContentEditor(ui.CircleSkinTCLines.Line2, CircleTCLine2ComboBox);
				InitializeLineContentEditor(ui.CircleSkinTCLines.Line3, CircleTCLine3ComboBox, CircleTCLine3FireCheckBox);

				// Small Screen
				InitializeLineContentEditor(ui.SmallSkinVWLines.Line1, SmallVWLine1ComboBox, SmallVWLine1FireCheckBox);
				InitializeLineContentEditor(ui.SmallSkinVWLines.Line2, SmallVWLine2ComboBox, SmallVWLine2FireCheckBox);

				InitializeLineContentEditor(ui.SmallSkinTCLines.Line1, SmallTCLine1ComboBox, SmallTCLine1FireCheckBox);
				InitializeLineContentEditor(ui.SmallSkinTCLines.Line2, SmallTCLine2ComboBox, SmallTCLine2FireCheckBox);

				Clicks2ComboBox.SelectItem(ui.Clicks[0]);
				Clicks3ComboBox.SelectItem(ui.Clicks[1]);
				Clicks4ComboBox.SelectItem(ui.Clicks[2]);

				WakeUpByPlusMinusCheckBox.Checked = ui.WakeUpByPlusMinus;
				Step1WCheckBox.Checked = ui.IsPowerStep1W;
			}

			var stats = m_configuration.Counters;
			{
				PuffsUpDown.SetValue(stats.PuffsCount);
				PuffsTimeUpDown.SetValue(stats.PuffsTime / 10m);
				PuffsTimeFormatComboBox.SelectItem(ui.PuffsTimeFormat);
			}

			var advanced = m_configuration.Advanced;
			{
				PuffCutOffUpDown.SetValue(advanced.PuffCutOff / 10m);
				ShuntCorrectionUpDown.SetValue(advanced.ShuntCorrection);
				BatteryModelComboBox.SelectItem(advanced.BatteryModel);
				X32CheckBox.Checked = advanced.IsX32;
				LightSleepCheckBox.Checked = advanced.IsLightSleepMode;
				ResetCountersCheckBox.Checked = advanced.ResetCountersOnStartup;
				CheckTCRCheckBox.Checked = advanced.CheckTCR;
				UsbChargeCheckBox.Checked = advanced.IsUsbCharge;
				UsbNoSleepCheckBox.Checked = advanced.UsbNoSleep;

				UpdatePowerCurveLabels(advanced.PowerCurves);
				UpdateTFRLables(advanced.TFRTables);

				Battery1OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[0] / 100m);
				Battery2OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[1] / 100m);
				Battery3OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[2] / 100m);
			}
		}

		private void InitializeLineContentEditor(ArcticFoxConfiguration.LineContent content, ComboBox comboBox, CheckBox checkBox = null)
		{
			var contentCopy = content;
			if (checkBox != null)
			{
				checkBox.Checked = contentCopy.HasFlag(ArcticFoxConfiguration.LineContent.FireTimeMask);
			}
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

				// General -> Layout Tab -> Classic Screen
				ui.ClassicSkinVWLines.Line1 = SaveLineContent(ClassicVWLine1ComboBox, ClassicVWLine1FireCheckBox);
				ui.ClassicSkinVWLines.Line2 = SaveLineContent(ClassicVWLine2ComboBox, ClassicVWLine2FireCheckBox);
				ui.ClassicSkinVWLines.Line3 = SaveLineContent(ClassicVWLine3ComboBox, ClassicVWLine3FireCheckBox);
				ui.ClassicSkinVWLines.Line4 = SaveLineContent(ClassicVWLine4ComboBox, ClassicVWLine4FireCheckBox);

				ui.ClassicSkinTCLines.Line1 = SaveLineContent(ClassicTCLine1ComboBox, ClassicTCLine1FireCheckBox);
				ui.ClassicSkinTCLines.Line2 = SaveLineContent(ClassicTCLine2ComboBox, ClassicTCLine2FireCheckBox);
				ui.ClassicSkinTCLines.Line3 = SaveLineContent(ClassicTCLine3ComboBox, ClassicTCLine3FireCheckBox);
				ui.ClassicSkinTCLines.Line4 = SaveLineContent(ClassicTCLine4ComboBox, ClassicTCLine4FireCheckBox);

				// General -> Layout Tab -> Circle Screen
				ui.CircleSkinVWLines.Line1 = SaveLineContent(CircleVWLine1ComboBox);
				ui.CircleSkinVWLines.Line2 = SaveLineContent(CircleVWLine2ComboBox);
				ui.CircleSkinVWLines.Line3 = SaveLineContent(CircleVWLine3ComboBox, CircleVWLine3FireCheckBox);

				ui.CircleSkinTCLines.Line1 = SaveLineContent(CircleTCLine1ComboBox);
				ui.CircleSkinTCLines.Line2 = SaveLineContent(CircleTCLine2ComboBox);
				ui.CircleSkinTCLines.Line3 = SaveLineContent(CircleTCLine3ComboBox, CircleTCLine3FireCheckBox);

				// General -> Layout Tab -> Small Screen
				ui.SmallSkinVWLines.Line1 = SaveLineContent(SmallVWLine1ComboBox, SmallVWLine1FireCheckBox);
				ui.SmallSkinVWLines.Line2 = SaveLineContent(SmallVWLine2ComboBox, SmallVWLine2FireCheckBox);

				ui.SmallSkinTCLines.Line1 = SaveLineContent(SmallTCLine1ComboBox, SmallTCLine1FireCheckBox);
				ui.SmallSkinTCLines.Line2 = SaveLineContent(SmallTCLine2ComboBox, SmallTCLine2FireCheckBox);

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
				stats.PuffsCount = (uint)PuffsUpDown.Value;
				stats.PuffsTime = (uint)(PuffsTimeUpDown.Value * 10);

				// Time sync
				stats.DateTime.Year = (ushort)now.Year;
				stats.DateTime.Month = (byte)now.Month;
				stats.DateTime.Day = (byte)now.Day;
				stats.DateTime.Hour = (byte)now.Hour;
				stats.DateTime.Minute = (byte)now.Minute;
				stats.DateTime.Second = (byte)now.Second;

				ui.PuffsTimeFormat = PuffsTimeFormatComboBox.GetSelectedItem<ArcticFoxConfiguration.PuffsTimeFormat>();
			}

			var advanced = m_configuration.Advanced;
			{
				advanced.PuffCutOff = (byte)(PuffCutOffUpDown.Value * 10);
				advanced.ShuntCorrection = (byte)ShuntCorrectionUpDown.Value;
				advanced.BatteryModel = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
				advanced.IsX32 = X32CheckBox.Checked;
				advanced.IsLightSleepMode = LightSleepCheckBox.Checked;
				advanced.ResetCountersOnStartup = ResetCountersCheckBox.Checked;
				advanced.CheckTCR = CheckTCRCheckBox.Checked;
				advanced.IsUsbCharge = UsbChargeCheckBox.Checked;
				advanced.UsbNoSleep = UsbNoSleepCheckBox.Checked;
				
				advanced.BatteryVoltageOffsets[0] = (sbyte)(Battery1OffsetUpDown.Value * 100);
				advanced.BatteryVoltageOffsets[1] = (sbyte)(Battery2OffsetUpDown.Value * 100);
				advanced.BatteryVoltageOffsets[2] = (sbyte)(Battery3OffsetUpDown.Value * 100);
			}
		}

		private ArcticFoxConfiguration.LineContent SaveLineContent(ComboBox comboBox, CheckBox checkBox = null)
		{
			var result = comboBox.GetSelectedItem<ArcticFoxConfiguration.LineContent>();
			if (checkBox != null && checkBox.Checked)
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
			var result = BinaryStructure.Read<ArcticFoxConfiguration>(m_encryption.Decode(source));
			if (existedConfiguration == null)
			{
				result.Info.MaxPower = MaxPower;
				result.Info.NumberOfBatteries = MaxBatteries;
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

			var result = ReadConfiguration(w => m_encryption.Decode(File.ReadAllBytes(fileName)));
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
					SaveWorkspace();
					var cfgCopy = BinaryStructure.Copy(m_configuration);
					{
						cfgCopy.Info.FirmwareVersion = 0;
						cfgCopy.Info.HardwareVersion = 0;
						cfgCopy.Info.MaxPower = 0;
						cfgCopy.Info.NumberOfBatteries = 0;
						cfgCopy.Info.ProductId = string.Empty;
					}
					var bytes = BinaryStructure.Write(cfgCopy);
					File.WriteAllBytes(sf.FileName, m_encryption.Encode(bytes));
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
			if (m_isWorkspaceOpen) return;

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
