using System;
using System.Drawing;

namespace NFirmwareEditor.Managers
{
	internal static class FirmwareImageProcessor
	{
		public static bool[,] Clear(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			var size = GetImageSize(imageData);
			return new bool[size.Width, size.Height];
		}

		public static bool[,] Invert(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => col, (row, height) => row, v => !v);
		}

		public static bool[,] ShiftUp(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => col, (row, height) => row - 1 >= 0 ? row - 1 : height - 1, v => v);
		}

		public static bool[,] ShiftDown(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => col, (row, height) => row + 1 < height ? row + 1 : 0, v => v);
		}

		public static bool[,] ShiftLeft(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => col - 1 >= 0 ? col - 1 : width - 1, (row, height) => row, v => v);
		}

		public static bool[,] ShiftRight(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => col + 1 < width ? col + 1 : 0, (row, height) => row, v => v);
		}

		public static bool[,] PasteImage(bool[,] sourceImageData, bool[,] copiedImageData)
		{
			if (sourceImageData == null) throw new ArgumentNullException("sourceImageData");
			if (copiedImageData == null) throw new ArgumentNullException("copiedImageData");

			var sourceSize = GetImageSize(sourceImageData);
			var copiedSize = GetImageSize(copiedImageData);

			var width = Math.Min(sourceSize.Width, copiedSize.Width);
			var height = Math.Min(sourceSize.Height, copiedSize.Height);

			var result = new bool[sourceSize.Width, sourceSize.Height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col, row] = copiedImageData[col, row];
				}
			}
			return result;
		}

		private static Size GetImageSize(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return new Size(imageData.GetLength(0), imageData.GetLength(1));
		}

		private static bool[,] ProcessImage(bool[,] imageData, Func<int, int, int> colEvaluator, Func<int, int, int> rowEvaluator, Func<bool, bool> dataEvaluator)
		{
			var width = imageData.GetLength(0);
			var height = imageData.GetLength(1);

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[colEvaluator(col, width), rowEvaluator(row, height)] = dataEvaluator(imageData[col, row]);
				}
			}
			return result;
		}
	}
}
