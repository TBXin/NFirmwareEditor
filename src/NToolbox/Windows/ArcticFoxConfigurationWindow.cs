using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.Serialization;
using NCore.UI;
using NCore.USB;
using NToolbox.Models;
using NToolbox.Properties;
using NToolbox.Services;

namespace NToolbox.Windows
{
	internal partial class ArcticFoxConfigurationWindow : EditorDialogWindow
	{
		private const ushort MaxPower = 3000;
		private const byte MaxBatteries = 4;
		private const int MinimumSupportedBuildNumber = 170106;
		private const int SupportedSettingsVersion = 7;

		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };
		private readonly IEncryption m_encryption = new ArcticFoxEncryption();
		private readonly Func<BackgroundWorker, byte[]> m_deviceConfigurationProvider = worker => HidConnector.Instance.ReadConfiguration(worker);

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
			m_worker.RunWorkerCompleted += (s, e) => ProgressLabel.Text = LocalizableStrings.StatusOperationComplete;

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

			ProgressLabel.Text = LocalizableStrings.StatusReady;

			SmartCheckBox.CheckedChanged += (s, e) => SelectedProfleComboBox.Enabled = !SmartCheckBox.Checked;
			BrightnessTrackBar.ValueChanged += (s, e) => BrightnessPercentLabel.Text = (int)(BrightnessTrackBar.Value * 100m / 255) + @"%";

			PowerCurvesListView.LargeImageList = new ImageList { ImageSize = new Size(60, 30) };
			PowerCurvesListView.Click += PowerCurvesListView_Click;

			MaterialsListView.LargeImageList = new ImageList { ImageSize = new Size(60, 30) };
			MaterialsListView.Click += MaterialsListView_Click;

			BatteryEditButton.Click += BatteryEditButton_Click;

			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;

			InitializeComboBoxes();
			InitializeMenu();
			InitializeTooltips();

