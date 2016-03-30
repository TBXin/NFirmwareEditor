using JetBrains.Annotations;

namespace NFirmwareEditor.Models
{
	internal class PatchModificationData
	{
		public PatchModificationData(long offset, [CanBeNull] byte? originalValue, [CanBeNull] byte? patchedValue)
		{
			Offset = offset;
			OriginalValue = originalValue;
			PatchedValue = patchedValue;
		}

		public long Offset { get; set; }

		[CanBeNull]
		public byte? OriginalValue { get; set; }

		[CanBeNull]
		public byte? PatchedValue { get; set; }
	}
}
