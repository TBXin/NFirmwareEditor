using System.Collections.Generic;

namespace NFirmware
{
	internal class FirmwareStringBlocks
	{
		internal FirmwareStringBlocks(IEnumerable<FirmwareStringMetadata> block1Images, IEnumerable<FirmwareStringMetadata> block2Images)
		{
			Block1Strings = block1Images ?? new List<FirmwareStringMetadata>();
			Block2Strings = block2Images ?? new List<FirmwareStringMetadata>();
		}

		internal IEnumerable<FirmwareStringMetadata> Block1Strings { get; private set; }

		internal IEnumerable<FirmwareStringMetadata> Block2Strings { get; private set; }
	}
}