			var multiplier = ApplicationService.GetDpiMultiplier();
			ProfilesTabControl.ItemSize = new Size
			(
				(int)(ProfilesTabControl.ItemSize.Width * multiplier),
				(int)(ProfilesTabControl.ItemSize.Height * multiplier)
			);
		}

		private void InitializeMenu()
		{
			var menu = new ContextMenu(new[]
			{
				new MenuItem(LocalizableStrings.ConfigurationMenuNew, NewMenuItem_Click),
				new MenuItem(LocalizableStrings.ConfigurationMenuOpen, OpenMenuItem_Click),
				new MenuItem(LocalizableStrings.ConfigurationMenuSaveAs, SaveAsMenuItem_Click)
			});
			ConfigurationMenuButton.Click += (s, e) => menu.Show(ConfigurationMenuButton, new Point(0, ConfigurationMenuButton.Height));
		}

		private void InitializeTooltips()
		{
			MainToolTip.SetToolTip(MainScreenSkinLabel, LocalizableStrings.MainScreenSkinTooltip);
			MainToolTip.SetToolTip(MainScreenSkinComboBox, LocalizableStrings.MainScreenSkinTooltip);

			MainToolTip.SetToolTip(UseClassicMenuLabel, LocalizableStrings.UseClassicMenuTooltip);
			MainToolTip.SetToolTip(UseClassicMenuCheckBox, LocalizableStrings.UseClassicMenuTooltip);

			MainToolTip.SetToolTip(ShowLogoLabel, LocalizableStrings.ShowLogoTooltip);
			MainToolTip.SetToolTip(ShowLogoCheckBox, LocalizableStrings.ShowLogoTooltip);

			MainToolTip.SetToolTip(ShowClockLabel, LocalizableStrings.ShowClockTooltip);
			MainToolTip.SetToolTip(ShowClockCheckBox, LocalizableStrings.ShowClockTooltip);

			MainToolTip.SetToolTip(PuffCutOffLabel, LocalizableStrings.MaxPuffTimeTooltip);
			MainToolTip.SetToolTip(PuffCutOffUpDown, LocalizableStrings.MaxPuffTimeTooltip);

			MainToolTip.SetToolTip(ShuntCorrectionLabel, LocalizableStrings.ShuntCorrectionTooltip);
			MainToolTip.SetToolTip(ShuntCorrectionUpDown, LocalizableStrings.ShuntCorrectionTooltip);

			MainToolTip.SetToolTip(X32Label, LocalizableStrings.X32Tooltip);
			MainToolTip.SetToolTip(X32CheckBox, LocalizableStrings.X32Tooltip);

			MainToolTip.SetToolTip(LightSleepLabel, LocalizableStrings.LightSleepTooltip);
			MainToolTip.SetToolTip(LightSleepCheckBox, LocalizableStrings.LightSleepTooltip);

			MainToolTip.SetToolTip(ResetCountersLabel, LocalizableStrings.RcobcTooltip);
			MainToolTip.SetToolTip(ResetCountersCheckBox, LocalizableStrings.RcobcTooltip);

			MainToolTip.SetToolTip(CheckTCRLabel, LocalizableStrings.CheckTCRTooltip);
			MainToolTip.SetToolTip(CheckTCRCheckBox, LocalizableStrings.CheckTCRTooltip);

			MainToolTip.SetToolTip(UsbChargeLabel, LocalizableStrings.UsbChargeTooltip);
			MainToolTip.SetToolTip(UsbChargeCheckBox, LocalizableStrings.UsbChargeTooltip);

			MainToolTip.SetToolTip(UsbNoSleepLabel, LocalizableStrings.UsbNoSleepTooltip);
			MainToolTip.SetToolTip(UsbNoSleepCheckBox, LocalizableStrings.UsbNoSleepTooltip);
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

			MainScreenSkinComboBox.Fill(PredefinedData.ArcticFox.MainScreenSkins);
			ChargeScreenComboBox.Fill(PredefinedData.ArcticFox.ChargeScreenTypes);
			ClockTypeComboBox.Fill(PredefinedData.ArcticFox.ClockTypes);
			ScreensaverTimeComboBox.Fill(PredefinedData.ArcticFox.ScreenSaverTimes);

			foreach (var clickComboBox in new[]
			{
				ClicksVW2ComboBox, ClicksVW3ComboBox, ClicksVW4ComboBox,
				ClicksTC2ComboBox, ClicksTC3ComboBox, ClicksTC4ComboBox
			})
			{
				clickComboBox.Fill(PredefinedData.ArcticFox.ClickActions);
			}
			UpDownButtonsComboBox.Fill(PredefinedData.ArcticFox.UpDownButtons);

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
				var result = InfoBox.Show(LocalizableStrings.MessageNoCompatibleUSBDevice, MessageBoxButtons.OKCancel);
				if (result == DialogResult.Cancel) return false;
			}
			return true;
		}

		[NotNull]
		private ConfigurationReadResult ReadBinaryConfiguration([NotNull] Func<BackgroundWorker, byte[]> configurationProvider, bool useWorker = true)
		{
			if (configurationProvider == null) throw new ArgumentNullException("configurationProvider");

			try
			{
				var data = configurationProvider(useWorker ? m_worker : null);
				if (data == null) return new ConfigurationReadResult(null, ReadResult.UnableToRead);

				var info = BinaryStructure.ReadBinary<ArcticFoxConfiguration.DeviceInfo>(data);
				if (info.SettingsVersion < SupportedSettingsVersion || info.FirmwareBuild < MinimumSupportedBuildNumber)
				{
					return new ConfigurationReadResult(null, ReadResult.OutdatedFirmware);
				}
				if (info.SettingsVersion > SupportedSettingsVersion)
				{
					return new ConfigurationReadResult(null, ReadResult.OutdatedToolbox);
				}

				var configuration = BinaryStructure.ReadBinary<ArcticFoxConfiguration>(data);
				return new ConfigurationReadResult(configuration, ReadResult.Success);
			}
			catch (TimeoutException)
			{
				return new ConfigurationReadResult(null, ReadResult.UnableToRead);
			}
		}

		private void WriteConfiguration()
		{
			var data = BinaryStructure.WriteBinary(m_configuration);
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

				if (deviceInfo.DisplaySize == ArcticFoxConfiguration.DisplaySize.W96H16)
				{
					MainScreenSkinLabel.Visible = MainScreenSkinComboBox.Visible = false;
					ClockTypeLabel.Visible = ClockTypeComboBox.Visible = false;
					UseClassicMenuLabel.Visible = UseClassicMenuCheckBox.Visible = false;
				}

				Battery2OffsetLabel.Visible = Battery2OffsetUpDown.Visible = Battery2OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 1;
				Battery3OffsetLabel.Visible = Battery3OffsetUpDown.Visible = Battery3OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 2;
				Battery4OffsetLabel.Visible = Battery4OffsetUpDown.Visible = Battery4OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 3;
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
				MainScreenSkinComboBox.SelectItem(ui.MainScreenSkin);
				UseClassicMenuCheckBox.Checked = ui.IsClassicMenu;
				ShowLogoCheckBox.Checked = ui.IsLogoEnabled;
				ShowClockCheckBox.Checked = ui.IsClockOnMainScreen;
				ClockTypeComboBox.SelectItem(ui.ClockType);
				ScreensaverTimeComboBox.SelectItem(ui.ScreensaveDuration);
				ChargeScreenComboBox.SelectItem(ui.ChargeScreenType);

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

				ClicksVW2ComboBox.SelectItem(ui.ClicksVW[0]);
				ClicksVW3ComboBox.SelectItem(ui.ClicksVW[1]);
				ClicksVW4ComboBox.SelectItem(ui.ClicksVW[2]);

				ClicksTC2ComboBox.SelectItem(ui.ClicksTC[0]);
				ClicksTC3ComboBox.SelectItem(ui.ClicksTC[1]);
				ClicksTC4ComboBox.SelectItem(ui.ClicksTC[2]);

				UpDownButtonsComboBox.SelectItem(ui.IsUpDownSwapped);
				WakeUpByPlusMinusCheckBox.Checked = ui.WakeUpByPlusMinus;
				Step1WCheckBox.Checked = ui.IsPowerStep1W;

				LayoutTabControl.SelectedTab = deviceInfo.DisplaySize == ArcticFoxConfiguration.DisplaySize.W64H128
					? ui.MainScreenSkin == ArcticFoxConfiguration.Skin.Classic
						? ClassicScreenTabPage
						: CircleScreenTabPage
					: SmallScreenTabPage;
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

				Battery1OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[0] / 100m);
				Battery2OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[1] / 100m);
				Battery3OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[2] / 100m);
				Battery4OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[3] / 100m);

				PowerCurvesListView.Items.Clear();
				PowerCurvesListView.LargeImageList.Images.Clear();
				for (var i = 0; i < m_configuration.Advanced.PowerCurves.Length; i++)
				{
					var powerCurve = m_configuration.Advanced.PowerCurves[i];
					var bitmap = ChartPreviewService.CreatePowerCurvePreview(powerCurve, PowerCurvesListView.LargeImageList.ImageSize);
					PowerCurvesListView.LargeImageList.Images.Add(bitmap);
					PowerCurvesListView.Items.Add(new ListViewItem(powerCurve.Name, i) { Tag = i });
				}

				MaterialsListView.Items.Clear();
				MaterialsListView.LargeImageList.Images.Clear();
				for (var i = 0; i < m_configuration.Advanced.TFRTables.Length; i++)
				{
					var tfrTable = m_configuration.Advanced.TFRTables[i];
					var bitmap = ChartPreviewService.CreateTFRCurvePreview(tfrTable, PowerCurvesListView.LargeImageList.ImageSize);
					MaterialsListView.LargeImageList.Images.Add(bitmap);
					MaterialsListView.Items.Add(new ListViewItem("[TFR] " + tfrTable.Name, i) { Tag = i });
				}
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
				ui.MainScreenSkin = MainScreenSkinComboBox.GetSelectedItem<ArcticFoxConfiguration.Skin>();
				ui.IsClassicMenu = UseClassicMenuCheckBox.Checked;
				ui.IsLogoEnabled = ShowLogoCheckBox.Checked;
				ui.IsClockOnMainScreen = ShowClockCheckBox.Checked;
				ui.ClockType = ClockTypeComboBox.GetSelectedItem<ArcticFoxConfiguration.ClockType>();
				ui.ScreensaveDuration = ScreensaverTimeComboBox.GetSelectedItem<ArcticFoxConfiguration.ScreenProtectionTime>();
				ui.ChargeScreenType = ChargeScreenComboBox.GetSelectedItem<ArcticFoxConfiguration.ChargeScreenType>();

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
				ui.ClicksVW[0] = ClicksVW2ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ClicksVW[1] = ClicksVW3ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ClicksVW[2] = ClicksVW4ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();

				ui.ClicksTC[0] = ClicksTC2ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ClicksTC[1] = ClicksTC3ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ClicksTC[2] = ClicksTC4ComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();

				ui.IsUpDownSwapped = UpDownButtonsComboBox.GetSelectedItem<bool>();
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
				advanced.BatteryVoltageOffsets[3] = (sbyte)(Battery4OffsetUpDown.Value * 100);
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
			for (var i = 0; i < curves.Length; i++)
			{
				PowerCurvesListView.Items[i].Text = curves[i].Name;
			}
		}

		private void UpdateTFRLables(ArcticFoxConfiguration.TFRTable[] tfrTables)
		{
			for (var i = 0; i < tfrTables.Length; i++)
			{
				MaterialsListView.Items[i].Text = @"[TFR] " + tfrTables[i].Name;
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
				var readResult = ReadBinaryConfiguration(m_deviceConfigurationProvider);
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

		internal void UpdatePowerCurvePreview(int powerCurveIndex)
		{
			var powerCurve = m_configuration.Advanced.PowerCurves[powerCurveIndex];
			UpdateListViewPreview(PowerCurvesListView, powerCurveIndex, ChartPreviewService.CreatePowerCurvePreview(powerCurve, PowerCurvesListView.LargeImageList.ImageSize));
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

		internal void UpdateTFRCurvePreview(int tfrTableIndex)
		{
			var tfrTable = m_configuration.Advanced.TFRTables[tfrTableIndex];
			UpdateListViewPreview(MaterialsListView, tfrTableIndex, ChartPreviewService.CreateTFRCurvePreview(tfrTable, MaterialsListView.LargeImageList.ImageSize));
		}

		private byte[] PrepairConfiguration(byte[] source, ArcticFoxConfiguration existedConfiguration = null)
		{
			var result = BinaryStructure.ReadBinary<ArcticFoxConfiguration>(m_encryption.Decode(source));
			if (existedConfiguration == null)
			{
				SetSharedDeviceInfo(result.Info);
			}
			else
			{
				result.Info = existedConfiguration.Info;
			}
			return BinaryStructure.WriteBinary(result);
		}

		private static void SetSharedDeviceInfo(ArcticFoxConfiguration.DeviceInfo deviceInfo)
		{
			deviceInfo.MaxPower = MaxPower;
			deviceInfo.NumberOfBatteries = MaxBatteries;
			deviceInfo.DisplaySize = ArcticFoxConfiguration.DisplaySize.W64H128;
		}

		private void OpenConfigurationFile(ArcticFoxConfiguration existedConfiguration)
		{
			string fileName;
			using (var op = new OpenFileDialog { Filter = FileFilters.ArcticFoxConfigFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			try
			{
				var existedInfoBlock = existedConfiguration != null ? existedConfiguration.Info.Copy() : null;
				var result = existedConfiguration ?? BinaryStructure.ReadBinary<ArcticFoxConfiguration>(m_encryption.Decode(Resources.new_configuration));
				var serializableConfiguration = Serializer.Read<SerializableConfiguration>(new MemoryStream(m_encryption.Decode(File.ReadAllBytes(fileName))));
				if (serializableConfiguration == null)
				{
					InfoBox.Show("Most likely you are trying to open an obsolete configuration file. This operation is not supported.");
					return;
				}
				BinaryStructure.ReadFromDictionary(result, serializableConfiguration.GetDictionary());
				if (existedInfoBlock != null)
				{
					result.Info = existedInfoBlock;
				}
				else
				{
					SetSharedDeviceInfo(result.Info);
				}
				OpenWorkspace(result);
			}
			catch (Exception ex)
			{
				Trace.Info(ex, "An error occurred during reading saved configuration file.");
				InfoBox.Show("Most likely you are trying to open an obsolete configuration file. This operation is not supported.");
			}
		}

		private void BatteryEditButton_Click(object sender, EventArgs e)
		{
			using (var editor = new DischargeProfileWindow(m_configuration.Advanced.CustomBatteryProfile))
			{
				editor.ShowDialog();
			}
		}

		private void PowerCurvesListView_Click(object sender, EventArgs e)
		{
			var curveIndex = PowerCurvesListView.SelectedItems.Count == 0
				? null
				: PowerCurvesListView.SelectedItems[0].Tag as int?;
			if (!curveIndex.HasValue) return;

			var curve = m_configuration.Advanced.PowerCurves[curveIndex.Value];

			PowerCurvesListView.SelectedItems.Clear();
			using (var editor = new PowerCurveProfileWindow(curve))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;

				UpdatePowerCurveNames();
				UpdatePowerCurvePreview(curveIndex.Value);
			}
		}

		private void MaterialsListView_Click(object sender, EventArgs e)
		{
			var tfrTableIndex = MaterialsListView.SelectedItems.Count == 0
				? null
				: MaterialsListView.SelectedItems[0].Tag as int?;
			if (!tfrTableIndex.HasValue) return;

			var tfrTable = m_configuration.Advanced.TFRTables[tfrTableIndex.Value];

			MaterialsListView.SelectedItems.Clear();
			using (var editor = new TFRProfileWindow(tfrTable))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;

				UpdateTFRCurveNames();
				UpdateTFRCurvePreview(tfrTableIndex.Value);
			}
		}

		private void UpdateListViewPreview(ListView listview, int imageIndex, Image newPreview)
		{
			if (listview.LargeImageList == null) return;
			if (listview.LargeImageList.Images.Count < imageIndex) return;

			var prevImage = listview.LargeImageList.Images[imageIndex];
			listview.LargeImageList.Images[imageIndex] = newPreview;

			prevImage.Dispose();
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

			var isBinary = ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Alt);
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

					byte[] bytes;
					if (isBinary)
					{
						bytes = BinaryStructure.WriteBinary(cfgCopy);
					}
					else
					{
						using (var ms = new MemoryStream())
						{
							Serializer.Write(new SerializableConfiguration(BinaryStructure.WriteToDictionary(cfgCopy)), ms);
							bytes = ms.ToArray();
						}
					}
					File.WriteAllBytes(sf.FileName, m_encryption.Encode(bytes));
				}
				catch (Exception ex)
				{
					Trace.ErrorException("An error occurred during save ArcticFox configuration.", ex);
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
				StatusLabel.Text = LocalizableStrings.StatusDevice + @" " + (m_isDeviceConnected ? LocalizableStrings.StatusDeviceConnected : LocalizableStrings.StatusDeviceDisconnected);
			});

			if (m_isWorkspaceOpen || !onStartup) return;
			if (!m_isDeviceConnected)
			{
				ShowWelcomeScreen(string.Format(LocalizableStrings.MessageConnectDevice, MinimumSupportedBuildNumber));
				return;
			}
			ReadConfigurationAndShowResult(m_deviceConfigurationProvider);
		}

		private void ReadConfigurationAndShowResult(Func<BackgroundWorker, byte[]> configurationProvider)
		{
			ShowWelcomeScreen(LocalizableStrings.MessageDownloadingSettings);
			try
			{
				var readResult = ReadBinaryConfiguration(configurationProvider, false);
				m_configuration = readResult.Configuration;
				if (readResult.Result == ReadResult.Success)
				{
					OpenWorkspace(readResult.Configuration);
				}
				else if (readResult.Result == ReadResult.OutdatedFirmware)
				{
					ShowWelcomeScreen(string.Format(LocalizableStrings.MessageConnectDevice, MinimumSupportedBuildNumber));
				}
				else if (readResult.Result == ReadResult.OutdatedToolbox)
				{
					ShowWelcomeScreen(LocalizableStrings.MessageOutdatedToolbox);
				}
				else if (readResult.Result == ReadResult.UnableToRead)
				{
					ShowWelcomeScreen(LocalizableStrings.MessageUnableToReadData);
				}
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
				ShowWelcomeScreen(LocalizableStrings.MessageUnableToReadData);
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
