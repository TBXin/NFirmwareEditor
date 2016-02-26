using System.Globalization;
using System.Xml.Serialization;

namespace NFirmwareEditor.Firmware
{
	public class FirmwareDefinition
	{
		[XmlAttribute]
		public string Name { get; set; }

		public ImageTableDefinition ImageTable1 { get; set; }

		public ImageTableDefinition ImageTable2 { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class ImageTableDefinition
	{
		[XmlElement("OffsetFrom")]
		public string OffsetFromString { get; set; }

		[XmlElement("OffsetTo")]
		public string OffsetToString { get; set; }

		[XmlIgnore]
		public long OffsetFrom
		{
			get { return HexStringToLong(OffsetFromString); }
		}

		[XmlIgnore]
		public long OffsetTo
		{
			get { return HexStringToLong(OffsetToString); }
		}

		private static long HexStringToLong(string hexNumber)
		{
			if (hexNumber.StartsWith("0x")) hexNumber = hexNumber.Substring(2);
			return long.Parse(hexNumber, NumberStyles.AllowHexSpecifier);
		}
	}
}