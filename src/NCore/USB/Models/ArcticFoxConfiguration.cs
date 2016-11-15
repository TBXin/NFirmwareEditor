using System.IO;

namespace NCore.USB.Models
{
	public class ArcticFoxConfiguration
	{
		public class DeviceInfo
		{
			[BinaryAsciiString(Length = 4)]
			public string ProductId;

			public uint HardwareVersion;
			public ushort MaxPower;
			public byte NumberOfBatteries;
			public uint FirmwareVersion;
			public byte SettingsVersion;
		}

		public class Profile
		{
			// ASCII chars "0..9", "A..Z", " ", "."
			[BinaryAsciiString(Length = 8)]
			public string Name;

			public ProfileFlags Flags;
			// 0 - Off, unit is 1/100 sec
			public byte PreheatTime;
			// Watts * 10
			public ushort PreheatPower;
			// Watts * 10
			public ushort Power;
			public ushort Temperature;
			// Multiplied by 1000
			public ushort Resistance;
			public ushort TCR;
		}

		public class ProfileFlags : IBinaryStructure
		{
			public Material Material;
			public byte IsTemperatureDominant;
			public byte IsCelcius;
			public byte IsResistanceLocked;
			public byte IsPreheatInPercents;
			
			public void Read(BinaryReader br)
			{
				var flags = br.ReadByte();
				Material = (Material)(flags & 0x0F);
			}

			public void Write(BinaryWriter bw)
			{
				throw new System.NotImplementedException();
			}
		}

		public enum Material
		{
			VariWatt = 0,
			Nickel = 1,
			Titanium = 2,
			StainlessSteel = 3,
			TCR = 4
		}
	}
}
