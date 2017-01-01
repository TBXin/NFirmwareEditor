using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using NCore;

namespace NFirmware
{
	public class Firmware
	{
		private readonly FirmwareStream m_bodyStream;

		private FirmwareImageBlocks m_imageBlocks;
		private FirmwareStringBlocks m_stringBlocks;

		internal Firmware
		(
			[NotNull] byte[] body, 
			[NotNull] FirmwareImageBlocks imageBlocks, 
			[NotNull] FirmwareStringBlocks stringBlocks, 
			[NotNull] FirmwareDefinition definition,
			EncryptionType encryptionType
		)
		{
			if (body == null) throw new ArgumentNullException("body");
			if (imageBlocks == null) throw new ArgumentNullException("imageBlocks");
			if (stringBlocks == null) throw new ArgumentNullException("stringBlocks");
			if (definition == null) throw new ArgumentNullException("definition");

			Definition = definition;
			EncryptionType = encryptionType;
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

		public EncryptionType EncryptionType { get; internal set; }

		[NotNull]
		public IDictionary<int, FirmwareImageMetadata> Block1Images
		{
			get { return m_imageBlocks.Block1Images; }
		}

		[NotNull]
		public IDictionary<int, FirmwareImageMetadata> Block2Images
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
		public short[] ReadString([NotNull] FirmwareStringMetadata stringMetadata)
		{
			if (stringMetadata == null) throw new ArgumentNullException("stringMetadata");

			var stringBytes = m_bodyStream.ReadBytes((int)stringMetadata.DataOffset, (int)stringMetadata.DataLength);
			if (!stringMetadata.TwoByteChars)
			{
				return stringBytes.Select(x => (short)x).ToArray();
			}

			var stringChars = new short[(int)Math.Ceiling(stringBytes.Length / 2d)];
			Buffer.BlockCopy(stringBytes, 0, stringChars, 0, stringBytes.Length);
			return stringChars;
		}

		[NotNull]
		public IEnumerable<bool[,]> ReadImages([NotNull, ItemNotNull] IEnumerable<FirmwareImageMetadata> metadata)
		{
			if (metadata == null) throw new ArgumentNullException("metadata");
			var result = new List<bool[,]>();
			foreach (var imageMetadata in metadata)
			{
				result.Add(ReadImage(imageMetadata));
			}
			return result;
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

		public void WriteChar(short stringChar, int index, [NotNull] FirmwareStringMetadata stringMetadata)
		{
			if (stringMetadata == null) throw new ArgumentNullException("stringMetadata");
			if (index > stringMetadata.DataLength) throw new InvalidDataException("String data does not correspond to the metadata.");

			if (stringMetadata.TwoByteChars)
			{
				m_bodyStream.WriteBytes((int)stringMetadata.DataOffset + index * 2, BitConverter.GetBytes(stringChar));
			}
			else
			{
				m_bodyStream.WriteByte((int)stringMetadata.DataOffset + index, (byte)stringChar);
			}
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
