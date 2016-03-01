using System;
using System.Collections;
using System.Linq;

namespace NFirmware
{
	/// <summary>
	/// Represents metadata of the image for LCD-display SSD1327.
	/// </summary>
	public class FirmwareImage2Metadata : FirmwareImageMetadata
	{
		/// <summary>
		/// Gets the length of the byte array that represents an image.
		/// </summary>
		public override long DataLength
		{
			get { return (int)Math.Ceiling(Width / 8f) * Height; }
		}

		/// <summary>
		/// Reads the image from bytes array and transforms to the two-dimensional bool array.
		/// </summary>
		/// <param name="imageBytes">Image bytes.</param>
		public override bool[,] Load(byte[] imageBytes)
		{
			var result = new bool[Width, Height];
			var colCounter = 0;
			var rowCounter = 0;

			foreach (var imageByte in imageBytes)
			{
				var bitArray = new BitArray(new[] { imageByte }).ToBoolArray().Reverse();
				foreach (var b in bitArray)
				{
					result[colCounter++, rowCounter] = b;
					if (colCounter == Width)
					{
						colCounter = 0;
						rowCounter++;
						break;
					}
				}

				if (rowCounter == Height) break;
			}
			return result;
		}

		/// <summary>
		/// Transforms two-dimensional bool array to the bytes array.
		/// </summary>
		/// <param name="imageData">Image data.</param>
		public override byte[] Save(bool[,] imageData)
		{
			var imageBytes = new byte[DataLength];
			var imageBytesCounter = 0;
			var colCounter = 0;

			for (var row = 0; row < Height; row++)
			{
				var byteBits = new bool[8];
				for (var col = 0; col < Width; col++)
				{
					byteBits[colCounter++] = imageData[col, row];
					if (colCounter == 8)
					{
						imageBytes[imageBytesCounter++] = new BitArray(byteBits.Reverse().ToArray()).ToByte();
						byteBits = new bool[8];
						colCounter = 0;
					}
				}

				if (colCounter > 0)
				{
					imageBytes[imageBytesCounter++] = new BitArray(byteBits.Reverse().ToArray()).ToByte();
					colCounter = 0;
				}
			}
			return imageBytes;
		}
	}
}
