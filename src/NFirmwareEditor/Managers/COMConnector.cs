using System;
using System.Management;
using JetBrains.Annotations;

namespace NFirmwareEditor.Managers
{
	internal class COMConnector
	{
		private const string PnpDeviceIdMask = "VID_0416&PID_5020";

		[CanBeNull]
		public string TryGetPortName()
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
	}
}
