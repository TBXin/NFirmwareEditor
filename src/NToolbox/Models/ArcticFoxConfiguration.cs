using System;
using System.IO;
using NCore;

namespace NToolbox.Models
{
	#pragma warning disable 0649
	internal class ArcticFoxConfiguration
	{
		internal const ushort MaxPower = 4000;
		internal const byte MaxBatteries = 4;
		internal const int MinimumSupportedBuildNumber = 170508;
		internal const int SupportedSettingsVersion = 8;

		public DeviceInfo Info;
		public GeneralConfiguration General;
		public UIConfiguration Interface;
		public CountersData Counters;
		public AdvancedConfiguration Advanced;

		internal class DeviceInfo
		{
			public byte SettingsVersion;

			[BinaryAsciiString(Length = 4)]
			public string ProductId;

			public uint HardwareVersion;
			public ushort MaxDevicePower;
			public byte NumberOfBatteries;
			public DisplaySize DisplaySize;
			public uint FirmwareVersion;
			public uint FirmwareBuild;

			public DeviceInfo Copy()
			{
				return (DeviceInfo)MemberwiseClone();
			}
		}

		internal class GeneralConfiguration
		{
			[BinaryArray(Length = 8)]
			public Profile[] Profiles;

			public byte SelectedProfile;
			public SmartMode SmartMode;
			public byte SmartRange; // 1..15, percents
		}

		internal class UIConfiguration
		{
			[BinaryArray(Length = 3)]
			public ClickAction[] ClicksVW;

			[BinaryArray(Length = 3)]
			public ClickAction[] ClicksTC;

			[BinaryArray(Length = 3)]
			public Shortcuts[] ShortcutsVW; // [0]: Fire -, [1]: Fire +, [2]: - +

			[BinaryArray(Length = 3)]
			public Shortcuts[] ShortcutsTC; // [0]: Fire -, [1]: Fire +, [2]: - +

			public ClassicLinesContent ClassicSkinVWLines;
			public ClassicLinesContent ClassicSkinTCLines;

			public CircleLinesContent CircleSkinVWLines;
			public CircleLinesContent CircleSkinTCLines;

			public FoxyLinesContent FoxySkinVWLines;
			public FoxyLinesContent FoxySkinTCLines;

			public SmallLinesContent SmallSkinVWLines;
			public SmallLinesContent SmallSkinTCLines;

			public byte Brightness;
			public byte DimTimeout; // 5..60 sec
			public byte DimTimeoutLocked; // 5..60 sec
			public byte ShowLogoDelay; // 1..60 sec
			public byte ShowClockDelay; // 1..60 sec
			public bool IsFlipped;
			public bool IsStealthMode;
			public bool WakeUpByPlusMinus;
			public bool IsPowerStep1W;
			public ChargeScreenType ChargeScreenType;
			public ChargeExtraType ChargeExtraType;
			public bool IsLogoEnabled;
			public bool IsClassicMenu;
			public ClockType ClockType;
			public bool IsClockOnMainScreen;
			public ScreenProtectionTime ScreensaveDuration;
			public byte PuffScreenDelay; // 0..50 = 0,0..5,0 sec
			public PuffsTimeFormat PuffsTimeFormat;
			public Skin MainScreenSkin; // 64x128: 0 - Classic, 1 - Circle, 2 - Foxy; 96x16: 0 - Classic, 1 - Lite
			public bool IsUpDownSwapped;
			public bool ShowChargingInStealth;
			public bool ShowScreensaverInStealth;
			public bool ClockOnClickInStealth;
			public FiveClicks FiveClicks;
		}

		internal class CountersData
		{
			public uint PuffsCount; // Max 99999
			public uint PuffsTime; // Value multiplied by 10. Max 359999.
			public DateTime DateTime;
		}

		internal class DateTime
		{
			public ushort Year;
			public byte Month;
			public byte Day;
			public byte Hour;
			public byte Minute;
			public byte Second;
		}

		internal class AdvancedConfiguration
		{
			public byte ShuntCorrection; // Value from 85 to 115
			public BatteryModel BatteryModel;
			[BinaryArray(Length = 3)]
			public CustomBattery[] CustomBatteryProfiles;

			public RtcMode RtcMode;
			public bool IsUsbCharge;
			public bool ResetCountersOnStartup;

			[BinaryArray(Length = 8)]
			public TFRTable[] TFRTables;

			public byte PuffCutOff; // Value from 10 to 150

			[BinaryArray(Length = 8)]
			public PowerCurve[] PowerCurves;

			[BinaryArray(Length = 4)]
			public sbyte[] BatteryVoltageOffsets; // Value from (-30 to 30) * 100

