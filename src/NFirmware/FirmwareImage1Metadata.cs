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
			get { return Width * ((Height + 7) / 8); }
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
			var stride = Width;
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					var byteIndex = y / 8 * stride + x;
					var bitIndex = y % 8;

					result[x, y] = (imageBytes[byteIndex] & (1 << bitIndex)) != 0;
				}
			}
			return result;
		}

		/// <summary>
		/// Transforms two-dimensional bool array to the bytes array.
		/// </summary>
		/// <param name="imageData">Image data.</param>
		public override byte[] Save(bool[,] imageData)
		{
			var imageBytes = CreateImageDataWithHeader();
			var stride = Width;
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					if (imageData[x, y] == false) continue;

					var byteIndex = y / 8 * stride + x;
					var bitIndex = y % 8;

					imageBytes[HeaderLength + byteIndex] |= (byte)(1 << bitIndex & 0xFF);
				}
			}
			return imageBytes;
		}
	}
}
