using NCore.UI;

namespace NToolbox.Services
{
	internal static class LocalizableStrings
	{
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

		public static string VapeModePower
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.Mode.Power", "Power"); }
		}

		public static string VapeModeTempControl
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.Profile.Mode.TempControl", "Temp. Control"); }
		}

		public static string BatteryModelGeneric
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.BatteryModel.Generic", "Generic Battery"); }
		}

		public static string BatteryModelCustom
		{
			get { return LocalizationManager.Instance.GetLocalizedString("Toolbox.ArcticFoxConfiguration.BatteryModel.Custom", "Custom"); }
		}

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
	}
}
