using System.Xml.Serialization;
using JetBrains.Annotations;

namespace NFirmware
{
	[XmlType("FirmwareDefinition")]
	public class FirmwareDefinition : NamespacelessObject
	{
		[XmlIgnore]
		internal string FileName { get; set; }

		[XmlIgnore]
		internal string Sha { get; set; }

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
		public StringsPreviewCorrection StringsPreviewCorrection { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class StringsPreviewCorrection
	{
		[XmlAttribute("ForGlyphs")]
		public string ForGlyphsString { get; set; }

		[CanBeNull]
		[XmlIgnore]
		public byte[] ForGlyphs
		{
			get { return ForGlyphsString.HexStringToByteArray(); }
		}
	}

	public class ImageTableDefinition : FirmwareTableDefinition
	{
	}

	public class StringTableDefinition : FirmwareTableDefinition
	{
		[XmlAttribute]
		public bool TwoBytesPerChar { get; set; }
	}

	public abstract class FirmwareTableDefinition
	{
		[XmlAttribute("From")]
		public string OffsetFromString { get; set; }

		[XmlAttribute("To")]
		public string OffsetToString { get; set; }

		[XmlAttribute("PtrFrom")]
		public string OffsetPtrFromString { get; set; }

		[XmlAttribute("PtrTo")]
		public string OffsetPtrToString { get; set; }

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

		[XmlIgnore]
		public long OffsetPtrFrom
		{
			get { return OffsetPtrFromString.HexStringToLong(); }
		}

		[XmlIgnore]
		public long OffsetPtrTo
		{
			get { return OffsetPtrToString.HexStringToLong(); }
		}

		[XmlIgnore]
		public bool IsPtrTable
		{
			get { return !string.IsNullOrEmpty(OffsetPtrFromString) && !string.IsNullOrEmpty(OffsetPtrToString); }
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
