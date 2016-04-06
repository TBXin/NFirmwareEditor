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
		public string FilePath { get; set; }

		public string Description { get; set; }
	}
}
