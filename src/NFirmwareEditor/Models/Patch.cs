using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFirmwareEditor.Models
{
	public class Patch : LoadableObject
	{
		public string Description { get; set; }

		[XmlElement("Data")]
		public string DataString { get; set; }

		[XmlIgnore]
		internal IEnumerable<PatchModificationData> Data { get; set; }

		[XmlIgnore]
		internal bool IsApplied { get; set; }

		[XmlIgnore]
		internal bool IsCompatible { get; set; }

		[XmlIgnore]
		internal string Sha { get; set; }

		public override string ToString()
		{
			return Name + " v" + Version;
		}
	}
}
