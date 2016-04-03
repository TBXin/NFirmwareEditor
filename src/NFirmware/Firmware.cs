using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace NFirmware
{
	public class Firmware
	{
		private readonly FirmwareStream m_bodyStream;

		private FirmwareImageBlocks m_imageBlocks;
		private FirmwareStringBlocks m_stringBlocks;

		internal Firmware([NotNull] byte[] body, [NotNull] FirmwareImageBlocks imageBlocks, [NotNull] FirmwareStringBlocks stringBlocks, [NotNull] FirmwareDefinition definition)
		{
			if (body == null) throw new ArgumentNullException("body");
			if (imageBlocks == null) throw new ArgumentNullException("imageBlocks");
			if (stringBlocks == null) throw new ArgumentNullException("stringBlocks");
			if (definition == null) throw new ArgumentNullException("definition");

			Definition = definition;
			m_bodyStream = new FirmwareStream(body);
			m_imageBlocks = imageBlocks;
			m_stringBlocks = stringBlocks;
		}

		[NotNull]
		public FirmwareStream BodyStream
		{
			get { return m_bodyStream; }
		}

		[NotNull]
		public FirmwareDefinition Definition { get; private set; }

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

		[NotNull, ItemNotNull]
		public IEnumerable<FirmwareStringMetadata> Block1Strings
		{
			get { return m_stringBlocks.Block1Strings; }
		}

		[NotNull, ItemNotNull]
		public IEnumerable<FirmwareStringMetadata> Block2Strings
		{
			get { return m_stringBlocks.Block2Strings; }
		}

		[NotNull]
		public byte[] GetBody()
		{
			return m_bodyStream.ToArray();
		}

		[NotNull]
		public bool[,] ReadImage([NotNull] FirmwareImageMetadata imageMetadata)
		{
			if (imageMetadata == null) throw new ArgumentNullException("imageMetadata");

			var imageData = m_bodyStream.ReadBytes((int)imageMetadata.DataOffset + FirmwareImageMetadata.HeaderLength, (int)imageMetadata.DataLength);
			return imageMetadata.Load(imageData);
		}

		[NotNull]
		public byte[] ReadString([NotNull] FirmwareStringMetadata stringMetadata)
		{
			if (stringMetadata == null) throw new ArgumentNullException("stringMetadata");
			return m_bodyStream.ReadBytes((int)stringMetadata.DataOffset, (int)stringMetadata.DataLength);
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
			m_bodyStream.WriteBytes((int)imageMetadata.DataOffset, imageBytes);
		}

		public void WriteString([NotNull] byte[] stringData, [NotNull] FirmwareStringMetadata stringMetadata)
		{
			if (stringData == null) throw new ArgumentNullException("stringData");
			if (stringMetadata == null) throw new ArgumentNullException("stringMetadata");
			if (stringData.Length != stringMetadata.DataLength) throw new InvalidDataException("String data does not correspond to the metadata.");

			m_bodyStream.WriteBytes((int)stringMetadata.DataOffset, stringData);
		}

		public void WriteChar(byte stringChar, int index, [NotNull] FirmwareStringMetadata stringMetadata)
		{
			if (stringMetadata == null) throw new ArgumentNullException("stringMetadata");
			if (index > stringMetadata.DataLength) throw new InvalidDataException("String data does not correspond to the metadata.");

			m_bodyStream.WriteByte((int)stringMetadata.DataOffset + index, stringChar);
		}

		internal void ReloadResources([NotNull] FirmwareLoader loader)
		{
			if (loader == null) throw new ArgumentNullException("loader");

			var firmwareBytes = GetBody();
			m_imageBlocks = loader.LoadImageBlocks(firmwareBytes, Definition);
			m_stringBlocks = loader.LoadStringBlocks(firmwareBytes, Definition);
		}
	}
}
