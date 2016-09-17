using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using HidSharp;
using JetBrains.Annotations;

namespace NFirmwareEditor.Managers
{
	internal class FirmwareUpdater
	{
		private static class Commands
		{
			public const byte ReadDataflash = 0x35;
			public const byte WriteDataflash = 0x53;
			public const byte ResetDataflash = 0x7C;

			public const byte WriteData = 0xC3;
			public const byte Restart = 0xB4;

			public const byte Screenshot = 0xC1;
		}

		private const int VendorId = 0x0416;
		private const int ProductId = 0x5020;
		private const int DataflashLength = 2048;
		private const int LogoOffset = 102400;
		private const int LogoLength = 1024;

		private static readonly byte[] s_hidSignature = Encoding.UTF8.GetBytes("HIDC");
		private static readonly HidDeviceLoader s_loader = new HidDeviceLoader();

		private static readonly IDictionary<string, DeviceInfo> s_supportedDevices = new Dictionary<string, DeviceInfo>
		{
			{ "E052", new DeviceInfo("Joyetech eVic VTC Mini", 64, 40) },
			{ "E043", new DeviceInfo("Joyetech eVic VTwo", 64, 40) },
			{ "E115", new DeviceInfo("Joyetech eVic VTwo Mini", 64, 40) },
			{ "E150", new DeviceInfo("Joyetech eVic Basic", 64, 40) },
			{ "E092", new DeviceInfo("Joyetech eVic AIO", 64, 40) },

			{ "E060", new DeviceInfo("Joyetech Cuboid", 64, 40) },
			{ "E056", new DeviceInfo("Joyetech Cuboid Mini", 64, 40) },

			{ "E083", new DeviceInfo("Joyetech eGrip II", 64, 40) },

			{ "M972", new DeviceInfo("Eleaf iStick TC200W", 96, 16) },
			{ "M011", new DeviceInfo("Eleaf iStick TC100W", 96, 16) },
			{ "M041", new DeviceInfo("Eleaf iStick Pico", 96, 16) },
			{ "M045", new DeviceInfo("Eleaf iStick Pico Mega", 96, 16) },
			{ "M046", new DeviceInfo("Eleaf iStick Power", 96, 16) },
			{ "M037", new DeviceInfo("Eleaf ASTER", 96, 16) },

			{ "W007", new DeviceInfo("Wismec Presa TC75W") },

			{ "W018", new DeviceInfo("Wismec Reuleaux RX2/3", 64, 48) },
			{ "W014", new DeviceInfo("Wismec Reuleaux RX200") },
			{ "W033", new DeviceInfo("Wismec Reuleaux RX200S", 64, 48) },

			{ "W010", new DeviceInfo("Vaporflask Classic") },
			{ "W011", new DeviceInfo("Vaporflask Lite") },
			{ "W013", new DeviceInfo("Vaporflask Stout") },

			{ "W016", new DeviceInfo("Beyondvape Centurion") }
		};

		private readonly Timer m_monitoringTimer;

		private int m_receiveBufferLength;
		private int m_sentBufferLength;
		private bool? m_isDeviceConnected;

		public static readonly DeviceInfo UnknownDevice = new DeviceInfo("Unknown device");

		public FirmwareUpdater()
		{
			m_monitoringTimer = new Timer(state =>
			{
				var previousState = m_isDeviceConnected;
				var device = s_loader.GetDeviceOrDefault(VendorId, ProductId);

				if (previousState.HasValue)
				{
					if (device == null && previousState == false) return;
					if (device != null && previousState == true) return;
				}

				m_isDeviceConnected = device != null;
				OnDeviceConnected(m_isDeviceConnected.Value);
			});
		}

		public event Action<bool> DeviceConnected;

		public bool IsDeviceConnected
		{
			get { return s_loader.GetDeviceOrDefault(VendorId, ProductId) != null; }
		}

		public void StartMonitoring()
		{
			m_isDeviceConnected = null;
			m_monitoringTimer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
		}

		public void StopMonitoring()
		{
			m_monitoringTimer.Change(Timeout.Infinite, Timeout.Infinite);
			m_isDeviceConnected = null;
		}

