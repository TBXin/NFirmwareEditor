using System.Xml.Serialization;

namespace NFirmwareEditor.Models
{
	[XmlRoot("ResourcePack")]
	public class ResourcePackFile : LoadableObject
	{
		public ResourcePackFile()
		{
		}

		[XmlIgnore]
		public string FileName { get; set; }

		[XmlIgnore]
		public string[] SuitableDefinitions { get; set; }

		public string Description { get; set; }
	}
}
