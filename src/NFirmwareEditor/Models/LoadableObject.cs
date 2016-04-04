using System.Xml.Serialization;

namespace NFirmwareEditor.Models
{
	public class LoadableObject : NamespacelessObject
	{
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
