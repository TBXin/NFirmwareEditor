using System;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;

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

		public static bool[,] FlipHorizontal(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => width - col - 1, (row, height) => row, v => v);
		}

		public static bool[,] FlipVertical(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");
			return ProcessImage(imageData, (col, width) => col, (row, height) => height - row - 1, v => v);
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

		public static bool[,] Rotate([NotNull] bool[,] imageData, bool clockwise)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			var imageSize = GetImageSize(imageData);
			var result = new bool[imageSize.Height, imageSize.Width];
			for (var col = 0; col < imageSize.Width; col++)
			{
				for (var row = 0; row < imageSize.Height; row++)
				{
					result[row, col] = clockwise
						? imageData[col, imageSize.Height - row - 1]
						: imageData[imageSize.Width - col - 1, row];
				}
			}
			return result;
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

		public static bool[,] ResizeImage([NotNull] bool[,] sourceImageData, Size newSize)
		{
			if (sourceImageData == null) throw new ArgumentNullException("sourceImageData");

			var sourceSize = GetImageSize(sourceImageData);
			var width = Math.Min(sourceSize.Width, newSize.Width);
			var height = Math.Min(sourceSize.Height, newSize.Height);

			var result = new bool[newSize.Width, newSize.Height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					result[col, row] = sourceImageData[col, row];
				}
			}
			return result;
		}

		public static Bitmap CropImage(Image srcImage, Rectangle cropArea)
		{
			var rtnImage = new Bitmap(cropArea.Width, cropArea.Height);
			using (var gfx = Graphics.FromImage(rtnImage))
			{
				gfx.DrawImage(srcImage, 0, 0, cropArea, GraphicsUnit.Pixel);
			}
			return rtnImage;
		}

		public static bool[,] MergeImages([NotNull] List<bool[,]> imagesData)
		{
			if (imagesData == null) throw new ArgumentNullException("imagesData");

			var totalWidth = 0;
			var totalHeight = 0;

			foreach (var imageData in imagesData)
			{
				var size = GetImageSize(imageData);

				totalWidth += size.Width;
				if (totalHeight < size.Height) totalHeight = size.Height;
			}

			var result = new bool[totalWidth, totalHeight];
			var colOffset = 0;
			foreach (var image in imagesData)
			{
				var size = GetImageSize(image);
				for (var col = 0; col < size.Width; col++)
				{
					for (var row = 0; row < size.Height; row++)
					{
						result[colOffset + col, row] = image[col, row];
					}
				}
				colOffset += image.GetLength(0);
			}
			return result;
		}

		public static Size GetImageSize(bool[,] imageData)
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
