using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace NFirmware
{
	public class FirmwareLoader
	{
		private readonly byte[] m_encryptedFirmwareMark = Encoding.Unicode.GetBytes("Nuvoton");
		private readonly FirmwareEncoder m_encoder;

		public FirmwareLoader([NotNull] FirmwareEncoder encoder)
		{
			if (encoder == null) throw new ArgumentNullException("encoder");
			m_encoder = encoder;
		}

		[CanBeNull]
		public Firmware TryLoad([NotNull] string filePath, [NotNull, ItemNotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			var bytes = LoadFile(filePath);
			var definition = DetermineDefinition(bytes, definitions);
			return definition != null ? Load(bytes, definition) : null;
		}

		[NotNull]
		public Firmware LoadEncrypted([NotNull] string filePath, [NotNull] FirmwareDefinition definition)
		{
			return Load(filePath, definition, true);
		}

		[NotNull]
		public Firmware LoadDecrypted([NotNull] string filePath, [NotNull] FirmwareDefinition definition)
		{
			return Load(filePath, definition, false);
		}

		public void SaveEncrypted([NotNull] string filePath, [NotNull] Firmware firmware)
		{
			Save(filePath, firmware, true);
		}

		public void SaveDecrypted([NotNull] string filePath, [NotNull] Firmware firmware)
		{
			Save(filePath, firmware, false);
		}

		internal byte[] LoadFile([NotNull] string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = File.ReadAllBytes(filePath);
			var decode = IsFirmwareEncrypted(data);
			return decode ? m_encoder.Decode(data) : data;
		}

		private void Save([NotNull] string filePath, Firmware firmware, bool encode)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = encode ? m_encoder.Encode(firmware.GetBody()) : firmware.GetBody();
			File.WriteAllBytes(filePath, data);
		}

		[CanBeNull]
		public FirmwareDefinition DetermineDefinition([NotNull] byte[] firmwareBytes, [NotNull, ItemNotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (firmwareBytes == null) throw new ArgumentNullException("firmwareBytes");
			if (definitions == null) throw new ArgumentNullException("definitions");

			foreach (var definition in definitions)
			{
				if (definition.Marker == null
				    || string.IsNullOrEmpty(definition.Marker.OffsetFromString)
				    || string.IsNullOrEmpty(definition.Marker.MarkerBytesString))
					continue;

				var bytes = firmwareBytes.Skip((int)definition.Marker.Offset).Take(definition.Marker.Marker.Length).ToArray();
				if (!definition.Marker.Marker.SequenceEqual(bytes)) continue;

				return definition;
			}
			return null;
		}

		private bool IsFirmwareEncrypted([NotNull] byte[] firmwareBytes)
		{
			if (firmwareBytes == null) throw new ArgumentNullException("firmwareBytes");
			if (m_encryptedFirmwareMark.Length > firmwareBytes.Length) return false;

			var idx = FindByteArray(firmwareBytes, m_encryptedFirmwareMark);
			return idx == -1;
		}

		private byte[] LoadFile([NotNull] string filePath, bool decode)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = File.ReadAllBytes(filePath);
			return decode ? m_encoder.Decode(data) : data;
		}

		private Firmware Load([NotNull] string filePath, [NotNull] FirmwareDefinition definition, bool decode)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = LoadFile(filePath, decode);
			return Load(data, definition);
		}

		private Firmware Load([NotNull] byte[] data, [NotNull] FirmwareDefinition definition)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (definition == null) throw new ArgumentNullException("definition");

			var imageBlocks = LoadImageBlocks(data, definition);
			var stringBlocks = LoadStringBlocks(data, definition);
			return new Firmware(data, imageBlocks, stringBlocks, definition);
		}

		private FirmwareImageBlocks LoadImageBlocks([NotNull] byte[] firmware, [NotNull] FirmwareDefinition definition)
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

		private FirmwareStringBlocks LoadStringBlocks([NotNull] byte[] firmware, [NotNull] FirmwareDefinition definition)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (definition == null) throw new ArgumentNullException("definition");

			using (var ms = new MemoryStream(firmware))
			{
				using (var reader = new BinaryReader(ms))
				{
					var block1String = ReadStringTable(definition.StringTable1, BlockType.Block1, reader);
					var block2Strings = ReadStringTable(definition.StringTable2, BlockType.Block2, reader);
					return new FirmwareStringBlocks(block1String, block2Strings);
				}
			}
		}

		private IEnumerable<FirmwareImageMetadata> ReadImageTable<T>([CanBeNull] ImageTableDefinition imageTableDefinition, [NotNull] BinaryReader reader) where T : FirmwareImageMetadata, new()
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

		private IEnumerable<FirmwareStringMetadata> ReadStringTable([CanBeNull] StringTableDefinition stringTableDefinition, BlockType blockType, [NotNull] BinaryReader reader)
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

					while (true)
					{
						if (reader.BaseStream.Position <= stringTableDefinition.OffsetTo)
						{
							dataLength++;
							var byte1 = reader.ReadByte();
							byte? byte2;
							if (reader.BaseStream.Position <= stringTableDefinition.OffsetTo)
							{
								byte2 = reader.ReadByte();
								reader.BaseStream.Position--;
							}
							else break;

							if (byte1 == 0x00 && byte2 != 0x00) break;
						}
						else break;
					}
					result.Add(new FirmwareStringMetadata(result.Count + 1, offset, dataLength, blockType));
				}
			}
			return result;
		}

		private static int FindByteArray(byte[] source, byte[] searchedBytes)
		{
			if (searchedBytes.Length > source.Length) return -1;

			for (var i = 0; i < source.Length - searchedBytes.Length; i++)
			{
				var match = true;
				for (var j = 0; j < searchedBytes.Length; j++)
				{
					if (source[i + j] != searchedBytes[j])
					{
						match = false;
						break;
					}
				}
				if (match) return i;
			}
			return -1;
		}
	}
}
