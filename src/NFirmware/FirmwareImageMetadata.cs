using System.IO;

namespace NFirmware
{
	/// <summary>
	/// Contains metadata of the firmware image.
	/// </summary>
	public abstract class FirmwareImageMetadata
	{
		internal const int HeaderLength = 2;

		/// <summary>
		/// Gets the image index.
		/// </summary>
		public int Index { get; protected set; }

		/// <summary>
		/// Gets the image width.
		/// </summary>
		public byte Width { get; set; }

		/// <summary>
		/// Gets the image height.
		/// </summary>
		public byte Height { get; set; }

		/// <summary>
		/// Gets the image absolute offset in the firmware.
		/// </summary>
		public long DataOffset { get; protected set; }

		/// <summary>
		/// Gets the length of the byte array that represents an image.
		/// </summary>
		public abstract long DataLength { get; }

		/// <summary>
		/// Gets the block type.
		/// </summary>
		public abstract BlockType BlockType { get; }

		/// <summary>
		/// Reads the image metadata at the specified offset.
		/// </summary>
		/// <param name="reader">Binary reader.</param>
		/// <param name="imageIndex">Character generator index.</param>
		internal FirmwareImageMetadata ReadMetadata(BinaryReader reader, int imageIndex)
		{
			DataOffset = reader.BaseStream.Position;
			Index = imageIndex;
			Width = reader.ReadByte();
			Height = reader.ReadByte();
			return this;
		}

		internal byte[] CreateImageDataWithHeader()
		{
			var result = new byte[DataLength + HeaderLength];
			{
				result[0] = Width;
				result[1] = Height;
			}
			return result;
		}

		/// <summary>
		/// Reads the image from bytes array and transforms to the two-dimensional bool array.
		/// </summary>
		/// <param name="imageBytes">Image bytes.</param>
		internal abstract bool[,] Load(byte[] imageBytes);

		/// <summary>
		/// Transforms two-dimensional bool array to the bytes array.
		/// </summary>
		/// <param name="imageData">Image data.</param>
		internal abstract byte[] Save(bool[,] imageData);

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="FirmwareImageMetadata"/>.
		/// </summary>
		public override string ToString()
		{
			return string.Format("[Img: 0x{0:X2}] 0x{1:X2} ", Index, DataOffset);
		}
	}
}
