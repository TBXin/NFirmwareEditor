using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NFirmwareEditor.Firmware
{
	internal static class FirmwareImageProcessor
	{
		public static List<ImageMetadata> EnumerateImages(byte[] firmware, long offsetFrom, long offsetTo)
		{
			var result = new List<ImageMetadata>();
			using (var ms = new MemoryStream(firmware))
			{
				using (var br = new BinaryReader(ms))
				{
					br.BaseStream.Seek(offsetFrom, SeekOrigin.Begin);

					var offsetsTable = new List<long>();
					while (br.BaseStream.Position <= offsetTo)
					{
						offsetsTable.Add(br.ReadUInt32());
					}

					for (var i = 0; i < offsetsTable.Count; i++)
					{
						br.BaseStream.Seek(offsetsTable[i], SeekOrigin.Begin);
						result.Add(ReadImageMetadataData(br, i + 1));
					}
				}
			}
			return result;
		}

		public static bool[,] ReadImage(byte[] firmware, ImageMetadata metadata)
		{
			var result = new bool[metadata.Width, metadata.Height];
			var colCounter = 0;
			var rowCounter = 0;
			for (var i = 0; i < metadata.DataLength; i++)
			{
				var bits = ToBoolArray(new BitArray(new[] { firmware[metadata.DataOffset + i] }));

				if (colCounter == metadata.Width)
				{
					colCounter = 0;
					rowCounter += bits.Length;
				}

				for (var j = 0; j < bits.Length; j++)
				{
					result[colCounter, rowCounter + j] = bits[j];
				}

				colCounter++;
			}
			return result;
		}

		private static ImageMetadata ReadImageMetadataData(BinaryReader reader, int index)
		{
			var name = string.Format("[Char: 0x{0:X2}] 0x{1:X2} ", index, reader.BaseStream.Position);
			var width = reader.ReadByte();
			var height = reader.ReadByte();
			var dataOffset = reader.BaseStream.Position;
			var dataLength = width * height / 8;
			reader.BaseStream.Seek(dataLength, SeekOrigin.Current);

			return new ImageMetadata(name, width, height, dataOffset, dataLength);
		}

		private static bool[] ToBoolArray(BitArray bitArray)
		{
			var result = new bool[bitArray.Length];
			for (var i = 0; i < result.Length; i++)
			{
				result[i] = bitArray[i];
			}
			return result;
		}
	}
}
