using System.Collections.Generic;

namespace NFirmware
{
	internal class FirmwareImageBlocks
	{
		internal FirmwareImageBlocks(IEnumerable<FirmwareImageMetadata> block1Images, IEnumerable<FirmwareImageMetadata> block2Images)
		{
			Block1Images = block1Images ?? new List<FirmwareImageMetadata>();
			Block2Images = block2Images ?? new List<FirmwareImageMetadata>();
		}

		internal IEnumerable<FirmwareImageMetadata> Block1Images { get; private set; }

		internal IEnumerable<FirmwareImageMetadata> Block2Images { get; private set; }
	}
}
