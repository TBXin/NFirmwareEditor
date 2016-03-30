using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NFirmwareEditor.Models
{
	internal class BulkPatchResult
	{
		public BulkPatchResult([NotNull] List<Patch> proceededPatches, [NotNull] List<Patch> conflictedPatches)
		{
			if (proceededPatches == null) throw new ArgumentNullException("proceededPatches");
			if (conflictedPatches == null) throw new ArgumentNullException("conflictedPatches");

			ProceededPatches = proceededPatches;
			ConflictedPatches = conflictedPatches;
		}

		[NotNull]
		public List<Patch> ProceededPatches { get; private set; }

		[NotNull]
		public List<Patch> ConflictedPatches { get; private set; }
	}
}
