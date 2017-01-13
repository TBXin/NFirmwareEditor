using System.Xml.Serialization;
using NCore.Serialization;

namespace NToolbox.Models.Export
{
	public class BatteryProfile : NamespacelessObject
	{
		public decimal Cutoff { get; set; }

		[XmlArray("Data"), XmlArrayItem("Point")]
		public BatteryProfilePoint[] Points { get; set; }
	}

	public class BatteryProfilePoint
	{
		[XmlAttribute]
		public byte Percent { get; set; }

		[XmlAttribute]
		public decimal Voltage { get; set; }
	}
}
