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
		private readonly ToolboxConfiguration m_toolboxConfiguration;
		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };
		private readonly IEncryption m_encryption = new ArcticFoxEncryption();
		private readonly Func<BackgroundWorker, byte[]> m_deviceConfigurationProvider = worker => HidConnector.Instance.ReadConfiguration(worker);

		private bool m_isWorkspaceOpen;
		private bool m_isDeviceConnected;
		private ArcticFoxConfiguration m_deviceConfiguration;

		public ArcticFoxConfigurationWindow([NotNull] ToolboxConfiguration toolboxConfiguration)
		{
			if (toolboxConfiguration == null) throw new ArgumentNullException("toolboxConfiguration");

			m_toolboxConfiguration = toolboxConfiguration;			
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
			MainToolTip.SetToolTip(ShowLogoLabel, LocalizableStrings.ShowLogoTooltip);
			MainToolTip.SetToolTip(ShowLogoCheckBox, LocalizableStrings.ShowLogoTooltip);

			MainToolTip.SetToolTip(ShowClockLabel, LocalizableStrings.ShowClockTooltip);
			MainToolTip.SetToolTip(ShowClockCheckBox, LocalizableStrings.ShowClockTooltip);

			MainToolTip.SetToolTip(PuffCutOffLabel, LocalizableStrings.MaxPuffTimeTooltip);
			MainToolTip.SetToolTip(PuffCutOffUpDown, LocalizableStrings.MaxPuffTimeTooltip);

			MainToolTip.SetToolTip(ShuntCorrectionLabel, LocalizableStrings.ShuntCorrectionTooltip);
			MainToolTip.SetToolTip(ShuntCorrectionUpDown, LocalizableStrings.ShuntCorrectionTooltip);

			MainToolTip.SetToolTip(RtcModeLabel, LocalizableStrings.RtcModeTooltip);
			MainToolTip.SetToolTip(RtcModeComboBox, LocalizableStrings.RtcModeTooltip);

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
				ClassicTCLine1ComboBox, ClassicTCLine2ComboBox, ClassicTCLine3ComboBox, ClassicTCLine4ComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.ClassicSkinLineContentItems);
			}

			// Fill Foxy Skin ComboBoxes
			foreach (var comboBox in new[]
			{
				FoxyVWLine1ComboBox, FoxyVWLine2ComboBox, FoxyVWLine3ComboBox,
				FoxyTCLine1ComboBox, FoxyTCLine2ComboBox, FoxyTCLine3ComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.FoxySkinLineContentItems);
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
			ChargeScreenExtraСomboBox.Fill(PredefinedData.ArcticFox.ChargeScreenExtraTypes);
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
			Clicks5ComboBox.Fill(PredefinedData.ArcticFox.Click5Actions);

			// Shortcuts - In Standby
			foreach (var comboBox in new[] 
			{
				InStandbyVWFireMinusComboBox, InStandbyVWFirePlusComboBox, InStandbyVWPlusMinusComboBox,
				InStandbyTCFireMinusComboBox, InStandbyTCFirePlusComboBox, InStandbyTCPlusMinusComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.ClickActions);
			}
			// Shortcuts - In Edit
			foreach (var comboBox in new[] 
			{
				InEditVWFireMinusComboBox, InEditVWFirePlusComboBox, InEditVWPlusMinusComboBox,
				InEditTCFireMinusComboBox, InEditTCFirePlusComboBox, InEditTCPlusMinusComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.ShortcutsInEdit);
			}
			// Shortcuts - In Menu
			foreach (var comboBox in new[] 
			{
				InMenuVWFireMinusComboBox, InMenuVWFirePlusComboBox, InMenuVWPlusMinusComboBox,
				InMenuTCFireMinusComboBox, InMenuTCFirePlusComboBox, InMenuTCPlusMinusComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.ShortcutsInMenu);
			}
			// Shortcuts - In Selector
			foreach (var comboBox in new[] 
			{
				InSelectorVWFireMinusComboBox, InSelectorVWFirePlusComboBox, InSelectorVWPlusMinusComboBox,
				InSelectorTCFireMinusComboBox, InSelectorTCFirePlusComboBox, InSelectorTCPlusMinusComboBox
			})
			{
				comboBox.Fill(PredefinedData.ArcticFox.ShortcutsInSelector);
			}

			UpDownButtonsComboBox.Fill(PredefinedData.ArcticFox.UpDownButtons);

			PuffsTimeFormatComboBox.Fill(PredefinedData.ArcticFox.PuffTimeFormats);
			SmartModeComboBox.Fill(PredefinedData.ArcticFox.SmartModes);
			BatteryModelComboBox.Fill(PredefinedData.ArcticFox.GenericBattery);

			BatteryModelComboBox.SelectedValueChanged += (s, e) =>
			{
				var batteryModel = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
				BatteryEditButton.Visible = batteryModel != ArcticFoxConfiguration.BatteryModel.Generic;
			};

			RtcModeComboBox.Fill(PredefinedData.ArcticFox.RtcModes);
			DeepSleepBehaviorComboBox.Fill(PredefinedData.ArcticFox.DeepSleepModes);

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

			SmartModeComboBox.SelectedIndexChanged += (s, e) =>
			{
				var mode = SmartModeComboBox.GetSelectedItem<ArcticFoxConfiguration.SmartMode>();
				SelectedProfleComboBox.Enabled = mode == ArcticFoxConfiguration.SmartMode.Off;
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
				if (info.SettingsVersion < ArcticFoxConfiguration.SupportedSettingsVersion || info.FirmwareBuild < ArcticFoxConfiguration.MinimumSupportedBuildNumber)
				{
					return new ConfigurationReadResult(null, ReadResult.OutdatedFirmware);
				}
				if (info.SettingsVersion > ArcticFoxConfiguration.SupportedSettingsVersion)
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
			var data = BinaryStructure.WriteBinary(m_deviceConfiguration);
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
			var deviceInfo = m_deviceConfiguration.Info;
			{
				DeviceNameLabel.Text = HidDeviceInfo.Get(deviceInfo.ProductId).Name;
				FirmwareVersionTextBox.Text = (deviceInfo.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
				BuildTextBox.Text = deviceInfo.FirmwareBuild.ToString();
				HardwareVersionTextBox.Text = (deviceInfo.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);

				if (deviceInfo.DisplaySize == ArcticFoxConfiguration.DisplaySize.W96H16)
				{
					MainScreenSkinComboBox.Fill(PredefinedData.ArcticFox.MainSmallScreenSkins);
					ClockTypeLabel.Visible = ClockTypeComboBox.Visible = false;

					if (LayoutTabControl.TabCount > 1)
					{
						LayoutTabControl.TabPages.Remove(ClassicScreenTabPage);
						LayoutTabControl.TabPages.Remove(FoxyScreenTabPage);
						LayoutTabControl.TabPages.Remove(CircleScreenTabPage);
					}
				}
				else
				{
					MainScreenSkinComboBox.Fill(PredefinedData.ArcticFox.MainBigScreenSkins);
					ClockTypeLabel.Visible = ClockTypeComboBox.Visible = true;

					if (LayoutTabControl.TabCount == 1)
					{
						LayoutTabControl.TabPages.Insert(0, CircleScreenTabPage);
						LayoutTabControl.TabPages.Insert(0, FoxyScreenTabPage);
						LayoutTabControl.TabPages.Insert(0, ClassicScreenTabPage);
					}
				}

				UsbChargeLabel.Visible = UsbChargeCheckBox.Visible = deviceInfo.NumberOfBatteries > 1;
				Battery2OffsetLabel.Visible = Battery2OffsetUpDown.Visible = Battery2OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 1;
				Battery3OffsetLabel.Visible = Battery3OffsetUpDown.Visible = Battery3OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 2;
				Battery4OffsetLabel.Visible = Battery4OffsetUpDown.Visible = Battery4OffsetVoltsLabel.Visible = deviceInfo.NumberOfBatteries > 3;
			}

			var general = m_deviceConfiguration.General;
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

					tabContent.Initialize(m_deviceConfiguration, i);
					tabContent.UpdatePowerCurveNames(m_deviceConfiguration.Advanced.PowerCurves);
					tabContent.UpdateTFRNames(m_deviceConfiguration.Advanced.TFRTables);
				}

				ProfilesTabControl.SelectedIndex = Math.Max(0, Math.Min(general.SelectedProfile, ProfilesTabControl.TabCount));
				SelectedProfleComboBox.SelectItem(general.SelectedProfile);

				SmartModeComboBox.SelectItem(general.SmartMode);
				SmartRangeUpDown.SetValue(general.SmartRange);
			}

			var ui = m_deviceConfiguration.Interface;
			{
				BrightnessTrackBar.Value = ui.Brightness;
				IdleTimeUpDow.SetValue(ui.DimTimeout);
				IdleLockedTimeUpDow.SetValue(ui.DimTimeoutLocked);
				PuffScreenDelayUpDown.SetValue(ui.PuffScreenDelay / 10m);
				IsStealthModeCheckBox.Checked = ui.IsStealthMode;
				ShowChargingInStealthCheckBox.Checked = ui.ShowChargingInStealth;
				ShowScreensaverInStealthCheckBox.Checked = ui.ShowScreensaverInStealth;
				FlippedModeCheckBox.Checked = ui.IsFlipped;
				MainScreenSkinComboBox.SelectItem(ui.MainScreenSkin);
				ShowLogoCheckBox.Checked = ui.IsLogoEnabled;
				ShowLogoDelayUpDown.SetValue(ui.ShowLogoDelay);
				ShowClockCheckBox.Checked = ui.IsClockOnMainScreen;
				ShowClockDelayUpDown.SetValue(ui.ShowClockDelay);
				ClockTypeComboBox.SelectItem(ui.ClockType);
				ScreensaverTimeComboBox.SelectItem(ui.ScreensaveDuration);
				ChargeScreenComboBox.SelectItem(ui.ChargeScreenType);
				ChargeScreenExtraСomboBox.SelectItem(ui.ChargeExtraType);

				// Classic Screen
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line1, ClassicVWLine1ComboBox, ClassicVWLine1FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line2, ClassicVWLine2ComboBox, ClassicVWLine2FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line3, ClassicVWLine3ComboBox, ClassicVWLine3FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinVWLines.Line4, ClassicVWLine4ComboBox, ClassicVWLine4FireCheckBox);

				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line1, ClassicTCLine1ComboBox, ClassicTCLine1FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line2, ClassicTCLine2ComboBox, ClassicTCLine2FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line3, ClassicTCLine3ComboBox, ClassicTCLine3FireCheckBox);
				InitializeLineContentEditor(ui.ClassicSkinTCLines.Line4, ClassicTCLine4ComboBox, ClassicTCLine4FireCheckBox);

				// Foxy Screen
				InitializeLineContentEditor(ui.FoxySkinVWLines.Line1, FoxyVWLine1ComboBox, FoxyVWLine1FireCheckBox);
				InitializeLineContentEditor(ui.FoxySkinVWLines.Line2, FoxyVWLine2ComboBox, FoxyVWLine2FireCheckBox);
				InitializeLineContentEditor(ui.FoxySkinVWLines.Line3, FoxyVWLine3ComboBox, FoxyVWLine3FireCheckBox);

				InitializeLineContentEditor(ui.FoxySkinTCLines.Line1, FoxyTCLine1ComboBox, FoxyTCLine1FireCheckBox);
				InitializeLineContentEditor(ui.FoxySkinTCLines.Line2, FoxyTCLine2ComboBox, FoxyTCLine2FireCheckBox);
				InitializeLineContentEditor(ui.FoxySkinTCLines.Line3, FoxyTCLine3ComboBox, FoxyTCLine3FireCheckBox);

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

				Clicks5ComboBox.SelectItem(ui.FiveClicks);

				// Shortcuts VW
				InStandbyVWFireMinusComboBox.SelectItem(ui.ShortcutsVW[0].InStandby);
				InStandbyVWFirePlusComboBox.SelectItem(ui.ShortcutsVW[1].InStandby);
				InStandbyVWPlusMinusComboBox.SelectItem(ui.ShortcutsVW[2].InStandby);

				InMenuVWFireMinusComboBox.SelectItem(ui.ShortcutsVW[0].InMenu);
				InMenuVWFirePlusComboBox.SelectItem(ui.ShortcutsVW[1].InMenu);
				InMenuVWPlusMinusComboBox.SelectItem(ui.ShortcutsVW[2].InMenu);

				InEditVWFireMinusComboBox.SelectItem(ui.ShortcutsVW[0].InEditMain);
				InEditVWFirePlusComboBox.SelectItem(ui.ShortcutsVW[1].InEditMain);
				InEditVWPlusMinusComboBox.SelectItem(ui.ShortcutsVW[2].InEditMain);

				InSelectorVWFireMinusComboBox.SelectItem(ui.ShortcutsVW[0].InSelector);
				InSelectorVWFirePlusComboBox.SelectItem(ui.ShortcutsVW[1].InSelector);
				InSelectorVWPlusMinusComboBox.SelectItem(ui.ShortcutsVW[2].InSelector);

				// Shortcuts TC
				InStandbyTCFireMinusComboBox.SelectItem(ui.ShortcutsTC[0].InStandby);
				InStandbyTCFirePlusComboBox.SelectItem(ui.ShortcutsTC[1].InStandby);
				InStandbyTCPlusMinusComboBox.SelectItem(ui.ShortcutsTC[2].InStandby);

				InMenuTCFireMinusComboBox.SelectItem(ui.ShortcutsTC[0].InMenu);
				InMenuTCFirePlusComboBox.SelectItem(ui.ShortcutsTC[1].InMenu);
				InMenuTCPlusMinusComboBox.SelectItem(ui.ShortcutsTC[2].InMenu);

				InEditTCFireMinusComboBox.SelectItem(ui.ShortcutsTC[0].InEditMain);
				InEditTCFirePlusComboBox.SelectItem(ui.ShortcutsTC[1].InEditMain);
				InEditTCPlusMinusComboBox.SelectItem(ui.ShortcutsTC[2].InEditMain);

				InSelectorTCFireMinusComboBox.SelectItem(ui.ShortcutsTC[0].InSelector);
				InSelectorTCFirePlusComboBox.SelectItem(ui.ShortcutsTC[1].InSelector);
				InSelectorTCPlusMinusComboBox.SelectItem(ui.ShortcutsTC[2].InSelector);

				UpDownButtonsComboBox.SelectItem(ui.IsUpDownSwapped);
				WakeUpByPlusMinusCheckBox.Checked = ui.WakeUpByPlusMinus;
				Step1WCheckBox.Checked = ui.IsPowerStep1W;

				if (deviceInfo.DisplaySize == ArcticFoxConfiguration.DisplaySize.W96H16)
				{
					LayoutTabControl.SelectedTab = SmallScreenTabPage;
				}
				else
				{
					if (ui.MainScreenSkin == ArcticFoxConfiguration.Skin.Classic) LayoutTabControl.SelectedTab = ClassicScreenTabPage;
					if (ui.MainScreenSkin == ArcticFoxConfiguration.Skin.Foxy) LayoutTabControl.SelectedTab = FoxyScreenTabPage;
					if (ui.MainScreenSkin == ArcticFoxConfiguration.Skin.Circle) LayoutTabControl.SelectedTab = CircleScreenTabPage;
				}
			}

			var stats = m_deviceConfiguration.Counters;
			{
				PuffsUpDown.SetValue(stats.PuffsCount);
				PuffsTimeUpDown.SetValue(stats.PuffsTime / 10m);
				PuffsTimeFormatComboBox.SelectItem(ui.PuffsTimeFormat);
			}

			var advanced = m_deviceConfiguration.Advanced;
			{
				PuffCutOffUpDown.SetValue(advanced.PuffCutOff / 10m);
				ShuntCorrectionUpDown.SetValue(advanced.ShuntCorrection);
				RtcModeComboBox.SelectItem(advanced.RtcMode);
				DeepSleepBehaviorComboBox.SelectItem(advanced.DeepSleepMode);
				ResetCountersCheckBox.Checked = advanced.ResetCountersOnStartup;
				CheckTCRCheckBox.Checked = advanced.CheckTCR;
				UsbChargeCheckBox.Checked = advanced.IsUsbCharge;
				UsbNoSleepCheckBox.Checked = advanced.UsbNoSleep;
				
				BatteryModelComboBox.SelectItem(advanced.BatteryModel);
				UpdateDischargeProfileNames();

				Battery1OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[0] / 100m);
				Battery2OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[1] / 100m);
				Battery3OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[2] / 100m);
				Battery4OffsetUpDown.SetValue(advanced.BatteryVoltageOffsets[3] / 100m);

				PowerCurvesListView.Items.Clear();
				PowerCurvesListView.LargeImageList.Images.Clear();
				for (var i = 0; i < m_deviceConfiguration.Advanced.PowerCurves.Length; i++)
				{
					var powerCurve = m_deviceConfiguration.Advanced.PowerCurves[i];
					var bitmap = ChartPreviewService.CreatePowerCurvePreview(powerCurve, PowerCurvesListView.LargeImageList.ImageSize);
					PowerCurvesListView.LargeImageList.Images.Add(bitmap);
					PowerCurvesListView.Items.Add(new ListViewItem(powerCurve.Name, i) { Tag = i });
				}

				MaterialsListView.Items.Clear();
				MaterialsListView.LargeImageList.Images.Clear();
				for (var i = 0; i < m_deviceConfiguration.Advanced.TFRTables.Length; i++)
				{
					var tfrTable = m_deviceConfiguration.Advanced.TFRTables[i];
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

		private void InitializeLineContentEditor(ArcticFoxConfiguration.FoxyLineContent content, ComboBox comboBox, CheckBox checkBox = null)
		{
			var contentCopy = content;
			if (checkBox != null)
			{
				checkBox.Checked = contentCopy.HasFlag(ArcticFoxConfiguration.FoxyLineContent.FireTimeMask);
			}
			contentCopy &= ~ArcticFoxConfiguration.FoxyLineContent.FireTimeMask;
			comboBox.SelectItem(contentCopy);
		}

		private void SaveWorkspace()
		{
			var general = m_deviceConfiguration.General;
			{
				// Profiles Tab
				for (var i = 0; i < general.Profiles.Length; i++)
				{
					var tabContent = (ProfileTabContent)ProfilesTabControl.TabPages[i].Controls[0];
					tabContent.Save(general.Profiles[i]);
				}

				general.SelectedProfile = SelectedProfleComboBox.GetSelectedItem<byte>();
				general.SmartMode = SmartModeComboBox.GetSelectedItem<ArcticFoxConfiguration.SmartMode>();
				general.SmartRange = (byte)SmartRangeUpDown.Value;
			}

			var ui = m_deviceConfiguration.Interface;
			{
				// General -> Screen Tab
				ui.Brightness = (byte)BrightnessTrackBar.Value;
				ui.DimTimeout = (byte)IdleTimeUpDow.Value;
				ui.DimTimeoutLocked = (byte)IdleLockedTimeUpDow.Value;
				ui.PuffScreenDelay = (byte)(PuffScreenDelayUpDown.Value * 10);
				ui.IsStealthMode = IsStealthModeCheckBox.Checked;
				ui.ShowChargingInStealth = ShowChargingInStealthCheckBox.Checked;
				ui.ShowScreensaverInStealth = ShowScreensaverInStealthCheckBox.Checked;
				ui.IsFlipped = FlippedModeCheckBox.Checked;
				ui.MainScreenSkin = MainScreenSkinComboBox.GetSelectedItem<ArcticFoxConfiguration.Skin>();
				ui.IsLogoEnabled = ShowLogoCheckBox.Checked;
				ui.ShowLogoDelay = (byte)ShowLogoDelayUpDown.Value;
				ui.IsClockOnMainScreen = ShowClockCheckBox.Checked;
				ui.ShowClockDelay = (byte)ShowClockDelayUpDown.Value;
				ui.ClockType = ClockTypeComboBox.GetSelectedItem<ArcticFoxConfiguration.ClockType>();
				ui.ScreensaveDuration = ScreensaverTimeComboBox.GetSelectedItem<ArcticFoxConfiguration.ScreenProtectionTime>();
				ui.ChargeScreenType = ChargeScreenComboBox.GetSelectedItem<ArcticFoxConfiguration.ChargeScreenType>();
				ui.ChargeExtraType = ChargeScreenExtraСomboBox.GetSelectedItem<ArcticFoxConfiguration.ChargeExtraType>();

				// General -> Layout Tab -> Classic Screen
				ui.ClassicSkinVWLines.Line1 = SaveLineContent(ClassicVWLine1ComboBox, ClassicVWLine1FireCheckBox);
				ui.ClassicSkinVWLines.Line2 = SaveLineContent(ClassicVWLine2ComboBox, ClassicVWLine2FireCheckBox);
				ui.ClassicSkinVWLines.Line3 = SaveLineContent(ClassicVWLine3ComboBox, ClassicVWLine3FireCheckBox);
				ui.ClassicSkinVWLines.Line4 = SaveLineContent(ClassicVWLine4ComboBox, ClassicVWLine4FireCheckBox);

				ui.ClassicSkinTCLines.Line1 = SaveLineContent(ClassicTCLine1ComboBox, ClassicTCLine1FireCheckBox);
				ui.ClassicSkinTCLines.Line2 = SaveLineContent(ClassicTCLine2ComboBox, ClassicTCLine2FireCheckBox);
				ui.ClassicSkinTCLines.Line3 = SaveLineContent(ClassicTCLine3ComboBox, ClassicTCLine3FireCheckBox);
				ui.ClassicSkinTCLines.Line4 = SaveLineContent(ClassicTCLine4ComboBox, ClassicTCLine4FireCheckBox);

				// General -> Layout Tab -> Foxy Screen
				ui.FoxySkinVWLines.Line1 = SaveFoxyLineContent(FoxyVWLine1ComboBox, FoxyVWLine1FireCheckBox);
				ui.FoxySkinVWLines.Line2 = SaveFoxyLineContent(FoxyVWLine2ComboBox, FoxyVWLine2FireCheckBox);
				ui.FoxySkinVWLines.Line3 = SaveFoxyLineContent(FoxyVWLine3ComboBox, FoxyVWLine3FireCheckBox);

				ui.FoxySkinTCLines.Line1 = SaveFoxyLineContent(FoxyTCLine1ComboBox, FoxyTCLine1FireCheckBox);
				ui.FoxySkinTCLines.Line2 = SaveFoxyLineContent(FoxyTCLine2ComboBox, FoxyTCLine2FireCheckBox);
				ui.FoxySkinTCLines.Line3 = SaveFoxyLineContent(FoxyTCLine3ComboBox, FoxyTCLine3FireCheckBox);

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

				ui.FiveClicks = Clicks5ComboBox.GetSelectedItem<ArcticFoxConfiguration.FiveClicks>();

				// Shortcuts VW
				ui.ShortcutsVW[0].InStandby = InStandbyVWFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ShortcutsVW[1].InStandby = InStandbyVWFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ShortcutsVW[2].InStandby = InStandbyVWPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();

				ui.ShortcutsVW[0].InMenu = InMenuVWFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInMenu>();
				ui.ShortcutsVW[1].InMenu = InMenuVWFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInMenu>();
				ui.ShortcutsVW[2].InMenu = InMenuVWPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInMenu>();

				ui.ShortcutsVW[0].InEditMain = InEditVWFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInEdit>();
				ui.ShortcutsVW[1].InEditMain = InEditVWFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInEdit>();
				ui.ShortcutsVW[2].InEditMain = InEditVWPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInEdit>();

				ui.ShortcutsVW[0].InSelector = InSelectorVWFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInSelector>();
				ui.ShortcutsVW[1].InSelector = InSelectorVWFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInSelector>();
				ui.ShortcutsVW[2].InSelector = InSelectorVWPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInSelector>();

				// Shortcuts TC
				ui.ShortcutsTC[0].InStandby = InStandbyTCFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ShortcutsTC[1].InStandby = InStandbyTCFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();
				ui.ShortcutsTC[2].InStandby = InStandbyTCPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ClickAction>();

				ui.ShortcutsTC[0].InMenu = InMenuTCFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInMenu>();
				ui.ShortcutsTC[1].InMenu = InMenuTCFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInMenu>();
				ui.ShortcutsTC[2].InMenu = InMenuTCPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInMenu>();

				ui.ShortcutsTC[0].InEditMain = InEditTCFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInEdit>();
				ui.ShortcutsTC[1].InEditMain = InEditTCFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInEdit>();
				ui.ShortcutsTC[2].InEditMain = InEditTCPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInEdit>();

				ui.ShortcutsTC[0].InSelector = InSelectorTCFireMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInSelector>();
				ui.ShortcutsTC[1].InSelector = InSelectorTCFirePlusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInSelector>();
				ui.ShortcutsTC[2].InSelector = InSelectorTCPlusMinusComboBox.GetSelectedItem<ArcticFoxConfiguration.ShortcutsInSelector>();

				ui.IsUpDownSwapped = UpDownButtonsComboBox.GetSelectedItem<bool>();
				ui.WakeUpByPlusMinus = WakeUpByPlusMinusCheckBox.Checked;
				ui.IsPowerStep1W = Step1WCheckBox.Checked;
			}

			var stats = m_deviceConfiguration.Counters;
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

			var advanced = m_deviceConfiguration.Advanced;
			{
				advanced.PuffCutOff = (byte)(PuffCutOffUpDown.Value * 10);
				advanced.ShuntCorrection = (byte)ShuntCorrectionUpDown.Value;
				advanced.BatteryModel = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
				advanced.RtcMode = RtcModeComboBox.GetSelectedItem<ArcticFoxConfiguration.RtcMode>();
				advanced.DeepSleepMode = DeepSleepBehaviorComboBox.GetSelectedItem<ArcticFoxConfiguration.DeepSleepMode>();
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

		private ArcticFoxConfiguration.FoxyLineContent SaveFoxyLineContent(ComboBox comboBox, CheckBox checkBox = null)
		{
			var result = comboBox.GetSelectedItem<ArcticFoxConfiguration.FoxyLineContent>();
			if (checkBox != null && checkBox.Checked)
			{
				result |= ArcticFoxConfiguration.FoxyLineContent.FireTimeMask;
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
				m_deviceConfiguration = readResult.Configuration;
				UpdateUI(InitializeWorkspace);
			}
			catch (Exception ex)
			{
				Trace.Warn(ex);
				InfoBox.Show(GetErrorMessage("downloading settings"));
			}
		}

		internal void UpdateDischargeProfileNames()
		{
			var selectedProfile = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
			BatteryModelComboBox.BeginUpdate();
			for (var i = 0; i < m_deviceConfiguration.Advanced.CustomBatteryProfiles.Length; i++)
			{
				var profile = m_deviceConfiguration.Advanced.CustomBatteryProfiles[i];
				var index = i + 1;

				BatteryModelComboBox.Items.RemoveAt(index);
				BatteryModelComboBox.Items.Insert(index, new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>(profile.Name, (ArcticFoxConfiguration.BatteryModel)index));
			}
			BatteryModelComboBox.EndUpdate();
			BatteryModelComboBox.SelectItem(selectedProfile);
		}

		internal void UpdatePowerCurveNames()
		{
			UpdatePowerCurveLabels(m_deviceConfiguration.Advanced.PowerCurves);
			foreach (TabPage tabPage in ProfilesTabControl.TabPages)
			{
				var tabContent = tabPage.Controls[0] as ProfileTabContent;
				if (tabContent == null) continue;

				tabContent.UpdatePowerCurveNames(m_deviceConfiguration.Advanced.PowerCurves);
			}
		}

		internal void UpdatePowerCurvePreview(int powerCurveIndex)
		{
			var powerCurve = m_deviceConfiguration.Advanced.PowerCurves[powerCurveIndex];
			UpdateListViewPreview(PowerCurvesListView, powerCurveIndex, ChartPreviewService.CreatePowerCurvePreview(powerCurve, PowerCurvesListView.LargeImageList.ImageSize));
		}

		internal void UpdateTFRCurveNames()
		{
			UpdateTFRLables(m_deviceConfiguration.Advanced.TFRTables);
			foreach (TabPage tabPage in ProfilesTabControl.TabPages)
			{
				var tabContent = tabPage.Controls[0] as ProfileTabContent;
				if (tabContent == null) continue;

				tabContent.UpdateTFRNames(m_deviceConfiguration.Advanced.TFRTables);
			}
		}

		internal void UpdateTFRCurvePreview(int tfrTableIndex)
		{
			var tfrTable = m_deviceConfiguration.Advanced.TFRTables[tfrTableIndex];
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
			deviceInfo.MaxDevicePower = ArcticFoxConfiguration.MaxPower;
			deviceInfo.NumberOfBatteries = ArcticFoxConfiguration.MaxBatteries;
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
			var selectedBattery = BatteryModelComboBox.GetSelectedItem<ArcticFoxConfiguration.BatteryModel>();
			// Ignoring GEN profile.
			var customBattery = m_deviceConfiguration.Advanced.CustomBatteryProfiles[(byte)selectedBattery - 1];

			using (var editor = new DischargeProfileWindow(customBattery))
			{
				if (editor.ShowDialog() != DialogResult.OK) return;
				UpdateDischargeProfileNames();
			}
		}

		private void PowerCurvesListView_Click(object sender, EventArgs e)
		{
			var curveIndex = PowerCurvesListView.SelectedItems.Count == 0
				? null
				: PowerCurvesListView.SelectedItems[0].Tag as int?;
			if (!curveIndex.HasValue) return;

			var curve = m_deviceConfiguration.Advanced.PowerCurves[curveIndex.Value];

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

			var tfrTable = m_deviceConfiguration.Advanced.TFRTables[tfrTableIndex.Value];

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
			ReadConfigurationAndShowResult(w => PrepairConfiguration(Resources.new_configuration, m_deviceConfiguration));
		}

		private void OpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenConfigurationFile(m_deviceConfiguration);
		}

		private void SaveAsMenuItem_Click(object sender, EventArgs e)
		{
			if (m_deviceConfiguration == null) return;

			using (var sf = new SaveFileDialog { Filter = FileFilters.ArcticFoxConfigFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;

				try
				{
					SaveWorkspace();
					var cfgCopy = BinaryStructure.Copy(m_deviceConfiguration);
					{
						cfgCopy.Info.FirmwareVersion = 0;
						cfgCopy.Info.HardwareVersion = 0;
						cfgCopy.Info.MaxDevicePower = 0;
						cfgCopy.Info.NumberOfBatteries = 0;
						cfgCopy.Info.ProductId = string.Empty;
					}

					byte[] bytes;
					var isBinary = ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Alt);
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
			OpenConfigurationFile(m_deviceConfiguration);
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
				if (!m_isDeviceConnected && !onStartup && m_toolboxConfiguration.CloseArcticFoxConfigurationWhenDeviceIsDisconnected)
				{
					Close();
				}
				else
				{
					DownloadButton.Enabled = UploadButton.Enabled = ResetButton.Enabled = m_isDeviceConnected;
					StatusLabel.Text = LocalizableStrings.StatusDevice + @" " + (m_isDeviceConnected ? LocalizableStrings.StatusDeviceConnected : LocalizableStrings.StatusDeviceDisconnected);
				}
			});

			if (m_isWorkspaceOpen || !onStartup) return;
			if (!m_isDeviceConnected)
			{
				ShowWelcomeScreen(string.Format(LocalizableStrings.MessageConnectDevice, ArcticFoxConfiguration.MinimumSupportedBuildNumber));
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
				m_deviceConfiguration = readResult.Configuration;
				if (readResult.Result == ReadResult.Success)
				{
					OpenWorkspace(readResult.Configuration);
				}
				else if (readResult.Result == ReadResult.OutdatedFirmware)
				{
					ShowWelcomeScreen(string.Format(LocalizableStrings.MessageConnectDevice, ArcticFoxConfiguration.MinimumSupportedBuildNumber));
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
			m_deviceConfiguration = configuration;
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
