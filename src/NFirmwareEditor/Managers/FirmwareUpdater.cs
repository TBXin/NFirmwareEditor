using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using HidSharp;

namespace NFirmwareEditor.Managers
{
	internal class FirmwareUpdater
	{
		private static class Commands
		{
			public const byte ReadDataFlash = 0x35;
			public const byte WriteDataFlash = 0x53;
			public const byte ResetDataFlash = 0x7C;

			public const byte WriteFirmware = 0xC3;
			public const byte Restart = 0xB4;
		}

		private static readonly IDictionary<string, string> s_deviceName = new Dictionary<string, string>
		{
			{ "E052", "Joyetech eVic-VTC Mini" },
			{ "E060", "Joyetech Cuboid" },
			{ "M011", "Eleaf iStick TC100W" },
			{ "W007", "Wismec Presa TC75W" },
			{ "W010", "Vaporflask Classic" },
			{ "W011", "Vaporflask Lite" },
			{ "W013", "Vaporflask Stout" },
			{ "W014", "Wismec Reuleaux RX200" }
		};

		private const int VendorId = 0x0416;
		private const int ProductId = 0x5020;
		private static readonly byte[] s_hidSignature = Encoding.UTF8.GetBytes("HIDC");
		private static readonly HidDeviceLoader s_loader = new HidDeviceLoader();

		private readonly object m_syncLock = new object();
		private readonly Timer m_monitoringTimer;

		private HidDevice m_device;
		private int m_receiveBufferLength;
		private int m_sentBufferLength;
		private bool? m_isDeviceConnected;

		public FirmwareUpdater()
		{
			m_monitoringTimer = new Timer(state =>
			{
				lock (m_syncLock)
				{
					var isConnected = IsDeviceConnected;

					if (!m_isDeviceConnected.HasValue) 
					{
						m_isDeviceConnected = isConnected;
					}
					else if (m_isDeviceConnected.Value == isConnected)
					{
						return;
					}
					else
					{
						m_isDeviceConnected = isConnected;
					}
					OnDeviceConnected(m_isDeviceConnected.Value);
				}
			});
			m_monitoringTimer.Change(Timeout.Infinite, Timeout.Infinite);
		}

		public event Action<bool> DeviceConnected;

		public bool IsDeviceConnected
		{
			get { return s_loader.GetDeviceOrDefault(VendorId, ProductId) != null; }
		}

		public void StartMonitoring()
		{
			m_monitoringTimer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(1));
		}

		public void StopMonitoring()
		{
			m_monitoringTimer.Change(Timeout.Infinite, Timeout.Infinite);
		}

		public DataFlash ReadDataFlash(BackgroundWorker worker = null)
		{
			var stream = Connect();
			Write(stream, CreateCommand(Commands.ReadDataFlash, 0, 2048));
			var rawData = Read(stream, 2048, worker);

			var checksum = BitConverter.ToInt32(rawData, 0);
			var data = new byte[rawData.Length - 4];
			Buffer.BlockCopy(rawData, 4, data, 0, data.Length);

			return new DataFlash
			{
				Checksum = checksum,
				Data = data
			};
		}

		public void WriteDataFlash(DataFlash dataFlash, BackgroundWorker worker = null)
		{
			var checksum = dataFlash.Data.Sum(x => x);
			var checksumBytes = BitConverter.GetBytes(checksum);
			var rawData = new byte[dataFlash.Data.Length + 4];

			Buffer.BlockCopy(checksumBytes, 0, rawData, 0, checksumBytes.Length);
			Buffer.BlockCopy(dataFlash.Data, 0, rawData, 4, dataFlash.Data.Length);

			var stream = Connect();
			Write(stream, CreateCommand(Commands.WriteDataFlash, 0, 2048));
			Write(stream, rawData, worker);
		}

		public void WriteFirmware(byte[] firmware, BackgroundWorker worker = null)
		{
			var stream = Connect();
			Write(stream, CreateCommand(Commands.WriteFirmware, 0, firmware.Length));
			Write(stream, firmware, worker);
		}

		public void RestartDevice()
		{
			var stream = Connect();
			Write(stream, CreateCommand(Commands.Restart, 0, 0));
			Close();
		}

		public void ResetDataFlash()
		{
			var stream = Connect();
			Write(stream, CreateCommand(Commands.ResetDataFlash, 0, 2048));
		}

		public static string GetDeviceName(string productId)
		{
			return s_deviceName.ContainsKey(productId) ? s_deviceName[productId] : "Unknown device";
		}

		private HidStream Connect()
		{
			if (m_device == null) m_device = s_loader.GetDeviceOrDefault(VendorId, ProductId);
			if (m_device == null) return null;

			m_receiveBufferLength = m_device.MaxOutputReportLength;
			m_sentBufferLength = m_device.MaxInputReportLength - 1;
			return m_device.Open();
		}

		private void Close()
		{
			m_device = null;
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

	internal class DataFlash
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
