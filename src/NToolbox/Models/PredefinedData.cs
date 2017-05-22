using System.Windows.Forms;
using NCore;
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

			public static object[] FoxySkinLineContentItems
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineOutputVolts, ArcticFoxConfiguration.FoxyLineContent.Vout),
						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineOutputAmps, ArcticFoxConfiguration.FoxyLineContent.Amps),

						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineLiveResistance, ArcticFoxConfiguration.FoxyLineContent.RealResistance),

						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLinePuffs, ArcticFoxConfiguration.FoxyLineContent.Puffs),
						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLinePuffsTime, ArcticFoxConfiguration.FoxyLineContent.Time),
						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineBatteriesVolts, ArcticFoxConfiguration.FoxyLineContent.BatteryVolts),

						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineDateTime, ArcticFoxConfiguration.FoxyLineContent.DateTime),

						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineLastPuffTime, ArcticFoxConfiguration.FoxyLineContent.LastPuff),
						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineLastPower, ArcticFoxConfiguration.FoxyLineContent.LastPower),
						new NamedItemContainer<ArcticFoxConfiguration.FoxyLineContent>(LocalizableStrings.InfoLineLastTemperature, ArcticFoxConfiguration.FoxyLineContent.LastTemperature)
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

			public static object[] MainBigScreenSkins
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinClassic, ArcticFoxConfiguration.Skin.Classic),
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinFoxy, ArcticFoxConfiguration.Skin.Foxy),
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinCircle, ArcticFoxConfiguration.Skin.Circle)
					};
				}
			}

			public static object[] MainSmallScreenSkins
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinClassic, ArcticFoxConfiguration.Skin.Classic),
						new NamedItemContainer<ArcticFoxConfiguration.Skin>(LocalizableStrings.SkinLite, ArcticFoxConfiguration.Skin.Circle)
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

			public static object[] ChargeScreenExtraTypes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ChargeExtraType>(LocalizableStrings.ChargeScreenExtraNone, ArcticFoxConfiguration.ChargeExtraType.None),
						new NamedItemContainer<ArcticFoxConfiguration.ChargeExtraType>(LocalizableStrings.ChargeScreenExtraAnalogClock, ArcticFoxConfiguration.ChargeExtraType.AnalogClock),
						new NamedItemContainer<ArcticFoxConfiguration.ChargeExtraType>(LocalizableStrings.ChargeScreenExtraDigitalClock, ArcticFoxConfiguration.ChargeExtraType.DigitalClock),
						new NamedItemContainer<ArcticFoxConfiguration.ChargeExtraType>(LocalizableStrings.ChargeScreenExtraLogo, ArcticFoxConfiguration.ChargeExtraType.Logo)
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

			public static ToolStripItem[] ClickActions
			{
				get
				{
					return new ToolStripItem[]
					{
						new ToolStripMenuItem(LocalizableStrings.ClickActionsNone) { Tag = ArcticFoxConfiguration.ClickAction.None },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsMainMenu) { Tag = ArcticFoxConfiguration.ClickAction.MainMenu },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsEdit) { Tag = ArcticFoxConfiguration.ClickAction.Edit },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsPreheatMenu) { Tag = ArcticFoxConfiguration.ClickAction.PreheatEdit },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsEditProfile) { Tag = ArcticFoxConfiguration.ClickAction.ProfileEdit },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsSelectProfile) { Tag = ArcticFoxConfiguration.ClickAction.ProfileSelector },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsResetCounters) { Tag = ArcticFoxConfiguration.ClickAction.ResetCounters },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsTDom) { Tag = ArcticFoxConfiguration.ClickAction.TemperatureDominant },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsResistanceLockUnlock) { Tag = ArcticFoxConfiguration.ClickAction.LockResistance },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsReReadResistanceAndSaveToProfile) { Tag = ArcticFoxConfiguration.ClickAction.ReReadResistanceAndSaveToProfile },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsReReadResistanceAndSmart) { Tag = ArcticFoxConfiguration.ClickAction.ReReadResistanceAndSmart },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsShowClock) { Tag = ArcticFoxConfiguration.ClickAction.MainScreenClock },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsInfoScreen) { Tag = ArcticFoxConfiguration.ClickAction.InfoScreen },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsPowerBank) { Tag = ArcticFoxConfiguration.ClickAction.PowerBank },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsStealthOnOff) { Tag = ArcticFoxConfiguration.ClickAction.StealthOnOff },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsSmartOnOff) { Tag = ArcticFoxConfiguration.ClickAction.SmartOnOff },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsLslOnOff) { Tag = ArcticFoxConfiguration.ClickAction.LslOnOff },
						new ToolStripSeparator(),
						new ToolStripMenuItem(LocalizableStrings.ClickActionsKeyLockUnlock) { Tag = ArcticFoxConfiguration.ClickAction.KeyLock },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsDeviceLockUnlock) { Tag = ArcticFoxConfiguration.ClickAction.DeviceLock },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsDeviceOnOff) { Tag = ArcticFoxConfiguration.ClickAction.OnOff }
					};
				}
			}

			public static ToolStripItem[] Click5Actions
			{
				get
				{
					return new ToolStripItem[]
					{
						new ToolStripMenuItem(LocalizableStrings.ClickActionsDeviceOnOff) { Tag = ArcticFoxConfiguration.FiveClicks.OnOff },
						new ToolStripMenuItem(LocalizableStrings.ClickActionsDeviceLockUnlock) { Tag = ArcticFoxConfiguration.FiveClicks.LockUnlock }
					};
				}
			}

			public static object[] ShortcutsInEdit
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInEdit>(LocalizableStrings.ClickActionsNone, ArcticFoxConfiguration.ShortcutsInEdit.None),
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInEdit>(LocalizableStrings.ClickActionsResetCounters, ArcticFoxConfiguration.ShortcutsInEdit.ResetCounters)
					};
				}
			}

			public static object[] ShortcutsInSelector
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInSelector>(LocalizableStrings.ClickActionsNone, ArcticFoxConfiguration.ShortcutsInSelector.None),
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInSelector>(LocalizableStrings.ClickActionsResestSavedResistance, ArcticFoxConfiguration.ShortcutsInSelector.ResetResistance)
					};
				}
			}

			public static object[] ShortcutsInMenu
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInMenu>(LocalizableStrings.ClickActionsNone, ArcticFoxConfiguration.ShortcutsInMenu.None),
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInMenu>(LocalizableStrings.ClickActionsMenuBack, ArcticFoxConfiguration.ShortcutsInMenu.Back),
						new NamedItemContainer<ArcticFoxConfiguration.ShortcutsInMenu>(LocalizableStrings.ClickActionsMenuExit, ArcticFoxConfiguration.ShortcutsInMenu.Exit)
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

			public static object[] SmartModes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.SmartMode>(LocalizableStrings.SmartModeOff, ArcticFoxConfiguration.SmartMode.Off),
						new NamedItemContainer<ArcticFoxConfiguration.SmartMode>(LocalizableStrings.SmartModeOn, ArcticFoxConfiguration.SmartMode.On),
						new NamedItemContainer<ArcticFoxConfiguration.SmartMode>(LocalizableStrings.SmartModeLazy, ArcticFoxConfiguration.SmartMode.Lazy)
					};
				}
			}

			public static object[] RtcModes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.RtcMode>("LXT", ArcticFoxConfiguration.RtcMode.Lxt),
						new NamedItemContainer<ArcticFoxConfiguration.RtcMode>("LIRC", ArcticFoxConfiguration.RtcMode.Lirc),
						new NamedItemContainer<ArcticFoxConfiguration.RtcMode>("LSL", ArcticFoxConfiguration.RtcMode.Lsl)
					};
				}
			}

			public static object[] DeepSleepModes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.DeepSleepMode>(LocalizableStrings.DeepSleepModeStandart, ArcticFoxConfiguration.DeepSleepMode.Standart),
						new NamedItemContainer<ArcticFoxConfiguration.DeepSleepMode>(LocalizableStrings.DeepSleepModeLockDevice, ArcticFoxConfiguration.DeepSleepMode.DeviceLock),
						new NamedItemContainer<ArcticFoxConfiguration.DeepSleepMode>(LocalizableStrings.DeepSleepModeTurnOffDevice, ArcticFoxConfiguration.DeepSleepMode.DeviceOff)
					};
				}
			}

			public static object[] GenericBattery
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>(LocalizableStrings.BatteryModelGeneric, ArcticFoxConfiguration.BatteryModel.Generic),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Custom1", ArcticFoxConfiguration.BatteryModel.Custom1),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Custom2", ArcticFoxConfiguration.BatteryModel.Custom2),
						new NamedItemContainer<ArcticFoxConfiguration.BatteryModel>("Custom3", ArcticFoxConfiguration.BatteryModel.Custom3)
					};
				}
			}

			public static object[] MonochromeConversionModes
			{
				get
				{
					return new object[]
					{
						new NamedItemContainer<MonochromeConversionMode>("Threshold based", MonochromeConversionMode.ThresholdBased),
						new NamedItemContainer<MonochromeConversionMode>("Floyd Steinberg Dithering", MonochromeConversionMode.FloydSteinbergDithering)
					};
				}
			}
		}
	}
}
