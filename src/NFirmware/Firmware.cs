using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NFirmware
{
	public class Firmware
	{
		private readonly FirmwareImageBlocks m_imageBlocks;

		internal Firmware(byte[] body, FirmwareImageBlocks imageBlocks)
		{
			if (body == null) throw new ArgumentNullException("body");
			if (imageBlocks == null) throw new ArgumentNullException("imageBlocks");

			Body = body;
			m_imageBlocks = imageBlocks;
		}

		internal byte[] Body { get; set; }

		public IEnumerable<FirmwareImageMetadata> Block1Images
		{
			get { return m_imageBlocks.Block1Images; }
		}

		public IEnumerable<FirmwareImageMetadata> Block2Images
		{
			get { return m_imageBlocks.Block2Images; }
		}

		public bool[,] ReadImage(FirmwareImageMetadata imageMetadata)
		{
			if (imageMetadata == null) throw new ArgumentNullException("imageMetadata");

			var imageData = GetImageBytes(Body, imageMetadata);
			return imageMetadata.Load(imageData);
		}

		public IEnumerable<bool[,]> ReadImages(IEnumerable<FirmwareImageMetadata> metadata)
		{
			if (metadata == null) throw new ArgumentNullException("metadata");
			return metadata.Select(ReadImage).ToList();
		}

		public void WriteImage(bool[,] imageData, FirmwareImageMetadata imageMetadata)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (imageMetadata == null) throw new ArgumentNullException("imageMetadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != imageMetadata.Width || height != imageMetadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var imageBytes = imageMetadata.Save(imageData);
			Buffer.BlockCopy(imageBytes, 0, Body, (int)imageMetadata.DataOffset, imageBytes.Length);
		}

		private static byte[] GetImageBytes(IEnumerable<byte> firmware, FirmwareImageMetadata metadata)
		{
			return firmware
				.Skip((int)metadata.DataOffset)
				.Take((int)metadata.DataLength)
				.ToArray();
		}
	}
}
