using NCore.UI;

namespace NToolbox.Services
{
	internal static class LocalizableStrings
	{
		public static string Second
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.Shared.SecondString", "second"); }
		}

		public static string Seconds
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.Shared.SecondsString", "seconds"); }
		}

		public static string MessageConnectDevice
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Message.ConnectDevice", "Connect device with\n\nArcticFox\n[{0}]\n\nfirmware or newer"); }
		}

		public static string MessageDownloadingSettings
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Message.DownloadingSettings", "Downloading settings..."); }
		}

		public static string MessageOutdatedToolbox
		{
			get
			{
				return LocalizationManager.Instance.GetLocalizedString
				(
					"Toolbox.ArcticFoxConfiguration.Message.OutdatedToolbox",
					"NFE Toolbox is outdated.\n\nTo continue, please download\n\nlatest available release."
				);
			}
		}

		public static string MessageNoCompatibleUSBDevice
		{
			get
			{
				return LocalizationManager.Instance.GetLocalizedString
				(
					"Toolbox.ArcticFoxConfiguration.Message.NoCompatibleUSBDevice",
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose."
				);
			}
		}

		public static string MessageUnableToReadData
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Message.UnableToReadData", "Unable to download device settings. Reconnect your device."); }
		}

		public static string StatusDevice
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Status.Device", "Device is"); }
		}

		public static string StatusDeviceConnected
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Status.Connected", "connected"); }
		}

		public static string StatusDeviceDisconnected
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Status.Disconnected", "disconnected"); }
		}

		public static string StatusReady
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Status.Ready", "Ready"); }
		}

		public static string StatusOperationComplete
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Status.OperationCompleted", "Operation completed"); }
		}

		public static string WattsLabel
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.WattsLabel", "W"); }
		}

		public static string PreheatCurveLabel
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.PreheatCurveLabel", "Preheat Curve:"); }
		}

		public static string PreheatPowerLabel
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.PreheatPowerLabel", "Preheat Power:"); }
		}

		public static string PreheatTypeAbsolute
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.PreheatType.Absolute", "Absolute (W)"); }
		}

		public static string PreheatTypeRelative
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.PreheatType.Relative", "Relative (%)"); }
		}

		public static string PreheatTypeCurve
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.PreheatType.Curve", "Curve"); }
		}

		#region ConfigurationMenu
		public static string ConfigurationMenuNew
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ConfigurationMenu.New", "New"); }
		}

		public static string ConfigurationMenuOpen
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ConfigurationMenu.Open", "Open"); }
		}

		public static string ConfigurationMenuSaveAs
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ConfigurationMenu.SaveAs", "Save As"); }
		}
		#endregion

		#region VapeMode
		public static string VapeModePower
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.Mode.Power", "Power"); }
		}

		public static string VapeModeTempControl
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.Mode.TempControl", "Temp. Control"); }
		}
		#endregion

		#region BatteryModels
		public static string BatteryModelGeneric
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.BatteryModel.Generic", "Generic Battery"); }
		}

		public static string BatteryModelCustom
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.BatteryModel.Custom", "Custom"); }
		}
		#endregion

		#region InfoLines
		public static string InfoLineNonDominant
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.NonDominant", "Non dominant (Pwr / Temp)"); }
		}

		public static string InfoLineVolts
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.Volts", "Volts"); }
		}

		public static string InfoLineOutputVolts
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.OutputVolts", "Output Volts"); }
		}

		public static string InfoLineOutputAmps
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.OutputAmps", "Output Amps"); }
		}

		public static string InfoLineResistance
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.Resistance", "Resistance"); }
		}

		public static string InfoLineLiveResistance
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.LiveResistance", "Live Resistance"); }
		}

		public static string InfoLinePuffs
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.LinePuffs", "Puffs"); }
		}

		public static string InfoLinePuffsTime
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.LinePuffsTime", "Puffs Time"); }
		}

		public static string InfoLineBatteriesVolts
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.BatteriesVolts", "Battery(s) Volts"); }
		}

		public static string InfoLineDateTime
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.DateTime", "Date/Time"); }
		}

		public static string InfoLineBoardTemp
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.BoardTemp", "Board Temperature"); }
		}

		public static string InfoLineLastPuffTime
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.LastPuffTime", "Last Puff Time"); }
		}

		public static string InfoLineLastPower
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.LastPower", "Last Power"); }
		}

		public static string InfoLineLastTemperature
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.LastTemperature", "Last Temperature"); }
		}

		public static string InfoLineBatteryIndicator
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.BatteryIndicator", "Battery"); }
		}

		public static string InfoLineBatteryIndicatorPercents
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.BatteryIndicatorPercents", "Battery + %"); }
		}

		public static string InfoLineBatteryIndicatorVolts
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.BatteryIndicatorVolts", "Battery + V"); }
		}

		public static string InfoLineBatteryPercents
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.InfoLines.BatteryPercents", "Battery %"); }
		}

		public static string SkinClassic
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Skin.Classic", "Classic"); }
		}

		public static string SkinCircle
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Skin.Circle", "Circle"); }
		}

		public static string ChargeScreenClassic
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ChargeScreen.Classic", "Classic"); }
		}

		public static string ChargeScreenExtended
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ChargeScreen.Extended", "Extended"); }
		}

		public static string ClockTypeAnalog
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClockType.Analog", "Analog"); }
		}

		public static string ClockTypeDigital
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClockType.Digital", "Digital"); }
		}
		#endregion

		#region ScreenProtectionTime
		public static string ScreenProtectionTimeOff
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.Off", "Off"); }
		}

		public static string ScreenProtectionTime1Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.1Min", "1 Min"); }
		}

		public static string ScreenProtectionTime2Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.2Min", "2 Min"); }
		}

		public static string ScreenProtectionTime5Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.5Min", "5 Min"); }
		}

		public static string ScreenProtectionTime10Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.10Min", "10 Min"); }
		}

		public static string ScreenProtectionTime15Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.15Min", "15 Min"); }
		}

		public static string ScreenProtectionTime20Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.20Min", "20 Min"); }
		}

		public static string ScreenProtectionTime30Min
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ScreenProtectionTime.30Min", "30 Min"); }
		}
		#endregion

		#region ClickActions
		public static string ClickActionsNone
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.None", "None"); }
		}

		public static string ClickActionsEdit
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.Edit", "Edit"); }
		}

		public static string ClickActionsMainMenu
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.MainMenu", "Main Menu"); }
		}

		public static string ClickActionsPreheatMenu
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.PreheatMenu", "Preheat Menu"); }
		}

		public static string ClickActionsSelectProfile
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.SelectProfile", "Select Profile"); }
		}

		public static string ClickActionsEditProfile
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.Edit Profile", "Edit Profile"); }
		}

		public static string ClickActionsTDom
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.TDom", "TDom"); }
		}

		public static string ClickActionsShowClock
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.ShowClock", "Show Clock"); }
		}

		public static string ClickActionsInfoScreen
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.InfoScreen", "Info Screen"); }
		}

		public static string ClickActionsSmartOnOff
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.SmartOnOff", "Smart On / Off"); }
		}

		public static string ClickActionsLslOnOff
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.LLSOnOf", "LSL On / Off"); }
		}

		public static string ClickActionsOnOff
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.ClickActions.OnOff", "On / Off"); }
		}
		#endregion

		#region UpDownButtons
		public static string UpDownButtonsNormal
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.UpDownButtons.Normal", "Normal"); }
		}

		public static string UpDownButtonsSwapped
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.UpDownButtons.Swapped", "Swapped"); }
		}
		#endregion

		#region PuffsTimeFormat
		public static string PuffsTimeFormatSeconds
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.PuffsTimeFormat.Seconds", "Seconds"); }
		}

		public static string PuffsTimeFormatHhMmSs
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.PuffsTimeFormat.HHMMSS", "HH:MM:SS"); }
		}
		#endregion

		#region DeviceMonitorPause
		public static string DeviceMonitorRecord
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.DeviceMonitor.RecordButton", "Record..."); }
		}

		public static string DeviceMonitorStopRecording
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.DeviceMonitor.RecordStopButton", "Stop Recording"); }
		}

		public static string DeviceMonitorPauseButton
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.DeviceMonitor.PauseButton", "Pause"); }
		}

		public static string DeviceMonitorPauseResumeButton
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.DeviceMonitor.PauseResumeButton", "Resume"); }
		}
		#endregion

		#region BroadcastStartStop
		public static string ScreenshooterStartBroadcast
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.Screenshooter.StartBroadcastButton", "Start broadcast"); }
		}

		public static string ScreenshooterStopBroadcast
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.Screenshooter.StopBroadcastButton", "Stop broadcast"); }
		}
		#endregion

		#region FirmwareUpdater
		public static string FirmwareUpdaterDeviceIsReady
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.DeviceIsReady", "Device is ready."); }
		}

		public static string FirmwareUpdaterWaitingForDevice
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.WaitingForDevice", "Waiting for device..."); }
		}

		public static string FirmwareUpdaterWaitingForDeviceAfterReset
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.WaitingForDeviceAfterReset", "Waiting for device after reset..."); }
		}

		public static string FirmwareUpdaterReadingDataflash
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.ReadingDataflash", "Reading dataflash..."); }
		}

		public static string FirmwareUpdaterWritingDataflash
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.WritingDataflash", "Writing dataflash..."); }
		}

		public static string FirmwareUpdaterRestartingDevice
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.RestartingDevice", "Restarting device..."); }
		}

		public static string FirmwareUpdaterUploadingFirmware
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.UploadingFirmware", "Uploading firmware..."); }
		}

		public static string FirmwareUpdaterDataflashReadAndSave
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.DataflashReadAndSave", "Dataflash was successfully read and saved to the file."); }
		}

		public static string FirmwareUpdaterDataflashWritten
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.DataflashWritten", "Dataflash was successfully written."); }
		}

		public static string FirmwareUpdaterDataflashReseted
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.FirmwareUpdater.Messages.DataflashReseted", "Dataflash has been reseted."); }
		}
		#endregion

		#region Tooltips
		public static string MainScreenSkinTooltip
		{
			get
			{
				return LocalizationManager.Instance.GetLocalizedString
				(
					"Toolbox.ArcticFoxConfiguration.Tooltips.MainScreenSkin",
					"Change main screen style. Applicable only for devices with big screen (64x128)."
				);
			}
		}

		public static string UseClassicMenuTooltip
		{
			get
			{
				return LocalizationManager.Instance.GetLocalizedString
				(
					"Toolbox.ArcticFoxConfiguration.Tooltips.UseClassicMenu",
					"Allows to switch profiles in the classical way, like old modes: Power, Ni, Ti, etc."
				);
			}
		}

		public static string ShowLogoTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.ShowLogo", "Show the logo on the main screen."); }
		}

		public static string ShowClockTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.ShowClock", "Show the clock on the main screen."); }
		}

		public static string MaxPuffTimeTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.MaxPuffTime", "Maximum puff time correction."); }
		}

		public static string ShuntCorrectionTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.ShuntCorrection", "Ohm-meter correction."); }
		}

		public static string X32Tooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.X32", "Enables or disables usage of the X32 crystal of the PCB."); }
		}

		public static string LightSleepTooltip
		{
			get
			{
				return LocalizationManager.Instance.GetLocalizedString
				(
					"Toolbox.ArcticFoxConfiguration.Tooltips.LightSleep",
					"Light Sleep Mode, for devices without secondary oscillator keep RTC accurate. It takes some more energy in standby mode, so user will be warned by ! sign right of battery indicator."
				);
			}
		}

		public static string RcobcTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.Rcobc", "Reset Counters on Battery Change, clear vaping statistics."); }
		}

		public static string CheckTCRTooltip
		{
			get
			{
				return LocalizationManager.Instance.GetLocalizedString
				(
					"Toolbox.ArcticFoxConfiguration.Tooltips.CheckTCR",
					"Check coil material TCR, switching this option to off can eliminate TCR Error on heavy coils."
				);
			}
		}

		public static string UsbChargeTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.UsbCharge", "Enables or disables charging via USB."); }
		}

		public static string UsbNoSleepTooltip
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Tooltips.UsbNoSleep", "Do not enter deep sleep mode while connected to USB."); }
		}
		#endregion

		#region MainWindowContextMenu
		public static string TrayShowToolbox
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.Tray.ShowToolbox", "Show NFE Toolbox window"); }
		}

		public static string TrayArcticFoxConfiguration
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.ArcticFoxConfigurationButton", "ArcticFox Configuration"); }
		}

		public static string TrayMyEvicConfiguration
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.MyEvicConfigurationButton", "myEvic Configuration"); }
		}

		public static string TrayDeviceMonitor
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.DeviceMonitorButton", "Device Monitor"); }
		}

		public static string TrayScreenshooter
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.ScreenshooterButton", "Screenshooter"); }
		}

		public static string TrayFirmwareUpdater
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.FirmwareUpdaterButton", "Firmware Updater"); }
		}

		public static string TrayArcticFoxConfigurationAutostart
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.Tray.ArcticFoxConfigurationWhenDeviceConnected", "Open ArcticFox Configuration when device is connected"); }
		}

		public static string TrayTimeSync
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.Tray.TimeSync", "Synchronize time when device is connected"); }
		}

		public static string TrayToolboxAutostart
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.Tray.StartWithWindows", "Run NFE Toolbox when Windows starts"); }
		}

		public static string TrayExit
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.MainWindow.Tray.Exit", "Exit"); }
		}
		#endregion
	}
}
