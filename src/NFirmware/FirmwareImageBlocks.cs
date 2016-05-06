using System.Collections.Generic;

namespace NFirmware
{
	internal class FirmwareImageBlocks
	{
		internal FirmwareImageBlocks(IDictionary<int, FirmwareImageMetadata> block1Images, IDictionary<int, FirmwareImageMetadata> block2Images)
		{
			Block1Images = block1Images ?? new Dictionary<int, FirmwareImageMetadata>();
			Block2Images = block2Images ?? new Dictionary<int, FirmwareImageMetadata>();
		}

		internal IDictionary<int, FirmwareImageMetadata> Block1Images { get; private set; }

		internal IDictionary<int, FirmwareImageMetadata> Block2Images { get; private set; }
	}
}
