using System.Xml.Serialization;
using JetBrains.Annotations;

namespace NFirmware
{
	public class FirmwareDefinition
	{
		[XmlAttribute]
		public string Name { get; set; }

		[CanBeNull]
		[XmlElement("Marker", typeof(FirmwareMarkerDefinition))]
		public FirmwareMarkerDefinition[] Markers { get; set; }

		[CanBeNull]
		public ImageTableDefinition ImageTable1 { get; set; }

		[CanBeNull]
		public ImageTableDefinition ImageTable2 { get; set; }

		[CanBeNull]
		public StringTableDefinition StringTable1 { get; set; }

		[CanBeNull]
		public StringTableDefinition StringTable2 { get; set; }

		[CanBeNull]
		[XmlElement("CharsToCorrect")]
		public string CharsToCorrectString { get; set; }

		[CanBeNull]
		[XmlIgnore]
		public byte[] CharsToCorrect
		{
			get { return CharsToCorrectString.HexStringToByteArray(); }
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public class ImageTableDefinition : FirmwareTableDefinition
	{
	}

	public class StringTableDefinition : FirmwareTableDefinition
	{
	}

	public abstract class FirmwareTableDefinition
	{
		[XmlElement("OffsetFrom")]
		public string OffsetFromString { get; set; }

		[XmlElement("OffsetTo")]
		public string OffsetToString { get; set; }

		[XmlIgnore]
		public long OffsetFrom
		{
			get { return OffsetFromString.HexStringToLong(); }
		}

		[XmlIgnore]
		public long OffsetTo
		{
			get { return OffsetToString.HexStringToLong(); }
		}
	}

	public class FirmwareMarkerDefinition
	{
		[XmlAttribute("Offset")]
		public string OffsetFromString { get; set; }

		[XmlAttribute("Bytes")]
		public string MarkerBytesString { get; set; }

		[XmlIgnore]
		public long Offset
		{
			get { return OffsetFromString.HexStringToLong(); }
		}

		[CanBeNull]
		[XmlIgnore]
		public byte[] Data
		{
			get { return MarkerBytesString.HexStringToByteArray(); }
		}
	}
}
