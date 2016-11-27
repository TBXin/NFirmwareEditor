using System;
using System.IO.Ports;
using System.Management;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using NCore;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class COMConnector
	{
		private const string PnpDeviceIdMask = "VID_0416&PID_5020";
		private static readonly char[] s_separatorChars = { '\r', '\n' };
		private SerialPort m_port;
		private string m_opendPort;

		public event Action Connected;
		public event Action Disconnected;
		public event Action<string> MessageReceived;
		public event Action<string> MonitorDataReceived;

		public bool IsConnected
		{
			get { return m_port != null && m_port.IsOpen; }
		}

		[CanBeNull]
		public string Connect(string portName = null)
		{
			if (IsConnected) Disconnect();

			portName = string.IsNullOrEmpty(portName) ? TryGetPortName() : portName;
			if (string.IsNullOrEmpty(portName)) return null;

			m_port = new SerialPort(portName) { RtsEnable = true, DtrEnable = true };
			m_port.DataReceived += COMPort_DataReceived;
			m_port.Open();

			System.Diagnostics.Trace.WriteLine("Port: " + portName + " opened. [IsOpen = " + m_port.IsOpen + "]");

			if (!m_port.IsOpen) return null;

			new Thread(PortMonitor) { IsBackground = true }.Start();
			m_opendPort = portName;
			OnConnected();
			return m_opendPort;
		}

		public void Disconnect()
		{
			if (!IsConnected && m_port == null) return;

			m_port.DataReceived -= COMPort_DataReceived;
			try
			{
				if (m_port.BytesToRead > 0)
				{
					System.Diagnostics.Trace.WriteLine("Flushing COM buffer[Size: " + m_port.BytesToRead + "]");
					var buffer = new byte[m_port.BytesToRead];
					m_port.Read(buffer, 0, m_port.BytesToRead);
					System.Diagnostics.Trace.WriteLine("COM buffer was flushed.");
				}
				System.Diagnostics.Trace.WriteLine("Attempt to close port: " + m_opendPort);
				m_port.Close();
				System.Diagnostics.Trace.WriteLine("Port: " + m_opendPort + " closed.");
			}
			finally
			{
				m_port = null;
				m_opendPort = null;
			}
		}

		public bool Send(string command)
		{
			if (string.IsNullOrEmpty(command)) throw new ArgumentNullException("command");
			if (!IsConnected) return false;

			var data = Encoding.ASCII.GetBytes(command);
			m_port.Write(data, 0, data.Length);
			System.Diagnostics.Trace.WriteLine("Sent command: " + command);
			return true;
		}

		[CanBeNull]
		private static string TryGetPortName()
		{
			using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM WIN32_SERIALPORT"))
			using (var collection = searcher.Get())
			{
				foreach (var device in collection)
				{
					var pnpDeviceId = (string)device.GetPropertyValue("PNPDeviceID");
					if (string.IsNullOrEmpty(pnpDeviceId)) continue;
					if (pnpDeviceId.IndexOf(PnpDeviceIdMask, StringComparison.OrdinalIgnoreCase) == -1) continue;

					var portName = (string)device.GetPropertyValue("DeviceID");
					return portName;
				}
			}
			return null;
		}

		private void COMPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string data;
			try
			{
				data = m_port.ReadExisting();
			}
			catch (InvalidOperationException ex)
			{
				Trace.Warn(ex);
				OnDisconnected();
				return;
			}

			var messages = data.Split(s_separatorChars, StringSplitOptions.RemoveEmptyEntries);

			foreach (var message in messages)
			{
				OnMessageReceived(message);

				var isStandby = message.StartsWith(SensorsKeys.StandbyKey, StringComparison.OrdinalIgnoreCase);
				var isFiring = message.StartsWith(SensorsKeys.FiringKey, StringComparison.OrdinalIgnoreCase);

				if (isStandby || isFiring) OnMonitorDataReceived(message);
			}
		}

		private void PortMonitor(object dummy)
		{
			do
			{
				var connectedPort = TryGetPortName();
				if (string.IsNullOrEmpty(connectedPort) || !string.Equals(m_opendPort, connectedPort))
				{
					OnDisconnected();
					break;
				}
				Thread.Sleep(500);
			}
			while (!string.IsNullOrEmpty(m_opendPort));
		}

		private void OnMessageReceived(string obj)
		{
			var handler = MessageReceived;
			if (handler != null) handler(obj);
		}

		protected virtual void OnMonitorDataReceived(string obj)
		{
			var handler = MonitorDataReceived;
			if (handler != null) handler(obj);
		}

		protected virtual void OnDisconnected()
		{
			var handler = Disconnected;
			if (handler != null) handler();
		}

		protected virtual void OnConnected()
		{
			var handler = Connected;
			if (handler != null) handler();
		}
	}
}
