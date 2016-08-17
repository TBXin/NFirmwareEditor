using System.Xml.Serialization;
using NFirmware;

namespace NFirmwareEditor.Models
{
	public class LoadableObject : NamespacelessObject
	{
		[XmlIgnore]
		public string FilePath { get; set; }

		[XmlIgnore]
		public string FileName { get; set; }

		[XmlAttribute]
		public string Definition { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public string Version { get; set; }

		[XmlAttribute]
		public string Author { get; set; }
	}
}