		public SimpleDataflash ReadDataflash(BackgroundWorker worker = null)
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.ReadDataflash, 0, DataflashLength));
				var rawData = Read(stream, DataflashLength, worker);

				var checksum = BitConverter.ToInt32(rawData, 0);
				var data = new byte[rawData.Length - 4];
				Buffer.BlockCopy(rawData, 4, data, 0, data.Length);

				return new SimpleDataflash
				{
					Checksum = checksum,
					Data = data
				};
			}
		}

		public byte[] Screenshot()
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.Screenshot, 0, 0x400));
				return Read(stream, 0x400);
			}
		}

		public void WriteDataflash(SimpleDataflash simpleDataflash, BackgroundWorker worker = null)
		{
			var checksumBytes = BitConverter.GetBytes(simpleDataflash.Data.Sum(x => x));
			var rawData = new byte[simpleDataflash.Data.Length + checksumBytes.Length];

			Buffer.BlockCopy(checksumBytes, 0, rawData, 0, checksumBytes.Length);
			Buffer.BlockCopy(simpleDataflash.Data, 0, rawData, checksumBytes.Length, simpleDataflash.Data.Length);

			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.WriteDataflash, 0, DataflashLength));
				Write(stream, rawData, worker);
			}
		}

		public void WriteFirmware(byte[] firmware, BackgroundWorker worker = null)
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.WriteData, 0, firmware.Length));
				Write(stream, firmware, worker);
			}
		}

		public void WriteLogo(byte[] block1ImageBytes, byte[] block2ImageBytes, BackgroundWorker worker = null)
		{
			if (block1ImageBytes == null) throw new ArgumentNullException("block1ImageBytes");
			if (block2ImageBytes == null) throw new ArgumentNullException("block2ImageBytes");
			if (block1ImageBytes.Length > 512) throw new ArgumentException("block1ImageBytes is to big. Maximum allowed size is 512 bytes.");
			if (block2ImageBytes.Length > 512) throw new ArgumentException("block2ImageBytes is to big. Maximum allowed size is 512 bytes.");

			var data = new byte[LogoLength];
			{
				Buffer.BlockCopy(block2ImageBytes, 0, data, 0, block2ImageBytes.Length);
				Buffer.BlockCopy(block1ImageBytes, 0, data, 512, block1ImageBytes.Length);
			}
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.WriteData, LogoOffset, LogoLength));
				Write(stream, data, worker);
			}
		}

		public void RestartDevice()
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.Restart, 0, 0));
			}
		}

		public void ResetDataFlash()
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.ResetDataflash, 0, DataflashLength));
			}
		}

		[NotNull]
		public static DeviceInfo GetDeviceInfo([CanBeNull] string productId)
		{
			return string.IsNullOrEmpty(productId) || !s_supportedDevices.ContainsKey(productId)
				? UnknownDevice
				: s_supportedDevices[productId];
		}

		private HidStream OpenDeviceStream()
		{
			var device = s_loader.GetDeviceOrDefault(VendorId, ProductId);
			if (device == null) return null;

			m_receiveBufferLength = device.MaxOutputReportLength;
			m_sentBufferLength = device.MaxInputReportLength - 1;

			return device.Open();
		}

		private byte[] Read(HidStream steam, int length, BackgroundWorker worker = null)
		{
			var offset = 0;
			var result = new byte[length];
			while (offset < length)
			{
				var data = new byte[m_receiveBufferLength];
				steam.Read(data);
				var bufferLength = offset + data.Length < length
					? data.Length
					: length - offset;

				Buffer.BlockCopy(data, 1, result, offset, bufferLength - 1);
				offset += bufferLength == data.Length
					? bufferLength - 1
					: bufferLength;

				if (worker != null) worker.ReportProgress((int)(offset * 100f / length));
			}
			if (worker != null) worker.ReportProgress(100);
			return result;
		}

		private void Write(HidStream steam, byte[] data, BackgroundWorker worker = null)
		{
			var offset = 0;
			while (offset < data.Length)
			{
				var bufferLength = data.Length - offset > m_sentBufferLength
					? m_sentBufferLength
					: data.Length - offset;

				var buffer = new byte[bufferLength + 1];
				{
					buffer[0] = 0;
					Buffer.BlockCopy(data, offset, buffer, 1, bufferLength);
				}

				steam.Write(buffer);
				offset += bufferLength;

				if (worker != null) worker.ReportProgress((int)(offset * 100f / data.Length));
			}

			if (worker != null) worker.ReportProgress(100);
		}

		private static byte[] CreateCommand(byte commandCode, int arg1, int arg2)
		{
			using (var ms = new MemoryStream())
			using (var bw = new BinaryWriter(ms))
			{
				bw.Write(commandCode);
				bw.Write((byte)14);
				bw.Write(arg1);
				bw.Write(arg2);
				bw.Write(s_hidSignature);

				var cmd = ms.ToArray();
				var checksum = cmd.Sum(x => x);
				bw.Write(checksum);

				return ms.ToArray();
			}
		}

		protected virtual void OnDeviceConnected(bool isConnected)
		{
			var handler = DeviceConnected;
			if (handler != null) handler(isConnected);
		}
	}

	internal class DeviceInfo
	{
		private readonly bool m_canUploadLogo;

		public DeviceInfo(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			m_canUploadLogo = false;
		}

		public DeviceInfo([NotNull] string name, byte logoWidth, byte logoHeight)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			LogoWidth = logoWidth;
			LogoHeight = logoHeight;
			m_canUploadLogo = true;
		}

		public string Name { get; private set; }

		public bool CanUploadLogo
		{
			get { return m_canUploadLogo; }
		}

		public byte LogoWidth { get; private set; }

		public byte LogoHeight { get; private set; }
	}

	internal class SimpleDataflash
	{
		private const int BootFlagOffset = 9;
		private const int HwVerOffset = 4;
		private const int FwVerOffset = 256;
		private const int ProductIdOffset = 312;
		private const int ProductIdLength = 4;

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

		public float HardwareVersion
		{
			get
			{
				var hwInt = BitConverter.ToInt32(Data, HwVerOffset);
				return hwInt / 100f;
			}
		}

		public float FirmwareVersion
		{
			get
			{
				var hwInt = BitConverter.ToInt32(Data, FwVerOffset);
				return hwInt / 100f;
			}
		}
	}
}
