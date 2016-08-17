using System;
using System.Drawing;
using System.Xml.Serialization;
using NFirmware;

namespace NFirmwareEditor.Models
{
	public class ExportedImage : NamespacelessObject
	{
		public ExportedImage()
		{
		}

		internal ExportedImage(int index, Size imageSize, bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			Index = index;
			Width = imageSize.Width;
			Height = imageSize.Height;
			Data = imageData;
		}

		[XmlAttribute("Index")]
		public string IndexString { get; set; }

		[XmlAttribute]
		public int Width { get; set; }

		[XmlAttribute]
		public int Height { get; set; }

		[XmlElement("Data")]
		public string DataString { get; set; }

		internal int Index { get; set; }

		internal bool[,] Data { get; set; }
	}
}
