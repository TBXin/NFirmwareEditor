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
		public Firmware TryLoad([NotNull] string filePath, [ItemNotNull] [NotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (definitions == null) throw new ArgumentNullException("definitions");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			return TryLoad(File.ReadAllBytes(filePath), definitions);
		}

		[CanBeNull]
		public Firmware TryLoad([NotNull] byte[] firmwareBytes, [ItemNotNull] [NotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (firmwareBytes == null) throw new ArgumentNullException("firmwareBytes");
			if (definitions == null) throw new ArgumentNullException("definitions");

			bool wasEncrypted;
			var bytes = DecryptIfNecessary(firmwareBytes, out wasEncrypted);
			var definition = DetermineDefinition(bytes, definitions);
			return definition == null
				? null
				: Load(bytes, definition, wasEncrypted);
		}

		[CanBeNull]
		public Firmware TryLoadUsingDefinition([NotNull] string filePath, [NotNull] FirmwareDefinition definition)
		{
			return TryLoad(filePath, new[] { definition });
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
			bool wasEncrytpted;
			return DecryptIfNecessary(data, out wasEncrytpted);
		}

		private byte[] DecryptIfNecessary(byte[] firmwareBytes, out bool wasEncrypted)
		{
			wasEncrypted = IsFirmwareEncrypted(firmwareBytes);
			return wasEncrypted ? m_encoder.Decode(firmwareBytes) : firmwareBytes;
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
				var markers = definition.Markers;
				if (markers == null || markers.Length == 0) continue;

				var found = true;
				foreach (var marker in markers)
				{
					if (marker.Data == null || marker.Data.Length == 0) continue;

					var bytes = firmwareBytes.Skip((int)marker.Offset).Take(marker.Data.Length).ToArray();
					if (!marker.Data.SequenceEqual(bytes))
					{
						found = false;
						break;
					}
				}

				if (found) return definition;
			}
			return null;
		}

		internal bool IsFirmwareEncrypted([NotNull] byte[] firmwareBytes)
		{
			if (firmwareBytes == null) throw new ArgumentNullException("firmwareBytes");
			if (m_encryptedFirmwareMark.Length > firmwareBytes.Length) return false;

			var idx = FindByteArray(firmwareBytes, m_encryptedFirmwareMark);
			return idx == -1;
		}

		private Firmware Load([NotNull] byte[] data, [NotNull] FirmwareDefinition definition, bool isEncrypted)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (definition == null) throw new ArgumentNullException("definition");

			var imageBlocks = LoadImageBlocks(data, definition);
			var stringBlocks = LoadStringBlocks(data, definition);
			return new Firmware(data, imageBlocks, stringBlocks, definition, isEncrypted);
		}

		internal FirmwareImageBlocks LoadImageBlocks([NotNull] byte[] firmware, [NotNull] FirmwareDefinition definition)
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

		internal FirmwareStringBlocks LoadStringBlocks([NotNull] byte[] firmware, [NotNull] FirmwareDefinition definition)
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
				var offsetsTable = new List<Tuple<long, long>>();
				reader.BaseStream.Seek(imageTableDefinition.OffsetFrom, SeekOrigin.Begin);
				while (reader.BaseStream.Position <= imageTableDefinition.OffsetTo)
				{
					var imageTableOffset = reader.BaseStream.Position;
					var imageDataOffset = reader.ReadUInt32();

					offsetsTable.Add(new Tuple<long, long>(imageTableOffset, imageDataOffset));
				}

				for (var i = 0; i < offsetsTable.Count; i++)
				{
					var imageTableOffset = offsetsTable[i].Item1;
					var imageDataOffset = offsetsTable[i].Item2;
					if (imageDataOffset == 0) continue;

					reader.BaseStream.Seek(imageDataOffset, SeekOrigin.Begin);
					result.Add(new T().ReadMetadata(reader, imageTableOffset, i + 1));
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

		internal static int FindByteArray(byte[] source, byte[] searchedBytes)
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