			public bool CheckTCR;
			public bool UsbNoSleep;
			public DeepSleepMode DeepSleepMode;
		}

		internal class TFRTable
		{
			[BinaryAsciiString(Length = 4)]
			public string Name;

			[BinaryArray(Length = 7)]
			public TFRPoint[] Points;
		}

		internal class TFRPoint
		{
			public ushort Temperature;
			public ushort Factor; // value * 10000
		}

		internal class PowerCurve
		{
			[BinaryAsciiString(Length = 8)]
			public string Name;

			[BinaryArray(Length = 12)]
			public PowerCurvePoint[] Points;
		}

		internal class PowerCurvePoint
		{
			public byte Time; // X * 10
			public byte Percent; // 0 - 255%
		}

		internal class CustomBattery
		{
			public CustomBattery()
			{
			}

			public CustomBattery(PercentsVoltage[] data, ushort cutoff)
			{
				Data = data;
				Cutoff = cutoff;
			}

			[BinaryAsciiString(Length = 4)]
			public string Name;

			[BinaryArray(Length = 11)]
			public PercentsVoltage[] Data;

			public ushort Cutoff; // Value multiplied by 100, from 275 to 350
		}

		internal class PercentsVoltage
		{
			public PercentsVoltage()
			{
			}

			public PercentsVoltage(ushort percents, ushort voltage)
			{
				Percents = percents;
				Voltage = voltage;
			}

			public ushort Percents; // Value from 0 to 100
			public ushort Voltage; // Value multiplied by 100, from 300 to 420

			#region Overrides of Object
			public override string ToString()
			{
				return Percents + "% - " + Voltage / 100f + "V";
			}
			#endregion
		}

		internal enum SmartMode : byte
		{
			Off = 0,
			On = 1,
			Lazy = 2
		}

		internal enum RtcMode : byte
		{
			Lxt = 0,
			Lirc = 1,
			Lsl = 2
		}

		internal enum BatteryModel : byte
		{
			Generic = 0,
			Custom1 = 1,
			Custom2 = 2,
			Custom3 = 3
		}

		internal enum ChargeScreenType : byte
		{
			Classic = 0,
			Extended = 1
		}

		internal enum ChargeExtraType : byte
		{
			None = 0,
			AnalogClock = 1,
			DigitalClock = 2,
			Logo = 3
		}

		internal enum FiveClicks : byte
		{
			OnOff = 0,
			LockUnlock = 1
		}

		internal enum ClockType : byte
		{
			Analog = 0,
			Digital = 1
		}

		internal enum PuffsTimeFormat : byte
		{
			Seconds = 0,
			HourMinuteSeconds = 1
		}

		internal enum Skin : byte
		{
			Classic = 0,
			Circle = 1,
			Foxy = 2
		}

		internal enum ScreenProtectionTime : byte
		{
			Off = 0,
			Min1 = 1,
			Min2 = 2,
			Min5 = 5,
			Min10 = 10,
			Min15 = 15,
			Min20 = 20,
			Min30 = 30
		}

		internal class ClassicLinesContent
		{
			public LineContent Line1;
			public LineContent Line2;
			public LineContent Line3;
			public LineContent Line4;
		}

		internal class CircleLinesContent
		{
			public LineContent Line1; // 0x20, 0x30...0x3A
			public LineContent Line2; // 0x20, 0x30...0x3A
			public LineContent Line3; // 0x40...0x43 | 0x80
		}

		internal class SmallLinesContent
		{
			public LineContent Line1; // 0x20, 0x30...0x3A | 0x80
			public LineContent Line2; // 0x20, 0x30...0x3A | 0x80
		}

		internal class FoxyLinesContent
		{
			public FoxyLineContent Line1;
			public FoxyLineContent Line2;
			public FoxyLineContent Line3;
		}

		internal enum ClickAction : byte
		{
			None = 0,
			Edit = 1,
			ProfileSelector = 2,
			TemperatureDominant = 3,
			MainScreenClock = 4,
			OnOff = 5,
			LslOnOff = 6,
			MainMenu = 7,
			PreheatEdit = 8,
			ProfileEdit = 9,
			SmartOnOff = 10,
			InfoScreen = 11,
			ResetCounters = 12,
			StealthOnOff = 13,
			KeyLock = 14,
			LockResistance = 15,
			PowerBank = 16,
			DeviceLock = 17,
			ReReadResistanceAndSaveToProfile = 18,
			ReReadResistanceAndSmart = 19
		}

		internal enum ShortcutsInEdit : byte
		{
			None = 0,
			ResetCounters = 1,
		}

