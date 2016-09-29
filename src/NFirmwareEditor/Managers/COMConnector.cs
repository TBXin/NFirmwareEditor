using System;
using System.IO.Ports;
using System.Management;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class COMConnector
	{
		private const string PnpDeviceIdMask = "VID_0416&PID_5020";
		private static readonly char[] s_separatorChars = { '\r', '\n' };
		private SerialPort m_port;
		private string m_opendPort;
		private bool m_monitorEnabled;

		public event Action<string> MessageReceived;
		public event Action<string> MonitorDataReceived;
		public event Action Disconnected;

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
			return m_opendPort;
		}

		public void Disconnect()
		{
			if (!IsConnected && m_port == null) return;

			m_port.DataReceived -= COMPort_DataReceived;
			try
			{
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

		public bool EnableDeviceMonitor()
		{
			if (m_monitorEnabled) return true;

			var sendResult = Send("M1");
			var waitResult = SpinWait.SpinUntil(() => m_monitorEnabled, TimeSpan.FromSeconds(2));
			return sendResult && waitResult;
		}

		public bool DisableDeviceMonitor()
		{
			if (!m_monitorEnabled) return true;

			var sendResult = Send("M0");
			var waitResult = SpinWait.SpinUntil(() => !m_monitorEnabled, TimeSpan.FromSeconds(2));
			return sendResult && waitResult;
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
			var data = m_port.ReadExisting();
			var messages = data.Split(s_separatorChars, StringSplitOptions.RemoveEmptyEntries);

			foreach (var message in messages)
			{
				OnMessageReceived(message);

				//System.Diagnostics.Trace.WriteLine(message);
				if (message.StartsWith(SensorsKeys.MonitorOn, StringComparison.OrdinalIgnoreCase))
				{
					m_monitorEnabled = true;
					System.Diagnostics.Trace.WriteLine("m_monitorEnabled = true");
				}
				if (message.StartsWith(SensorsKeys.MonitorOff, StringComparison.OrdinalIgnoreCase))
				{
					m_monitorEnabled = false;
					System.Diagnostics.Trace.WriteLine("m_monitorEnabled = false");
				}
				if (m_monitorEnabled)
				{
					var isStandby = message.StartsWith(SensorsKeys.StandbyKey, StringComparison.OrdinalIgnoreCase);
					var isFiring = message.StartsWith(SensorsKeys.FiringKey, StringComparison.OrdinalIgnoreCase);

					if (isStandby || isFiring) OnMonitorDataReceived(message);
				}
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
	}
}
