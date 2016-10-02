using System;
using System.IO;
using NFirmwareEditor.Managers;

// ReSharper disable InconsistentNaming
namespace NFirmwareEditor.Models
{
	internal class Dataflash
	{
		public DataflashParamsBlock ParamsBlock { get; set; }

		[BinaryOffset(Absolute = 0x100)]
		public DataflashInfoBlock InfoBlock { get; set; }

		[BinaryOffset(Absolute = 0x200)]
		public DataflashNFEBlock NFEBlock { get; set; }
	}

	internal class DataflashParamsBlock
	{
		public uint PageChecksum { get; set; }
		public uint HardwareVersion { get; set; }
		public byte Magic { get; set; }
		public BootMode BootMode { get; set; }
		public VapeMode SelectedMode { get; set; }

		[BinaryOffset(Relative = 1)]
		public ushort Power { get; set; }

		public ushort Temperature { get; set; }
		public ushort TCPower { get; set; }
		public ushort VWVolts { get; set; }
		public LineContentType ThirdLineContent { get; set; }
		public byte ResistanceType { get; set; }
		public byte TemperatureAlgo { get; set; }
		public bool IsCelsius { get; set; }
		public ushort Resistance { get; set; }
		public ushort ResistanceTi { get; set; }
		public ushort ResistanceNi { get; set; }
		public bool ResistanceTiLocked { get; set; }
		public bool ResistanceNiLocked { get; set; }
		public bool TiOn { get; set; }
		public bool StealthOn { get; set; }

		[BinaryArray(Length = 21)]
		public ushort[] TempCoefsNi { get; set; }

		[BinaryArray(Length = 21)]
		public ushort[] TempCoefsTi { get; set; }

		/// <summary>4 bytes structure.</summary>
		[BinaryOffset(Relative = 2)]
		public DataflashStatus Status { get; set; }

		public ushort AtomizerResistance { get; set; }

		/// <summary>0,1,2,3,4 = Open,Short,Low,Large,Ok</summary>
		public byte AtomizerStatus { get; set; }

		public byte ShuntCorrection { get; set; }
		public ushort ResistanceSS { get; set; }

		public bool ResistanceSSLocked { get; set; }
		public byte UIVersion { get; set; }
		public byte SelectedTCRIndex { get; set; }
		public BatteryModel SelectedBatteryModel { get; set; }

		[BinaryArray(Length = 3)]
		public ushort[] TCR { get; set; }

		public ushort ResistanceTCR { get; set; }
		public bool ResistanceTCRLocked { get; set; }
		public ScreensaverType ScreensaverType { get; set; }
		public byte LastTCMode { get; set; }
		public ScreenProtectionTime ScreenProtectionTime { get; set; }

		[BinaryArray(Length = 10)]
		public ushort[] SavedCfgRez { get; set; }

		[BinaryArray(Length = 10)]
		public ushort[] SavedCfgPwr { get; set; }

		public ushort FBBest { get; set; }
		public byte FBSpeed { get; set; }
		public byte byte_2000033D { get; set; }

		/// <summary>pc = ( ( 100 * dfContrast ) / 255 );</summary>
		public byte Contrast { get; set; }

		public VapeModes DisabledModes { get; set; }
		public ushort ClkRatio { get; set; }
		public ushort PreheatPwr { get; set; }
		public byte PreheatTime { get; set; }

		[BinaryArray(Length = 3)]
		public ClickAction[] MClicks { get; set; }

		public byte ScreenDimTimeout { get; set; }

		[BinaryOffset(Relative = 1)]
		public CustomBattery CustomBattery { get; set; }
	}

	internal class DataflashInfoBlock
	{
		public uint FWVersion { get; set; }
		public uint LDVersion { get; set; }
		public uint fmcCID { get; set; }
		public uint fmcDID { get; set; }
		public uint fmcPID { get; set; }

		[BinaryArray(Length = 3)]
		public uint[] fmcUID { get; set; }

		[BinaryArray(Length = 4)]
		public uint[] fmcUCID { get; set; }

		public uint PuffCount { get; set; }
		public uint TimeCount { get; set; }

		[AsciiString(Length = 4)]
		public string ProductID { get; set; }

		public uint MaxHWVersion { get; set; }
		public ushort Year { get; set; }
		public byte Month { get; set; }
		public byte Day { get; set; }
		public byte Hour { get; set; }
		public byte Minute { get; set; }
		public byte Second { get; set; }
	}

	internal class DataflashNFEBlock
	{
		[BinaryArray(Length = 3)]
		public byte[] Build { get; set; }
	}

	internal class DataflashStatus : IBinaryReaderWriter
	{
		// 1st byte
		public bool Off { get; set; }
		public bool Keylock { get; set; }
		public bool Flipped { get; set; }
		public bool NoLogo { get; set; }
		public bool AnalogClock { get; set; }
		public bool VirtualCom { get; set; }
		public bool Storage { get; set; }
		public bool DebugEnable { get; set; }

