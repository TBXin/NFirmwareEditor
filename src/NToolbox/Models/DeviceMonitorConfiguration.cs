using System.Xml.Serialization;
using NCore.Serialization;

namespace NToolbox.Models
{
	[XmlType("DeviceMonitorConfiguration")]
	public class DeviceMonitorConfiguration : NamespacelessObject
	{
		public DeviceMonitorConfiguration()
		{
			ActiveSeries = new SerializableDictionary<string, bool>();
		}

		public SerializableDictionary<string, bool> ActiveSeries { get; set; }
	}
}
