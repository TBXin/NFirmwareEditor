using JetBrains.Annotations;

namespace NFirmwareEditor.Models
{
	internal class PatchModificationData
	{
		public PatchModificationData(long offset, [CanBeNull] byte? patchedValue)
		{
			Offset = offset;
			IgnoreOriginalValue = true;
			PatchedValue = patchedValue;
		}

		public PatchModificationData(long offset, [CanBeNull] byte? originalValue, [CanBeNull] byte? patchedValue)
		{
			Offset = offset;
			OriginalValue = originalValue;
			PatchedValue = patchedValue;
		}

		public long Offset { get; set; }

		public bool IgnoreOriginalValue { get; private set; }

		[CanBeNull]
		public byte? OriginalValue { get; private set; }

		[CanBeNull]
		public byte? PatchedValue { get; private set; }
	}
}
