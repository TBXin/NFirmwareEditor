using System.Xml.Serialization;
using NCore.Serialization;

namespace NToolbox.Models
{
	[XmlType("DeviceMonitorConfiguration")]
	public class DeviceMonitorConfiguration : NamespacelessObject
	{
		public DeviceMonitorConfiguration()
		{
			ShowPuffsBoundaries = true;
			ActiveSeries = new SerializableDictionary<string, bool>();
		}

		public bool ShowPuffsBoundaries { get; set; }

		public SerializableDictionary<string, bool> ActiveSeries { get; set; }
	}
}
