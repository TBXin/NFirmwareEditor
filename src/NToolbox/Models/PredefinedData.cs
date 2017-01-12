using NCore.UI;
using NToolbox.Services;

namespace NToolbox.Models
{
	internal static class PredefinedData
	{
		internal class ArcticFox
		{
			internal class Profile
			{
				public static object[] PreheatTypes
				{
					get
					{
						return new object[]
						{
							new NamedItemContainer<ArcticFoxConfiguration.PreheatType>(LocalizableStrings.PreheatTypeAbsolute, ArcticFoxConfiguration.PreheatType.Watts),
							new NamedItemContainer<ArcticFoxConfiguration.PreheatType>(LocalizableStrings.PreheatTypeRelative, ArcticFoxConfiguration.PreheatType.Percents),
							new NamedItemContainer<ArcticFoxConfiguration.PreheatType>(LocalizableStrings.PreheatTypeCurve, ArcticFoxConfiguration.PreheatType.Curve)
						};
					}
				}

				public static object[] PowerCurves
				{
					get
					{
						return new object[]
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
					}
				}

				public static object[] TemperatureTypes
				{
					get
					{
						return new object[]
						{
							new NamedItemContainer<bool>("°F", false),
							new NamedItemContainer<bool>("°C", true)
						};
					}
				}

				public static object[] Materials
				{
					get
					{
						return new object[]
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
				}
			}

