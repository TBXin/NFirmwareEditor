using NCore.UI;

namespace NToolbox.Models
{
	internal static class PredefinedData
	{
		internal class ArcticFox
		{
			internal class Profile
			{
				public static readonly object[] PreheatTypes =
				{
					new NamedItemContainer<ArcticFoxConfiguration.PreheatType>("Absolute (W)", ArcticFoxConfiguration.PreheatType.Watts),
					new NamedItemContainer<ArcticFoxConfiguration.PreheatType>("Relative (%)", ArcticFoxConfiguration.PreheatType.Percents),
					new NamedItemContainer<ArcticFoxConfiguration.PreheatType>("Curve", ArcticFoxConfiguration.PreheatType.Curve)
				};

				public static readonly object[] PowerCurves =
				{
					new NamedItemContainer<byte>("Curve 1", 0),
					new NamedItemContainer<byte>("Curve 2", 1),
					new NamedItemContainer<byte>("Curve 3", 2),
					new NamedItemContainer<byte>("Curve 4", 3),
					new NamedItemContainer<byte>("Curve 5", 4),
					new NamedItemContainer<byte>("Curve 6", 5),
					new NamedItemContainer<byte>("Curve 7", 6),
					new NamedItemContainer<byte>("Curve 8", 7)
				};

				public static readonly object[] TemperatureTypes =
				{
					new NamedItemContainer<bool>("°F", false),
					new NamedItemContainer<bool>("°C", true)
				};

