namespace NFirmware
{
	public class FirmwareStringMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FirmwareStringMetadata"/> class.
		/// </summary>
		public FirmwareStringMetadata(int index, long dataOffset, long dataLength, BlockType blockType)
		{
			Index = index;
			DataOffset = dataOffset;
			DataLength = dataLength;
			BlockType = blockType;
		}

		/// <summary>
		/// Gets the string index.
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Gets the string absolute offset in the firmware.
		/// </summary>
		public long DataOffset { get; private set; }

		/// <summary>
		/// Gets the length of the byte array that represents a string.
		/// </summary>
		public long DataLength { get; private set; }

		/// <summary>
		/// Gets the block type.
		/// </summary>
		public BlockType BlockType { get; private set; }

		public override string ToString()
		{
			return string.Format("[Str: 0x{0:X2}] 0x{1:X2} ", Index, DataOffset);
		}
	}
}