		// 2nd byte
		public bool X32Off { get; set; }
		public bool TemperatureDominant { get; set; }
		public bool Step1W { get; set; }
		public bool DigitalClock { get; set; }
		public bool BatteryPercent { get; set; }
		public bool PreheatPercent { get; set; }
		public bool WakeUpByPlusMinus { get; set; }
		public bool UseClassicMenu { get; set; }

		#region Implementation of IBinaryReaderWriter
		public void Read(BinaryReader r)
		{
			var data = r.ReadBytes(4);
			var b1 = data[0];
			{
				Off = DataflashManager.GetBit(b1, 1);
				Keylock = DataflashManager.GetBit(b1, 2);
				Flipped = DataflashManager.GetBit(b1, 3);
				NoLogo = DataflashManager.GetBit(b1, 4);
				AnalogClock = DataflashManager.GetBit(b1, 5);
				VirtualCom = DataflashManager.GetBit(b1, 6);
				Storage = DataflashManager.GetBit(b1, 7);
				DebugEnable = DataflashManager.GetBit(b1, 8);
			}

			var b2 = data[1];
			{
				X32Off = DataflashManager.GetBit(b2, 1);
				TemperatureDominant = DataflashManager.GetBit(b2, 2);
				Step1W = DataflashManager.GetBit(b2, 3);
				DigitalClock = DataflashManager.GetBit(b2, 4);
				BatteryPercent = DataflashManager.GetBit(b2, 5);
				PreheatPercent = DataflashManager.GetBit(b2, 6);
				WakeUpByPlusMinus = DataflashManager.GetBit(b2, 7);
				UseClassicMenu = DataflashManager.GetBit(b2, 8);
			}
		}

		public void Write(BinaryWriter r)
		{
			byte b1 = 0;
			b1 = DataflashManager.SetBit(b1, 1, Off);
			b1 = DataflashManager.SetBit(b1, 2, Keylock);
			b1 = DataflashManager.SetBit(b1, 3, Flipped);
			b1 = DataflashManager.SetBit(b1, 4, NoLogo);
			b1 = DataflashManager.SetBit(b1, 5, AnalogClock);
			b1 = DataflashManager.SetBit(b1, 6, VirtualCom);
			b1 = DataflashManager.SetBit(b1, 7, Storage);
			b1 = DataflashManager.SetBit(b1, 8, DebugEnable);

			byte b2 = 0;
			b2 = DataflashManager.SetBit(b2, 1, X32Off);
			b2 = DataflashManager.SetBit(b2, 2, TemperatureDominant);
			b2 = DataflashManager.SetBit(b2, 3, Step1W);
			b2 = DataflashManager.SetBit(b2, 4, DigitalClock);
			b2 = DataflashManager.SetBit(b2, 5, BatteryPercent);
			b2 = DataflashManager.SetBit(b2, 6, PreheatPercent);
			b2 = DataflashManager.SetBit(b2, 7, WakeUpByPlusMinus);
			b2 = DataflashManager.SetBit(b2, 8, UseClassicMenu);

			r.Write(new byte[] { b1, b2, 0, 0 });
		}
		#endregion
	}

	public class CustomBattery
	{
		[BinaryArray(Length = 11)]
		public PercentsVoltage[] Data { get; set; }

		public ushort Cutoff { get; set; }

		public ushort InternalResistance { get; set; }
	}

	public class PercentsVoltage
	{
		public ushort Percents { get; set; }

		public ushort Voltage { get; set; }
	}

	internal enum BootMode : byte
	{
		APROM = 0,
		LDROM = 1
	}

	internal enum VapeMode : byte
	{
		TempNi = 0,
		TempTi = 1,
		TempSS = 2,
		TCR = 3,
		Power = 4,
		Bypass = 5,
		Start = 6
	}

	[Flags]
	internal enum VapeModes : byte
	{
		None = 0,
		TempNi = 1 << 0,
		TempTi = 1 << 1,
		TempSS = 1 << 2,
		TCR = 1 << 3,
		Power = 1 << 4,
		Bypass = 1 << 5,
		Start = 1 << 6
	}

	internal enum ClickAction : byte
	{
		None = 0,
		Edit = 1,
		Clock = 2,
		TDom = 3,
		NextMode = 4,
		OnOff = 5
	}

	internal enum ScreensaverType : byte
	{
		None = 0,
		Clock = 1,
		Cube = 2
	}

	internal enum ScreenProtectionTime : byte
	{
		Min1 = 0,
		Min2 = 1,
		Min5 = 2,
		Min10 = 3,
		Min15 = 4,
		Min20 = 5,
		Min30 = 6,
		Off = 7
	}

	internal enum LineContentType : byte
	{
		Amps = 0,
		Puffs = 1,
		Time = 2,
		BatteryVoltage = 3,
		OutputVoltage = 4,
		BoardTemperature = 5,
		RealTimeResistance = 6,
		DataTime = 7
	}

	internal enum BatteryModel : byte
	{
		Generic = 0,
		Samsung25R = 1,
		LGHG2 = 2,
		LGHE4 = 3,
		Samsung30Q = 4,
		SonyVTC4 = 5,
		SonyVTC5 = 6,
		Custom = 7
	}
}
