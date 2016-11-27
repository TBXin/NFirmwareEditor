using System;
using System.Text;

namespace NCore.USB.Models
{
	public class SimpleDataflash
	{
		private const int BootFlagOffset = 9;
		private const int HwVerOffset = 4;
		private const int FwVerOffset = 256;
		private const int ProductIdOffset = 312;
		private const int ProductIdLength = 4;
		private const int BuildOffset = 0x200;
		private const int BuildLength = 3;

		public int Checksum { get; set; }

		public byte[] Data { get; set; }

		public bool LoadFromLdrom
		{
			get { return Data[BootFlagOffset] == 1; }
			set { Data[BootFlagOffset] = (byte)(value ? 1 : 0); }
		}

		public string ProductId
		{
			get { return Encoding.UTF8.GetString(Data, ProductIdOffset, ProductIdLength); }
		}

		public int HardwareVersion
		{
			get
			{
				var hwInt = BitConverter.ToInt32(Data, HwVerOffset);
				return hwInt;
			}
			set
			{
				var newHwBytes = BitConverter.GetBytes(value);
				Buffer.BlockCopy(newHwBytes, 0, Data, HwVerOffset, newHwBytes.Length);
			}
		}

		public int FirmwareVersion
		{
			get
			{
				var hwInt = BitConverter.ToInt32(Data, FwVerOffset);
				return hwInt;
			}
		}

		public int Build
		{
			get
			{
				var buildString = string.Empty;
				for (var i = BuildOffset; i < BuildOffset + BuildLength; i++)
				{
					buildString += Data[i].ToString("D2");
				}
				int build;
				return int.TryParse(buildString, out build) ? build : -1;
			}
		}
	}
}