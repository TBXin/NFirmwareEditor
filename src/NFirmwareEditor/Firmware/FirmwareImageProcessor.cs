using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NFirmwareEditor.Firmware
{
	internal static class FirmwareImageProcessor
	{
		public static List<ImageMetadata> EnumerateImages(byte[] firmware, long offsetFrom, long offsetTo)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

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
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (metadata == null) throw new ArgumentNullException("metadata");

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

		public static void WriteImage(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var imageBytesCounter = 0;
			var imageBytes = new byte[metadata.DataLength];
			var rowCounter = 0;

			while (rowCounter < height)
			{
				var byteBits = new bool[8];
				for (var col = 0; col < width; col++)
				{
					for (var row = 0; row < byteBits.Length; row++)
					{
						byteBits[row] = imageData[col, rowCounter + row];
					}
					imageBytes[imageBytesCounter++] = ToByte(new BitArray(byteBits));
				}
				rowCounter += byteBits.Length;
			}

			Buffer.BlockCopy(imageBytes, 0, firmware, (int)metadata.DataOffset, imageBytes.Length);
		}

		public static bool[,] ClearImage(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = new bool[width, height];
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] InverseImage(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col, row] = !imageData[col, row];
				}
			}
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] MoveImageUp(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col, row - 1 >= 0 ? row - 1 : height - 1] = imageData[col, row];
				}
			}
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] MoveImageDown(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col, row + 1 < height ? row + 1 : 0] = imageData[col, row];
				}
			}
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] MoveImageLeft(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col - 1 >= 0 ? col - 1 : width - 1, row] = imageData[col, row];
				}
			}
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] MoveImageRight(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col + 1 < width ? col + 1 : 0, row] = imageData[col, row];
				}
			}
			WriteImage(firmware, result, metadata);
			return result;
		}

		private static ImageMetadata ReadImageMetadataData(BinaryReader reader, int index)
		{
			if (reader == null) throw new ArgumentNullException("reader");

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
			if (bitArray == null) throw new ArgumentNullException("bitArray");

			var result = new bool[bitArray.Length];
			for (var i = 0; i < result.Length; i++)
			{
				result[i] = bitArray[i];
			}
			return result;
		}

		private static byte ToByte(BitArray bitArray)
		{
			if (bitArray == null) throw new ArgumentNullException("bitArray");
			if (bitArray.Count != 8) throw new ArgumentException("bits");

			var bytes = new byte[1];
			bitArray.CopyTo(bytes, 0);
			return bytes[0];
		}
	}
}
