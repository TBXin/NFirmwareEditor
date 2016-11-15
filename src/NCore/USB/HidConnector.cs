using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using HidSharp;
using JetBrains.Annotations;
using NCore.USB.Models;

namespace NCore.USB
{
	public class HidConnector
	{
		private const int VendorId = 0x0416;
		private const int ProductId = 0x5020;
		private const int DataflashLength = 2048;
		private const int ConfigurationLength = 512;
		private const int LogoOffset = 102400;
		private const int LogoLength = 1024;

		private static class Commands
		{
			public const byte ReadDataflash = 0x35;
			public const byte WriteDataflash = 0x53;
			public const byte ResetDataflash = 0x7C;

			public const byte WriteData = 0xC3;
			public const byte Restart = 0xB4;

			public const byte Screenshot = 0xC1;

			public const byte EnableCOM = 0x42;
			public const byte DeviceMonitor = 0x43;
			public const byte Puff = 0x44;

			public const byte ReadConfiguration = 0x60;
			public const byte WriteConfiguration = 0x61;
		}

		private static readonly byte[] s_hidSignature = Encoding.UTF8.GetBytes("HIDC");
		private static readonly HidDeviceLoader s_loader = new HidDeviceLoader();

		private static readonly IDictionary<string, HidDeviceInfo> s_supportedDevices = new Dictionary<string, HidDeviceInfo>
		{
			{ "E052", new HidDeviceInfo("Joyetech eVic VTC Mini", 64, 40) },
			{ "E043", new HidDeviceInfo("Joyetech eVic VTwo", 64, 40) },
			{ "E115", new HidDeviceInfo("Joyetech eVic VTwo Mini", 64, 40) },
			{ "E079", new HidDeviceInfo("Joyetech eVic VTC Dual", 64, 40) },
			{ "E150", new HidDeviceInfo("Joyetech eVic Basic", 64, 40) },
			{ "E092", new HidDeviceInfo("Joyetech eVic AIO", 64, 40) },

			{ "E060", new HidDeviceInfo("Joyetech Cuboid", 64, 40) },
			{ "E056", new HidDeviceInfo("Joyetech Cuboid Mini", 64, 40) },

			{ "E083", new HidDeviceInfo("Joyetech eGrip II", 64, 40) },

			{ "M972", new HidDeviceInfo("Eleaf iStick TC200W", 96, 16) },
			{ "M011", new HidDeviceInfo("Eleaf iStick TC100W", 96, 16) },
			{ "M041", new HidDeviceInfo("Eleaf iStick Pico", 96, 16) },
			{ "M045", new HidDeviceInfo("Eleaf iStick Pico Mega", 96, 16) },
			{ "M046", new HidDeviceInfo("Eleaf iStick Power", 96, 16) },
			{ "M037", new HidDeviceInfo("Eleaf ASTER", 96, 16) },

			{ "W007", new HidDeviceInfo("Wismec Presa TC75W") },

			{ "W018", new HidDeviceInfo("Wismec Reuleaux RX2/3", 64, 48) },
			{ "W014", new HidDeviceInfo("Wismec Reuleaux RX200") },
			{ "W033", new HidDeviceInfo("Wismec Reuleaux RX200S", 64, 48) },
			{ "W026", new HidDeviceInfo("Wismec Reuleaux RX75", 64, 48) },

			{ "W010", new HidDeviceInfo("Vaporflask Classic") },
			{ "W011", new HidDeviceInfo("Vaporflask Lite") },
			{ "W013", new HidDeviceInfo("Vaporflask Stout") },

			{ "W016", new HidDeviceInfo("Beyondvape Centurion") }
		};

		private readonly Timer m_monitoringTimer;

		private int m_receiveBufferLength;
		private int m_sentBufferLength;
		private bool? m_isDeviceConnected;

		public static readonly HidDeviceInfo UnknownHidDevice = new HidDeviceInfo("Unknown device");

		public HidConnector()
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

		public void StartUSBConnectionMonitoring()
		{
			m_isDeviceConnected = null;
			m_monitoringTimer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
		}

		public void StopUSBConnectionMonitoring()
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

		public byte[] ReadConfiguration(BackgroundWorker worker = null)
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.ReadConfiguration, 0, ConfigurationLength));
				return Read(stream, ConfigurationLength, worker);
			}
		}

		public void WriteConfiguration(byte[] data, BackgroundWorker worker = null)
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.WriteConfiguration, 0, ConfigurationLength));
				Write(stream, data, worker);
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

		public void EnableCOM()
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.EnableCOM, 0, 0));
			}
		}

		public void SetupDeviceMonitor(bool enable)
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.DeviceMonitor, enable ? 1 : 0, 0));
			}
		}

		public void MakePuff(int seconds)
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.Puff, seconds, 0));
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

		public void ResetDataflash()
		{
			using (var stream = OpenDeviceStream())
			{
				Write(stream, CreateCommand(Commands.ResetDataflash, 0, DataflashLength));
			}
		}

		[NotNull]
		public static HidDeviceInfo GetDeviceInfo([CanBeNull] string productId)
		{
			return string.IsNullOrEmpty(productId) || !s_supportedDevices.ContainsKey(productId)
				? UnknownHidDevice
				: s_supportedDevices[productId];
		}

		public HidStream OpenDeviceStream()
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

				if (worker != null)
					worker.ReportProgress((int)(offset * 100f / length));
			}
			if (worker != null) worker.ReportProgress(100);
			return result;
		}

		public void Write(HidStream steam, byte[] data, BackgroundWorker worker = null)
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

		public static byte[] CreateCommand(byte commandCode, int arg1, int arg2)
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
}
