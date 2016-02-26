using System.Collections;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Firmware
{
	internal class Image1Metadata : ImageMetadata
	{
		public override long DataLength
		{
			get { return Width * Height / 8; }
		}

		public override bool[,] ReadImage(byte[] imageBytes)
		{
			var result = new bool[Width, Height];
			var colCounter = 0;
			var rowCounter = 0;

			foreach (var imageByte in imageBytes)
			{
				var bits = new BitArray(new[] { imageByte }).ToBoolArray();

				if (colCounter == Width)
				{
					colCounter = 0;
					rowCounter += bits.Length;
				}

				for (var j = 0; j < bits.Length; j++)
				{
					result[colCounter, rowCounter + j] = bits[j];
				}

				colCounter++;
			}
			return result;
		}

		public override byte[] WriteImage(bool[,] imageData)
		{
			var imageBytes = new byte[DataLength];
			var imageBytesCounter = 0;
			var rowCounter = 0;

			while (rowCounter < Height)
			{
				var byteBits = new bool[8];
				for (var col = 0; col < Width; col++)
				{
					for (var row = 0; row < byteBits.Length; row++)
					{
						byteBits[row] = imageData[col, rowCounter + row];
					}
					imageBytes[imageBytesCounter++] = new BitArray(byteBits).ToByte();
				}
				rowCounter += byteBits.Length;
			}
			return imageBytes;
		}
	}
}
