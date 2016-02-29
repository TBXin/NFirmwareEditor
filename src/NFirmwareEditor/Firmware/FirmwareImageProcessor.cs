using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NFirmwareEditor.Firmware
{
	internal static class FirmwareImageProcessor
	{
		public static FirmwareImages EnumerateImages(byte[] firmware, FirmwareDefinition definition)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (definition == null) throw new ArgumentNullException("definition");

			using (var ms = new MemoryStream(firmware))
			{
				using (var br = new BinaryReader(ms))
				{
					var block1Images = ReadImagesTable<Image1Metadata>(definition.ImageTable1, br);
					var block2Images = ReadImagesTable<Image2Metadata>(definition.ImageTable2, br);

					return new FirmwareImages(block1Images, block2Images);
				}
			}
		}

		private static List<ImageMetadata> ReadImagesTable<T>(ImageTableDefinition imageTableDefinition, BinaryReader reader) where T : ImageMetadata, new()
		{
			var result = new List<ImageMetadata>();
			if (imageTableDefinition == null) return result;

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
			return result;
		}

		public static bool[,] ReadImage(byte[] firmware, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var imageData = GetImageBytes(firmware, metadata);
			return metadata.ReadImage(imageData);
		}

		public static List<bool[,]> ReadImages(byte[] firmware, IEnumerable<ImageMetadata> metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (metadata == null) throw new ArgumentNullException("metadata");

			return metadata.Select(x => ReadImage(firmware, x)).ToList();
		}

		public static void WriteImage(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var imageBytes = metadata.WriteImage(imageData);
			Buffer.BlockCopy(imageBytes, 0, firmware, (int)metadata.DataOffset, imageBytes.Length);
		}

		public static bool[,] ClearAllPixelsImage(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
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

		public static bool[,] InvertImage(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = ProcessImage(imageData, col => col, row => row, v => !v);
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] ShiftImageUp(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = ProcessImage(imageData, col => col, row => row - 1 >= 0 ? row - 1 : height - 1, v => v);
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] ShiftImageDown(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = ProcessImage(imageData, col => col, row => row + 1 < height ? row + 1 : 0, v => v);
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] ShiftImageLeft(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = ProcessImage(imageData, col => col - 1 >= 0 ? col - 1 : width - 1, row => row, v => v);
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] ShiftImageRight(byte[] firmware, bool[,] imageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (imageData == null) throw new ArgumentNullException("imageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			if (width != metadata.Width || height != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var result = ProcessImage(imageData, col => col + 1 < width ? col + 1 : 0, row => row, v => v);
			WriteImage(firmware, result, metadata);
			return result;
		}

		public static bool[,] PasteImage(byte[] firmware, bool[,] sourceImageData, bool[,] copiedImageData, ImageMetadata metadata)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (sourceImageData == null) throw new ArgumentNullException("sourceImageData");
			if (copiedImageData == null) throw new ArgumentNullException("copiedImageData");
			if (metadata == null) throw new ArgumentNullException("metadata");

			var sourceWidth = sourceImageData.GetLength(0);
			var sourceHeight = sourceImageData.GetLength(1);

			if (sourceWidth != metadata.Width || sourceHeight != metadata.Height) throw new InvalidDataException("Image data does not correspond to the metadata.");

			var copiedWidth = copiedImageData.GetLength(0);
			var copiedHeigth = copiedImageData.GetLength(1);
			var width = Math.Min(sourceWidth, copiedWidth);
			var heigth = Math.Min(sourceHeight, copiedHeigth);

			var result = new bool[sourceWidth, sourceHeight];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < heigth; row++)
				{
					result[col, row] = copiedImageData[col, row];
				}
			}
			WriteImage(firmware, result, metadata);
			return result;
		}

		private static byte[] GetImageBytes(byte[] firmware, ImageMetadata metadata)
		{
			return firmware
				.Skip((int)metadata.DataOffset)
				.Take((int)metadata.DataLength)
				.ToArray();
		}

		private static bool[,] ProcessImage(bool[,] imageData, Func<int, int> colEvaluator, Func<int, int> rowEvaluator, Func<bool, bool> dataEvaluator)
		{
			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[colEvaluator(col), rowEvaluator(row)] = dataEvaluator(imageData[col, row]);
				}
			}
			return result;
		}
	}
}
