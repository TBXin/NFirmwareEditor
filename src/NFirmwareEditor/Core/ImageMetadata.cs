namespace NFirmwareEditor.Core
{
	internal class ImageMetadata
	{
		public ImageMetadata(string name, byte width, byte height, long dataOffset, long dataLength)
		{
			Name = name;
			Width = width;
			Height = height;
			DataOffset = dataOffset;
			DataLength = dataLength;
		}

		public string Name { get; set; }

		public byte Width { get; set; }

		public byte Height { get; set; }

		public long DataOffset { get; set; }

		public long DataLength { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
