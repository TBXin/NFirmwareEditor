using System.Xml.Serialization;
using NCore.Serialization;

namespace NToolbox.Models
{
	[XmlType("DeviceMonitorConfiguration")]
	public class ToolboxConfiguration : NamespacelessObject
	{
		public ToolboxConfiguration()
		{
			ShowPuffsBoundaries = true;
			ActiveSeries = new SerializableDictionary<string, bool>();
		}

		public bool OpenArcticFoxConfigurationWhenDeviceIsConnected { get; set; }

		public bool SynchronizeTimeWhenDeviceIsConnected { get; set; }

		public bool TakeScreenshotBeforeSave { get; set; }

		public int SelectedScreenSize { get; set; }

		public bool ShowPuffsBoundaries { get; set; }

		public SerializableDictionary<string, bool> ActiveSeries { get; set; }
	}
}
