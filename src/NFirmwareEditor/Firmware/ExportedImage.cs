using System;
using System.Text;
using System.Xml.Serialization;

namespace NFirmwareEditor.Firmware
{
	public class ExportedImage
	{
		private const char TrueChar = 'X';
		private const char FalseChar = '.';
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
		public string ImageAsciiString { get; set; }

		[XmlIgnore]
		public bool[,] Data
		{
			get { return ReadImageFromAsciiString(Width, Height, ImageAsciiString); }
			set
			{
				m_data = value;
				ImageAsciiString = WriteImageToAsciiString(Width, Height, m_data);
			}
		}

		private static bool[,] ReadImageFromAsciiString(int width, int height, string text)
		{
			var result = new bool[width, height];
			if (string.IsNullOrEmpty(text)) return result;

			var rows = text.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			for (var row = 0; row < rows.Length; row++)
			{
				for (var col = 0; col < rows[row].Length; col++)
				{
					result[col, row] = IsTrue(rows[row][col]);
				}
			}
			return result;
		}

		private static string WriteImageToAsciiString(int width, int height, bool[,] data)
		{
			var sb = new StringBuilder();
			sb.AppendLine();
			for (var row = 0; row < height; row++)
			{
				for (var col = 0; col < width; col++)
				{
					sb.Append(data[col, row] ? TrueChar : FalseChar);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		private static bool IsTrue(char c)
		{
			return c.Equals(TrueChar) || c.Equals('1');
		}
	}
}
