using System.Collections.Generic;

namespace NFirmwareEditor.Firmware
{
	internal class FirmwareImages
	{
		public FirmwareImages(List<ImageMetadata> block1Images, List<ImageMetadata> block2Images)
		{
			Block1Images = block1Images;
			Block2Images = block2Images;
		}

		public List<ImageMetadata> Block1Images { get; private set; }

		public List<ImageMetadata> Block2Images { get; private set; }
	}
}
