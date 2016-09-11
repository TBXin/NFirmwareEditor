using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace NFirmware
{
	public static class FirmwareAnalyzer
	{
		[CanBeNull]
		public static FirmwareDefinition Analyze([NotNull] byte[] firmwareBytes)
		{
			if (firmwareBytes == null) throw new ArgumentNullException("firmwareBytes");

			using (var br = new BinaryReader(new MemoryStream(firmwareBytes)))
			{
				var markers = ReadMarkers(br, new long[] { 0x90, 0xC0 });

				var imageTable2 = LookupImageTable(br, 0);
				if (imageTable2 == null) return null;

				var imageTable1 = LookupImageTable(br, imageTable2.OffsetTo);
				if (imageTable1 == null) return null;

				if (imageTable1.OffsetTo - imageTable1.OffsetFrom != imageTable2.OffsetTo - imageTable2.OffsetFrom) return null;

				var imageTableSize = (imageTable1.OffsetTo - imageTable1.OffsetFrom) / 4;
				var stringTable = LookupStringTable(br, imageTable1.OffsetTo, imageTableSize);
				if (stringTable == null) return null;

				imageTable1.OffsetTo -= 4;
				imageTable2.OffsetTo -= 4;
				stringTable.OffsetTo -= 1;

				return new FirmwareDefinition
				{
					Markers = markers.ToArray(),
					ImageTable1 = imageTable1,
					ImageTable2 = imageTable2,
					StringTable1 = stringTable,
					Sha = "not_supported",
					FileName = "auto_generated.xml",
					Name = "Automatic Recognition",
					StringsPreviewCorrection = new StringsPreviewCorrection { ForGlyphsString = "" }
				};
			}
		}

		private static List<FirmwareMarkerDefinition> ReadMarkers(BinaryReader br, IEnumerable<long> offsets, int bytesCount = 2)
		{
			var result = new List<FirmwareMarkerDefinition>();
			foreach (var offset in offsets)
			{
				br.BaseStream.Position = offset;
				var data = br.ReadBytes(bytesCount);
				result.Add(new FirmwareMarkerDefinition
				{
					OffsetFromString = "0x" + offset.ToString("X2"),
					MarkerBytesString = string.Join(" ", data.Select(x => string.Format("0x{0:X2}", x)))
				});
			}
			return result;
		}

		private static ImageTableDefinition LookupImageTable(BinaryReader br, long startSearchFromOffset)
		{
			var lowerOffsetThreshold = br.BaseStream.Length / 2;
			var tableLengthThresholdInBytes = 100 * 4;
			br.BaseStream.Position = startSearchFromOffset;

			long tableStart;
			long tableEnd;

			while (true)
			{
				if (!LookupImageTableInternal(br, LookupMode.UntilValidReference, lowerOffsetThreshold, out tableStart)) break;
				if (!LookupImageTableInternal(br, LookupMode.UntilInvalidReference, lowerOffsetThreshold, out tableEnd)) break;
				if (tableEnd - tableStart < tableLengthThresholdInBytes) continue;

				return new ImageTableDefinition
				{
					OffsetFromString = "0x" + tableStart.ToString("X4"),
					OffsetToString = "0x" + tableEnd.ToString("X4")
				};
			}
			return null;
		}

		private static bool LookupImageTableInternal(BinaryReader br, LookupMode mode, long thresholdValue, out long offset)
		{
			while (br.BaseStream.Position + 4 <= br.BaseStream.Length)
			{
				var data = br.ReadUInt32();
				switch (mode)
				{
					case LookupMode.UntilValidReference:
					{
						// Skipping values if they are not references.
						if (data <= thresholdValue || data >= br.BaseStream.Length) continue;

						offset = br.BaseStream.Position - 4;
						return true;
					}
					case LookupMode.UntilInvalidReference:
					{
						// Skipping values if they are references.
						if (data > thresholdValue && data < br.BaseStream.Length) continue;

						offset = br.BaseStream.Position - 4;
						return true;
					}
					default: throw new ArgumentOutOfRangeException("mode", mode, null);
				}
			}
			offset = 0;
			return false;
		}

		private static StringTableDefinition LookupStringTable(BinaryReader br, long startSearchFromOffset, long tableSize)
		{
			var tableLengthThresholdInBytes = 100;
			br.BaseStream.Position = startSearchFromOffset;

			long tableStart;
			long tableEnd;

			while (true)
			{
				if (!LookupStringTableInternal(br, LookupMode.UntilValidReference, tableSize, out tableStart)) break;
				if (!LookupStringTableInternal(br, LookupMode.UntilInvalidReference, tableSize, out tableEnd)) break;
				if (tableEnd - tableStart < tableLengthThresholdInBytes) continue;

				return new StringTableDefinition
				{
					OffsetFromString = "0x" + tableStart.ToString("X4"),
					OffsetToString = "0x" + tableEnd.ToString("X4"),
					TwoBytesPerChar = true
				};
			}

			return null;
		}

		private static bool LookupStringTableInternal(BinaryReader br, LookupMode mode, long thresholdValue, out long offset)
		{
			ushort prevData = 0;
			while (br.BaseStream.Position + 2 <= br.BaseStream.Length)
			{
				var data = br.ReadUInt16();
				switch (mode)
				{
					case LookupMode.UntilValidReference:
					{
						if (data <= 10 || data > thresholdValue)
						{
							br.BaseStream.Position -= 1;
							continue;
						}

						data = br.ReadUInt16();
						if (data <= 10 || data > thresholdValue)
						{
							br.BaseStream.Position -= 3;
							continue;
						}

						br.BaseStream.Position -= 4;
						offset = br.BaseStream.Position;
						return true;
					}
					case LookupMode.UntilInvalidReference:
					{
						if ((prevData != 0 && data == 0) || (data > 0 && data <= thresholdValue))
						{
							prevData = data;
							continue;
						}

						offset = br.BaseStream.Position - 2;
						return true;
					}
					default: throw new ArgumentOutOfRangeException("mode", mode, null);
				}
			}
			offset = 0;
			return false;
		}

		private enum LookupMode
		{
			UntilValidReference,
			UntilInvalidReference
		}
	}
}