			public static object[] ClassicSkinLineContentItems
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineNonDominant, ArcticFoxConfiguration.LineContent.NonDominant),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineVolts, ArcticFoxConfiguration.LineContent.Volt),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineOutputVolts, ArcticFoxConfiguration.LineContent.Vout),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineOutputAmps, ArcticFoxConfiguration.LineContent.Amps),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineResistance, ArcticFoxConfiguration.LineContent.Resistance),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLiveResistance, ArcticFoxConfiguration.LineContent.RealResistance),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLinePuffs, ArcticFoxConfiguration.LineContent.Puffs),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLinePuffsTime, ArcticFoxConfiguration.LineContent.Time),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteriesVolts, ArcticFoxConfiguration.LineContent.BatteryVolts),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineDateTime, ArcticFoxConfiguration.LineContent.DateTime),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBoardTemp, ArcticFoxConfiguration.LineContent.BoardTemperature),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastPuffTime, ArcticFoxConfiguration.LineContent.LastPuff),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastPower, ArcticFoxConfiguration.LineContent.LastPower),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastTemperature, ArcticFoxConfiguration.LineContent.LastTemperature),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryIndicator, ArcticFoxConfiguration.LineContent.Battery),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryIndicatorPercents, ArcticFoxConfiguration.LineContent.BatteryWithPercents),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryIndicatorVolts, ArcticFoxConfiguration.LineContent.BatteryWithVolts)
					};
				}
			}

			public static object[] CircleSkinLineContentItems
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineOutputVolts, ArcticFoxConfiguration.LineContent.Vout),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineOutputAmps, ArcticFoxConfiguration.LineContent.Amps),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineResistance, ArcticFoxConfiguration.LineContent.Resistance),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLiveResistance, ArcticFoxConfiguration.LineContent.RealResistance),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLinePuffs, ArcticFoxConfiguration.LineContent.Puffs),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLinePuffsTime, ArcticFoxConfiguration.LineContent.Time),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteriesVolts, ArcticFoxConfiguration.LineContent.BatteryVolts),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineDateTime, ArcticFoxConfiguration.LineContent.DateTime),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBoardTemp, ArcticFoxConfiguration.LineContent.BoardTemperature),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastPuffTime, ArcticFoxConfiguration.LineContent.LastPuff),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastPower, ArcticFoxConfiguration.LineContent.LastPower),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastTemperature, ArcticFoxConfiguration.LineContent.LastTemperature)
					};
				}
			}

			public static object[] CircleSkin3RdLineContentItems
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryIndicator, ArcticFoxConfiguration.LineContent.Battery),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryIndicatorPercents, ArcticFoxConfiguration.LineContent.BatteryWithPercents),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryIndicatorVolts, ArcticFoxConfiguration.LineContent.BatteryWithVolts)
					};
				}
			}

			public static object[] SmallScreenLineContentItems
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineOutputVolts, ArcticFoxConfiguration.LineContent.Vout),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineOutputAmps, ArcticFoxConfiguration.LineContent.Amps),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineResistance, ArcticFoxConfiguration.LineContent.Resistance),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLiveResistance, ArcticFoxConfiguration.LineContent.RealResistance),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLinePuffs, ArcticFoxConfiguration.LineContent.Puffs),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLinePuffsTime, ArcticFoxConfiguration.LineContent.Time),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteriesVolts, ArcticFoxConfiguration.LineContent.BatteryVolts),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBatteryPercents, ArcticFoxConfiguration.LineContent.BatteryPercents),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineDateTime, ArcticFoxConfiguration.LineContent.DateTime),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineBoardTemp, ArcticFoxConfiguration.LineContent.BoardTemperature),

						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastPuffTime, ArcticFoxConfiguration.LineContent.LastPuff),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastPower, ArcticFoxConfiguration.LineContent.LastPower),
						new NamedItemContainer<ArcticFoxConfiguration.LineContent>(LocalizableStrings.InfoLineLastTemperature, ArcticFoxConfiguration.LineContent.LastTemperature)
					};
				}
			}

			public static object[] MainScreenSkins
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinClassic, ArcticFoxConfiguration.Skin.Classic),
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinCircle, ArcticFoxConfiguration.Skin.Circle)
					};
				}
			}

			public static object[] ChargeScreenTypes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ChargeScreenType>(LocalizableStrings.ChargeScreenClassic, ArcticFoxConfiguration.ChargeScreenType.Classic),
						new NamedItemContainer<ArcticFoxConfiguration.ChargeScreenType>(LocalizableStrings.ChargeScreenExtended, ArcticFoxConfiguration.ChargeScreenType.Extended)
					};
				}
			}

			public static object[] ClockTypes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ClockType>(LocalizableStrings.ClockTypeAnalog, ArcticFoxConfiguration.ClockType.Analog),
						new NamedItemContainer<ArcticFoxConfiguration.ClockType>(LocalizableStrings.ClockTypeDigital, ArcticFoxConfiguration.ClockType.Digital)
					};
				}
			}

			public static object[] ScreenSaverTimes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTimeOff, ArcticFoxConfiguration.ScreenProtectionTime.Off),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime1Min, ArcticFoxConfiguration.ScreenProtectionTime.Min1),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime2Min, ArcticFoxConfiguration.ScreenProtectionTime.Min2),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime5Min, ArcticFoxConfiguration.ScreenProtectionTime.Min5),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime10Min, ArcticFoxConfiguration.ScreenProtectionTime.Min10),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime15Min, ArcticFoxConfiguration.ScreenProtectionTime.Min15),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime20Min, ArcticFoxConfiguration.ScreenProtectionTime.Min20),
						new NamedItemContainer<ArcticFoxConfiguration.ScreenProtectionTime>(LocalizableStrings.ScreenProtectionTime30Min, ArcticFoxConfiguration.ScreenProtectionTime.Min30)
					};
				}
			}

			public static object[] ClickActions
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsNone, ArcticFoxConfiguration.ClickAction.None),

						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsEdit, ArcticFoxConfiguration.ClickAction.Edit),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsMainMenu, ArcticFoxConfiguration.ClickAction.MainMenu),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsPreheatMenu, ArcticFoxConfiguration.ClickAction.Preheat),

						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsSelectProfile, ArcticFoxConfiguration.ClickAction.ProfileSelector),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsEditProfile, ArcticFoxConfiguration.ClickAction.ProfileEdit),

						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsTDom, ArcticFoxConfiguration.ClickAction.TemperatureDominant),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsShowClock, ArcticFoxConfiguration.ClickAction.MainScreenClock),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsInfoScreen, ArcticFoxConfiguration.ClickAction.InfoScreen),

						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsSmartOnOff, ArcticFoxConfiguration.ClickAction.SmartOnOff),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsLslOnOff, ArcticFoxConfiguration.ClickAction.LslOnOff),
						new NamedItemContainer<ArcticFoxConfiguration.ClickAction>(LocalizableStrings.ClickActionsOnOff, ArcticFoxConfiguration.ClickAction.OnOff)
					};
				}
			}

			public static object[] UpDownButtons
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<bool>(LocalizableStrings.UpDownButtonsNormal, false),
						new NamedItemContainer<bool>(LocalizableStrings.UpDownButtonsSwapped, true)
					};
				}
			}

			public static object[] PuffTimeFormats
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.PuffsTimeFormat>(LocalizableStrings.PuffsTimeFormatSeconds, ArcticFoxConfiguration.PuffsTimeFormat.Seconds),
						new NamedItemContainer<ArcticFoxConfiguration.PuffsTimeFormat>(LocalizableStrings.PuffsTimeFormatHhMmSs, ArcticFoxConfiguration.PuffsTimeFormat.HourMinuteSeconds)
					};
				}
			}

			public static object[] BatteryModels
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>(LocalizableStrings.BatteryModelGeneric, ArcticFoxConfiguration.BatteryModel.Generic),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Samsung 25R", ArcticFoxConfiguration.BatteryModel.Samsung25R),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Samsung 30Q", ArcticFoxConfiguration.BatteryModel.Samsung30Q),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("LG HG2", ArcticFoxConfiguration.BatteryModel.LGHG2),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("LG HE4", ArcticFoxConfiguration.BatteryModel.LGHE4),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Sony VTC4", ArcticFoxConfiguration.BatteryModel.SonyVTC4),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Sony VTC5", ArcticFoxConfiguration.BatteryModel.SonyVTC5),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>(LocalizableStrings.BatteryModelCustom, ArcticFoxConfiguration.BatteryModel.Custom)
					};
				}
			}
		}
	}
}
