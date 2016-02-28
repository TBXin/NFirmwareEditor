using System.IO;

namespace NFirmwareEditor.Firmware
{
	/// <summary>
	/// Contains metadata of the firmware image.
	/// </summary>
	internal abstract class ImageMetadata
	{
		/// <summary>
		/// Gets the image name.
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets the image width.
		/// </summary>
		public byte Width { get; protected set; }

		/// <summary>
		/// Gets the image height.
		/// </summary>
		public byte Height { get; protected set; }

		/// <summary>
		/// Gets the image absolute offset in the firmware.
		/// </summary>
		public long DataOffset { get; protected set; }

		/// <summary>
		/// Gets the length of the byte array that represents an image.
		/// </summary>
		public abstract long DataLength { get; }

		/// <summary>
		/// Reads the image metadata at the specified offset.
		/// </summary>
		/// <param name="reader">Binary reader.</param>
		/// <param name="offset">Offset on which metadata is located.</param>
		public ImageMetadata ReadMetadata(BinaryReader reader, int offset)
		{
			Name = string.Format("[Char: 0x{0:X2}] 0x{1:X2} ", offset, reader.BaseStream.Position);
			Width = reader.ReadByte();
			Height = reader.ReadByte();
			DataOffset = reader.BaseStream.Position;
			return this;
		}

		/// <summary>
		/// Reads the image from bytes array and transforms to the two-dimensional bool array.
		/// </summary>
		/// <param name="imageBytes">Image bytes.</param>
		public abstract bool[,] ReadImage(byte[] imageBytes);

		/// <summary>
		/// Transforms two-dimensional bool array to the bytes array.
		/// </summary>
		/// <param name="imageData">Image data.</param>
		public abstract byte[] WriteImage(bool[,] imageData);

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="ImageMetadata"/>.
		/// </summary>
		public override string ToString()
		{
			return Name;
		}
	}
}
