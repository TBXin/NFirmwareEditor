namespace NFirmware
{
	public class FirmwareStringMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FirmwareStringMetadata"/> class.
		/// </summary>
		public FirmwareStringMetadata(long dataOffset, long dataLength)
		{
			DataOffset = dataOffset;
			DataLength = dataLength;
		}

		/// <summary>
		/// Gets the string absolute offset in the firmware.
		/// </summary>
		public long DataOffset { get; private set; }

		/// <summary>
		/// Gets the length of the byte array that represents a string.
		/// </summary>
		public long DataLength { get; private set; }
	}
}
