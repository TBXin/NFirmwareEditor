using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace NFirmware
{
	public class Firmware
	{
		private readonly FirmwareImageBlocks m_imageBlocks;
		private readonly FirmwareStringBlocks m_stringBlocks;

		internal Firmware([NotNull] byte[] body, [NotNull] FirmwareImageBlocks imageBlocks, [NotNull] FirmwareStringBlocks stringBlocks)
		{
			if (body == null) throw new ArgumentNullException("body");
			if (imageBlocks == null) throw new ArgumentNullException("imageBlocks");
			if (stringBlocks == null) throw new ArgumentNullException("stringBlocks");

			Body = body;
			m_imageBlocks = imageBlocks;
			m_stringBlocks = stringBlocks;
		}

		[NotNull]
		internal byte[] Body { get; set; }

		[NotNull, ItemNotNull]
		public IEnumerable<FirmwareImageMetadata> Block1Images
		{
			get { return m_imageBlocks.Block1Images; }
		}

		[NotNull, ItemNotNull]
		public IEnumerable<FirmwareImageMetadata> Block2Images
		{
			get { return m_imageBlocks.Block2Images; }
		}

		public IEnumerable<FirmwareStringMetadata> Block1Strings
		{
			get { return m_stringBlocks.Block1Strings; }
		}

		public IEnumerable<FirmwareStringMetadata> Block2Strings
		{
			get { return m_stringBlocks.Block2Strings; }
		}

		[NotNull]
		public bool[,] ReadImage([NotNull] FirmwareImageMetadata imageMetadata)
		{
			if (imageMetadata == null) throw new ArgumentNullException("imageMetadata");

			var imageData = GetImageBytes(Body, imageMetadata);
			return imageMetadata.Load(imageData);
		}

		[NotNull]
		public IEnumerable<bool[,]> ReadImages([NotNull, ItemNotNull] IEnumerable<FirmwareImageMetadata> metadata)
		{
			if (metadata == null) throw new ArgumentNullException("metadata");
			return metadata.Select(ReadImage).ToList();
		}

		public void WriteImage([NotNull] bool[,] imageData, [NotNull] FirmwareImageMetadata imageMetadata)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (imageMetadata == null) throw new ArgumentNullException("imageMetadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != imageMetadata.Width || height != imageMetadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var imageBytes = imageMetadata.Save(imageData);
			Buffer.BlockCopy(imageBytes, 0, Body, (int)imageMetadata.DataOffset, imageBytes.Length);
		}

		[NotNull]
		private static byte[] GetImageBytes([NotNull] IEnumerable<byte> firmware, [NotNull] FirmwareImageMetadata metadata)
		{
			return firmware
				.Skip((int)metadata.DataOffset)
				.Take((int)metadata.DataLength)
				.ToArray();
		}
	}
}
