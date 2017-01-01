using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NCore;

namespace NFirmware
{
	public class FirmwareLoader
	{
		private static readonly IDictionary<EncryptionType, IEncryption> s_encryptors = new Dictionary<EncryptionType, IEncryption>
		{
			{ EncryptionType.Joyetech, new JoyetechEncryption() },
			{ EncryptionType.ArcticFox, new ArcticFoxEncryption() }
		};

		private readonly byte[] m_encryptedFirmwareMark = Encoding.ASCII.GetBytes("Joyetech APROM");

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

			EncryptionType encryptionType;
			var bytes = DecryptIfNecessary(firmwareBytes, out encryptionType);
			var definition = DetermineDefinition(bytes, definitions) ?? FirmwareAnalyzer.Analyze(bytes);

			return definition == null
				? null
				: Load(bytes, definition, encryptionType);
		}

		[CanBeNull]
		public Firmware TryLoadUsingDefinition([NotNull] string filePath, [NotNull] FirmwareDefinition definition)
		{
			return TryLoad(filePath, new[] { definition });
		}

		public void SaveEncrypted([NotNull] string filePath, [NotNull] Firmware firmware)
		{
			Save(filePath, firmware, firmware.EncryptionType);
		}

		public void SaveDecrypted([NotNull] string filePath, [NotNull] Firmware firmware)
		{
			Save(filePath, firmware, EncryptionType.None);
		}

		public byte[] LoadFile([NotNull] string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = File.ReadAllBytes(filePath);
			EncryptionType encryptionType;
			return DecryptIfNecessary(data, out encryptionType);
		}

		private byte[] DecryptIfNecessary(byte[] firmwareBytes, out EncryptionType encryptionType)
		{
			foreach (var encryptor in s_encryptors.Values)
			{
				var result = encryptor.Decode(firmwareBytes);
				if (!IsFirmwareEncrypted(result))
				{
					encryptionType = encryptor.Type;
					return result;
				}
			}

			encryptionType = EncryptionType.None;
			return firmwareBytes;
		}

		private void Save([NotNull] string filePath, Firmware firmware, EncryptionType encryptionType)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			var data = s_encryptors.ContainsKey(encryptionType)
				? s_encryptors[encryptionType].Encode(firmware.GetBody())
				: firmware.GetBody();

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

			var idx = firmwareBytes.FindByteArray(m_encryptedFirmwareMark);
			return idx == -1;
		}

		private Firmware Load([NotNull] byte[] data, [NotNull] FirmwareDefinition definition, EncryptionType encryptionType)
		{
			if (data == null) throw new ArgumentNullException("data");
			if (definition == null) throw new ArgumentNullException("definition");

			var imageBlocks = LoadImageBlocks(data, definition);
			var stringBlocks = LoadStringBlocks(data, definition);
			return new Firmware(data, imageBlocks, stringBlocks, definition, encryptionType);
		}

		internal FirmwareImageBlocks LoadImageBlocks([NotNull] byte[] firmware, [NotNull] FirmwareDefinition definition)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (definition == null) throw new ArgumentNullException("definition");

			using (var ms = new MemoryStream(firmware))
			{
				using (var reader = new BinaryReader(ms))
				{
					var block1Images = ReadImageTable<FirmwareImage1Metadata>(definition.ImageTable1, reader).ToDictionary(x => x.Index, x => x);
					var block2Images = ReadImageTable<FirmwareImage2Metadata>(definition.ImageTable2, reader).ToDictionary(x => x.Index, x => x);
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
					var block1String = ReadStringTable(definition.StringTable1, reader);
					var block2Strings = ReadStringTable(definition.StringTable2, reader, 1024);
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
				long offsetFrom;
				long offsetTo;
				if (!GetOffsets(imageTableDefinition, reader, out offsetFrom, out offsetTo)) return result;

				var offsetsTable = new List<Tuple<long, long>>();
				reader.BaseStream.Seek(offsetFrom, SeekOrigin.Begin);
				while (reader.BaseStream.Position <= offsetTo)
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

		private IEnumerable<FirmwareStringMetadata> ReadStringTable([CanBeNull] StringTableDefinition stringTableDefinition, [NotNull] BinaryReader reader, int startIndex = 0)
		{
			if (stringTableDefinition == null) return new List<FirmwareStringMetadata>();
			if (reader == null) throw new ArgumentNullException("reader");

			var charLength = GetCharLength(stringTableDefinition.TwoBytesPerChar);
			var result = new List<FirmwareStringMetadata>();
			{
				long offsetFrom;
				long offsetTo;
				if (!GetOffsets(stringTableDefinition, reader, out offsetFrom, out offsetTo)) return result;

				reader.BaseStream.Seek(offsetFrom, SeekOrigin.Begin);
				while (reader.BaseStream.Position <= offsetTo)
				{
					var offset = reader.BaseStream.Position;
					var dataLength = 0;

					while (true)
					{
						if (reader.BaseStream.Position <= offsetTo)
						{
							var char1 = ReadChar(reader, stringTableDefinition.TwoBytesPerChar);
							dataLength += charLength;

							short? char2;
							if (reader.BaseStream.Position <= offsetTo)
							{
								char2 = ReadChar(reader, stringTableDefinition.TwoBytesPerChar);
								reader.BaseStream.Position -= charLength;
							}
							else break;

							if (char1 == 0x00 && char2 != 0x00) break;
						}
						else break;
					}
					result.Add(new FirmwareStringMetadata(startIndex + result.Count + 1, offset, dataLength, stringTableDefinition.TwoBytesPerChar));
				}
			}
			return result;
		}

		private bool GetOffsets(FirmwareTableDefinition table, BinaryReader reader, out long offsetFrom, out long offsetTo)
		{
			offsetFrom = 0;
			offsetTo = 0;

			if (table.IsPtrTable)
			{
				reader.BaseStream.Seek(table.OffsetPtrFrom, SeekOrigin.Begin);
				var tempOffsetFrom = reader.ReadUInt32();
				if (tempOffsetFrom == 0) return false;

				reader.BaseStream.Seek(table.OffsetPtrTo, SeekOrigin.Begin);
				var tempOffsetTo = reader.ReadUInt32();
				if (tempOffsetTo == 0) return false;
				if (tempOffsetFrom > tempOffsetTo - 4) return false;

				offsetFrom = tempOffsetFrom;
				offsetTo = tempOffsetTo - 4;
				return true;
			}

			offsetFrom = table.OffsetFrom;
			offsetTo = table.OffsetTo;
			return true;
		}

		private short ReadChar(BinaryReader reader, bool twoBytesPerChar)
		{
			return twoBytesPerChar ? reader.ReadInt16() : reader.ReadByte();
		}

		private int GetCharLength(bool twoBytesPerChar)
		{
			return twoBytesPerChar ? 2 : 1;
		}
	}
}
