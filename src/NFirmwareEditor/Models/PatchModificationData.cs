namespace NFirmwareEditor.Models
{
	internal class PatchModificationData
	{
		public PatchModificationData(long offset, byte? originalValue, byte patchedValue)
		{
			Offset = offset;
			OriginalValue = originalValue;
			PatchedValue = patchedValue;
		}

		public long Offset { get; set; }

		public byte? OriginalValue { get; set; }

		public byte PatchedValue { get; set; }
	}
}
