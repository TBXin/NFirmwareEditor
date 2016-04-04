using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace NFirmwareEditor.Models
{
	public class ResourcePack : LoadableObject
	{
		public ResourcePack()
		{
		}

		public ResourcePack(string definition, [NotNull] List<ExportedImage> images)
		{
			if (string.IsNullOrEmpty(definition)) throw new ArgumentNullException("definition");
			if (images == null) throw new ArgumentNullException("images");

			Definition = definition;
			Images = images;
		}

		[CanBeNull]
		[XmlArray("Images")]
		[XmlArrayItem("Image")]
		public List<ExportedImage> Images { get; set; }
	}
}
