using System.IO;

namespace NFirmwareEditor.Firmware
{
	internal abstract class ImageMetadata
	{
		public string Name { get; protected set; }

		public byte Width { get; protected set; }

		public byte Height { get; protected set; }

		public long DataOffset { get; protected set; }

		public abstract long DataLength { get; }

		public ImageMetadata ReadMetadata(BinaryReader reader, int index)
		{
			Name = string.Format("[Char: 0x{0:X2}] 0x{1:X2} ", index, reader.BaseStream.Position);
			Width = reader.ReadByte();
			Height = reader.ReadByte();
			DataOffset = reader.BaseStream.Position;
			return this;
		}

		public abstract bool[,] ReadImage(byte[] imageBytes);

		public abstract byte[] WriteImage(bool[,] imageData);

		public override string ToString()
		{
			return Name;
		}
	}
}
