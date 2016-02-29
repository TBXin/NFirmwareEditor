using System;
using System.Text;
using System.Xml.Serialization;

namespace NFirmwareEditor.Firmware
{
	public class ExportedImage
	{
		private bool[,] m_data;

		public ExportedImage()
		{
		}

		public ExportedImage(int index, bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			IndexString = index.ToString("X2");
			Width = imageData.GetLength(0);
			Height = imageData.GetLength(1);
			Data = imageData;
		}

		[XmlAttribute("Char")]
		public string IndexString { get; set; }

		[XmlAttribute]
		public int Width { get; set; }

		[XmlAttribute]
		public int Height { get; set; }

		[XmlElement("Image")]
		public string ImagePixels { get; set; }

		[XmlIgnore]
		public bool[,] Data
		{
			get
			{
				var result = new bool[Width, Height];
				if (string.IsNullOrEmpty(ImagePixels)) return result;

				var rows = ImagePixels.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				for (var row = 0; row < rows.Length; row++)
				{
					for (var col = 0; col < rows[row].Length; col++)
					{
						result[col, row] = rows[row][col].Equals('1');
					}
				}
				return result;
			}
			set
			{
				m_data = value;

				var sb = new StringBuilder();
				sb.AppendLine();
				for (var row = 0; row < Height; row++)
				{
					for (var col = 0; col < Width; col++)
					{
						sb.Append(m_data[col, row] ? "1" : "0");
					}
					sb.AppendLine();
				}
				ImagePixels = sb.ToString();
			}
		}
	}
}
