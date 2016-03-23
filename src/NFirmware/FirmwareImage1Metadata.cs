using System;
using System.Collections;

namespace NFirmware
{
	/// <summary>
	/// Represents metadata of the image for LCD-display SSD1306.
	/// </summary>
	public class FirmwareImage1Metadata : FirmwareImageMetadata
	{
		/// <summary>
		/// Gets the length of the byte array that represents an image.
		/// </summary>
		public override long DataLength
		{
			get { return Width * (int)Math.Ceiling(Height / 8f); }
		}

		/// <summary>
		/// Gets the block type.
		/// </summary>
		public override BlockType BlockType
		{
			get { return BlockType.Block1; }
		}

		/// <summary>
		/// Loads the image from bytes array and transforms to the two-dimensional bool array.
		/// </summary>
		/// <param name="imageBytes">Image bytes.</param>
		internal override bool[,] Load(byte[] imageBytes)
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
					if (rowCounter + j >= Height) break;
					result[colCounter, rowCounter + j] = bits[j];
				}

				colCounter++;
			}
			return result;
		}

		/// <summary>
		/// Transforms two-dimensional bool array to the bytes array.
		/// </summary>
		/// <param name="imageData">Image data.</param>
		internal override byte[] Save(bool[,] imageData)
		{
			var imageBytes = CreateEmptyImage();
			var imageBytesCounter = HeaderLength;
			var rowCounter = 0;

			while (rowCounter < Height)
			{
				var byteBits = new bool[8];
				for (var col = 0; col < Width; col++)
				{
					for (var row = 0; row < byteBits.Length; row++)
					{
						if (rowCounter + row >= Height) break;
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