		internal enum ShortcutsInSelector : byte
		{
			None = 0,
			ResetResistance = 1
		}

		internal enum ShortcutsInMenu : byte
		{
			None = 0,
			Back = 1,
			Exit = 2
		}

		internal class Shortcuts
		{
			public ClickAction InStandby;
			public ShortcutsInEdit InEditMain;
			public ShortcutsInSelector InSelector;
			public ShortcutsInMenu InMenu;
		}

		internal enum DeepSleepMode : byte
		{
			Standart = 0,
			DeviceOff = 1,
			DeviceLock = 2
		}

		[Flags]
		internal enum LineContent : byte
		{
			NonDominant = 0,
			Volt = 0x10,
			Resistance = 0x20,

			Amps = 0x30,
			Puffs = 0x31,
			Time = 0x32,
			BatteryVolts = 0x33,
			Vout = 0x34,
			BoardTemperature = 0x35,
			RealResistance = 0x36,
			DateTime = 0x37,
			LastPuff = 0x38,
			LastTemperature = 0x39,
			LastPower = 0x3A,
			BatteryPercents = 0x3B,

			Battery = 0x40,
			BatteryWithPercents = 0x41,
			BatteryWithVolts = 0x42,

			FireTimeMask = 0x80
		}

		[Flags]
		internal enum FoxyLineContent : byte
		{
			Amps = 0x30,
			Puffs = 0x31,
			Time = 0x32,
			BatteryVolts = 0x33,
			Vout = 0x34,
			RealResistance = 0x35,
			DateTime = 0x36,
			LastPuff = 0x37,
			LastTemperature = 0x38,
			LastPower = 0x39,

			FireTimeMask = 0x80
		}

		internal class Profile
		{
			// ASCII chars "0..9", "A..Z", " ", "."
			[BinaryAsciiString(Length = 8)]
			public string Name;

			public ProfileFlags Flags;
			public PreheatType PreheatType;
			public byte SelectedCurve;
			// 0 - Off, unit is 1/100 sec
			public byte PreheatTime;
			// 0 - Off, units is 1/10 sec
			public byte PreheatDelay;
			// Watts * 10
			public ushort PreheatPower;
			// Watts * 10
			public ushort Power;
			public ushort Temperature;
			// Multiplied by 1000
			public ushort Resistance;
			public ushort TCR;
			public PIRegulator PIRegulator;

			#region Overrides of Object
			public override string ToString()
			{
				return string.Format
				(
					"{0}, Pwr: {1}, Temp: {2}, Res: {3}, PP: {4}, PT: {5}, TCR: {6}",
					Name,
					Power / 10f,
					Temperature,
					Resistance / 1000f,
					PreheatPower / 10f,
					PreheatTime / 100f,
					TCR
				);
			}
			#endregion
		}

		internal class ProfileFlags : IBinaryStructure
		{
			public Material Material;
			public bool IsTemperatureDominant;
			public bool IsCelcius;
			public bool IsResistanceLocked;
			public bool IsEnabled;

			public void Read(BinaryReader br)
			{
				var flags = br.ReadByte();
				Material = (Material)(flags & 0x0F);
				IsTemperatureDominant = flags.GetBit(5);
				IsCelcius = flags.GetBit(6);
				IsResistanceLocked = flags.GetBit(7);
				IsEnabled = flags.GetBit(8);
			}

			public void Write(BinaryWriter bw)
			{
				var flags = ((byte)Material)
					.SetBit(5, IsTemperatureDominant)
					.SetBit(6, IsCelcius)
					.SetBit(7, IsResistanceLocked)
					.SetBit(8, IsEnabled);
				bw.Write(flags);
			}
		}

		internal enum Material : byte
		{
			VariWatt = 0,
			Nickel = 1,
			Titanium = 2,
			StainlessSteel = 3,
			TCR = 4,
			TFR1 = 5,
			TFR2 = 6,
			TFR3 = 7,
			TFR4 = 8,
			TFR5 = 9,
			TFR6 = 10,
			TFR7 = 11,
			TFR8 = 12
		}

		internal enum PreheatType : byte
		{
			Watts = 0,
			Percents = 1,
			Curve = 2
		}

		internal enum DisplaySize : byte
		{
			W64H128 = 0,
			W96H16 = 1
		}

		internal class PIRegulator
		{
			public bool IsEnabled;
			public byte Range; // 0 - 100
			public ushort PValue; // 10 - 6000
			// ReSharper disable once InconsistentNaming
			public ushort IValue; // 0 - 9000
		}
	}
	#pragma warning restore 0649
}
