using System;
using System.Collections.Generic;
using System.IO;

namespace NFirmware
{
	public class FirmwareLoader
	{
		private readonly FirmwareEncoder m_encoder;

		public FirmwareLoader(FirmwareEncoder encoder)
		{
			if (encoder == null) throw new ArgumentNullException("encoder");
			m_encoder = encoder;
		}

		public Firmware LoadEncrypted(string filePath, FirmwareDefinition definition)
		{
			return Load(filePath, definition, true);
		}

		public Firmware LoadDecrypted(string filePath, FirmwareDefinition definition)
		{
			return Load(filePath, definition, false);
		}

		public void SaveEncrypted(string filePath, Firmware firmware)
		{
			Save(filePath, firmware, true);
		}

		public void SaveDecrypted(string filePath, Firmware firmware)
		{
			Save(filePath, firmware, false);
		}

		private void Save(string filePath, Firmware firmware, bool encode)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = encode ? m_encoder.Encode(firmware.Body) : firmware.Body;
			File.WriteAllBytes(filePath, data);
		}

		private Firmware Load(string filePath, FirmwareDefinition definition, bool decode)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = File.ReadAllBytes(filePath);
			data = decode ? m_encoder.Decode(data) : data;

			var imageBlocks = LoadImageBlocks(data, definition);
			var stringBlocks = LoadStringBlocks(data, definition);
			return new Firmware(data, imageBlocks, stringBlocks);
		}

		private FirmwareImageBlocks LoadImageBlocks(byte[] firmware, FirmwareDefinition definition)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (definition == null) throw new ArgumentNullException("definition");

			using (var ms = new MemoryStream(firmware))
			{
				using (var reader = new BinaryReader(ms))
				{
					var block1Images = ReadImageTable<FirmwareImage1Metadata>(definition.ImageTable1, reader);
					var block2Images = ReadImageTable<FirmwareImage2Metadata>(definition.ImageTable2, reader);
					return new FirmwareImageBlocks(block1Images, block2Images);
				}
			}
		}

		private FirmwareStringBlocks LoadStringBlocks(byte[] firmware, FirmwareDefinition definition)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (definition == null) throw new ArgumentNullException("definition");

			using (var ms = new MemoryStream(firmware))
			{
				using (var reader = new BinaryReader(ms))
				{
					var block1String = ReadStringTable(definition.StringTable1, reader);
					var block2Strings = ReadStringTable(definition.StringTable2, reader);
					return new FirmwareStringBlocks(block1String, block2Strings);
				}
			}
		}

		private IEnumerable<FirmwareImageMetadata> ReadImageTable<T>(ImageTableDefinition imageTableDefinition, BinaryReader reader) where T : FirmwareImageMetadata, new()
		{
			if (imageTableDefinition == null) return new List<FirmwareImageMetadata>();
			if (reader == null) throw new ArgumentNullException("reader");

			var result = new List<FirmwareImageMetadata>();
			{
				var offsetsTable = new List<long>();
				reader.BaseStream.Seek(imageTableDefinition.OffsetFrom, SeekOrigin.Begin);
				while (reader.BaseStream.Position <= imageTableDefinition.OffsetTo)
				{
					var offset = reader.ReadUInt32();
					if (offset == 0) continue;

					offsetsTable.Add(offset);
				}

				for (var i = 0; i < offsetsTable.Count; i++)
				{
					reader.BaseStream.Seek(offsetsTable[i], SeekOrigin.Begin);
					result.Add(new T().ReadMetadata(reader, i + 1));
				}
			}
			return result;
		}

		private IEnumerable<FirmwareStringMetadata> ReadStringTable(StringTableDefinition stringTableDefinition, BinaryReader reader)
		{
			if (stringTableDefinition == null) return new List<FirmwareStringMetadata>();
			if (reader == null) throw new ArgumentNullException("reader");

			var result = new List<FirmwareStringMetadata>();
			{
				reader.BaseStream.Seek(stringTableDefinition.OffsetFrom, SeekOrigin.Begin);
				while (reader.BaseStream.Position <= stringTableDefinition.OffsetTo)
				{
					var offset = reader.BaseStream.Position;
					var dataLength = 0;
					while (reader.ReadByte() != 0 && reader.BaseStream.Position <= stringTableDefinition.OffsetTo)
					{
						dataLength++;
					}
					result.Add(new FirmwareStringMetadata(result.Count + 1, offset, dataLength));
				}
			}
			return result;
		}
	}
}