				public static readonly object[] Materials =
				{
					new NamedItemContainer<ArcticFoxConfiguration.Material>("Nickel 200", ArcticFoxConfiguration.Material.Nickel),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("Titanium 1", ArcticFoxConfiguration.Material.Titanium),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("SS 316", ArcticFoxConfiguration.Material.StainlessSteel),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TCR", ArcticFoxConfiguration.Material.TCR),

					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR1", ArcticFoxConfiguration.Material.TFR1),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR2", ArcticFoxConfiguration.Material.TFR2),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR3", ArcticFoxConfiguration.Material.TFR3),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR4", ArcticFoxConfiguration.Material.TFR4),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR5", ArcticFoxConfiguration.Material.TFR5),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR6", ArcticFoxConfiguration.Material.TFR6),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR7", ArcticFoxConfiguration.Material.TFR7),
					new NamedItemContainer<ArcticFoxConfiguration.Material>("TFR8", ArcticFoxConfiguration.Material.TFR8)
				};
			}

			public static readonly object[] ClassicSkinLineContentItems =
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

			public static readonly object[] CircleSkinLineContentItems =
			{
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
			};

			public static readonly object[] CircleSkin3RdLineContentItems =
			{
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery", ArcticFoxConfiguration.LineContent.Battery),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery + %", ArcticFoxConfiguration.LineContent.BatteryWithPercents),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery + V", ArcticFoxConfiguration.LineContent.BatteryWithVolts)
			};

			public static readonly object[] SmallScreenLineContentItems =
			{
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Output Volts", ArcticFoxConfiguration.LineContent.Vout),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Output Amps", ArcticFoxConfiguration.LineContent.Amps),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Resistance", ArcticFoxConfiguration.LineContent.Resistance),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Live Resistance", ArcticFoxConfiguration.LineContent.RealResistance),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Puffs", ArcticFoxConfiguration.LineContent.Puffs),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Puffs Time", ArcticFoxConfiguration.LineContent.Time),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery Volts", ArcticFoxConfiguration.LineContent.BatteryVolts),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Battery %", ArcticFoxConfiguration.LineContent.BatteryPercents),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Date/Time", ArcticFoxConfiguration.LineContent.DateTime),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Board Temperature", ArcticFoxConfiguration.LineContent.BoardTemperature),

				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Last Puff Time", ArcticFoxConfiguration.LineContent.LastPuff),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Last Power", ArcticFoxConfiguration.LineContent.LastPower),
				new NamedItemContainer<ArcticFoxConfiguration.LineContent>("Last Temperature", ArcticFoxConfiguration.LineContent.LastTemperature),
			};

			public static readonly object[] MainScreenSkins =
			{
				new NamedItemContainer<ArcticFoxConfiguration.Skin>("Classic", ArcticFoxConfiguration.Skin.Classic),
				new NamedItemContainer<ArcticFoxConfiguration.Skin>("Circle", ArcticFoxConfiguration.Skin.Circle)
			};

			public static readonly object[] ChargeScreenTypes =
			{
				new NamedItemContainer<ArcticFoxConfiguration.ChargeScreenType>("Classic", ArcticFoxConfiguration.ChargeScreenType.Classic),
				new NamedItemContainer<ArcticFoxConfiguration.ChargeScreenType>("Extended", ArcticFoxConfiguration.ChargeScreenType.Extended)
			};

			public static readonly object[] ClockTypes =
			{
				new NamedItemContainer<ArcticFoxConfiguration.ClockType>("Analog", ArcticFoxConfiguration.ClockType.Analog),
				new NamedItemContainer<ArcticFoxConfiguration.ClockType>("Digital", ArcticFoxConfiguration.ClockType.Digital)
			};

			public static readonly object[] ScreenSaverTimes =
			{
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("Off", ArcticFoxConfiguration.ScreenProtectionTime.Off),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("1 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min1),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("2 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min2),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("5 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min5),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("10 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min10),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("15 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min15),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("20 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min20),
				new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>("30 Min", ArcticFoxConfiguration.ScreenProtectionTime.Min30)
			};

			public static readonly object[] ClickActions =
			{
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("None", ArcticFoxConfiguration.ClickAction.None),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Edit", ArcticFoxConfiguration.ClickAction.Edit),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Main Menu", ArcticFoxConfiguration.ClickAction.MainMenu),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Preheat Menu", ArcticFoxConfiguration.ClickAction.Preheat),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Select Profile", ArcticFoxConfiguration.ClickAction.ProfileSelector),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Edit Profile", ArcticFoxConfiguration.ClickAction.ProfileEdit),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("TDom", ArcticFoxConfiguration.ClickAction.TemperatureDominant),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Show Clock", ArcticFoxConfiguration.ClickAction.MainScreenClock),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Info Screen", ArcticFoxConfiguration.ClickAction.InfoScreen),

				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("Smart On / Off", ArcticFoxConfiguration.ClickAction.SmartOnOff),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("LSL On / Off", ArcticFoxConfiguration.ClickAction.LslOnOff),
				new NamedItemContainer<ArcticFoxConfiguration.ClickAction>("On / Off", ArcticFoxConfiguration.ClickAction.OnOff)
			};

			public static readonly object[] PuffTimeFormats =
			{
				new NamedItemContainer<ArcticFoxConfiguration.PuffsTimeFormat>("Seconds", ArcticFoxConfiguration.PuffsTimeFormat.Seconds),
				new NamedItemContainer<ArcticFoxConfiguration.PuffsTimeFormat>("HH:MM:SS", ArcticFoxConfiguration.PuffsTimeFormat.HourMinuteSeconds)
			};

			public static readonly object[] BatteryModels =
			{
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Generic Battery", ArcticFoxConfiguration.BatteryModel.Generic),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Samsung 25R", ArcticFoxConfiguration.BatteryModel.Samsung25R),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Samsung 30Q", ArcticFoxConfiguration.BatteryModel.Samsung30Q),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("LG HG2", ArcticFoxConfiguration.BatteryModel.LGHG2),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("LG HE4", ArcticFoxConfiguration.BatteryModel.LGHE4),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Sony VTC4", ArcticFoxConfiguration.BatteryModel.SonyVTC4),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Sony VTC5", ArcticFoxConfiguration.BatteryModel.SonyVTC5),
				new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Custom", ArcticFoxConfiguration.BatteryModel.Custom)
			};
		}
	}
}
