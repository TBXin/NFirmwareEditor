using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal static class BitmapProcessor
	{
		private static readonly byte[] s_bitMasks = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

		public static Image CreateBitmapFromBytesArray(int width, int height, [NotNull] byte[] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			var bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
			var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
			try
			{
				Marshal.Copy(imageData, 0, bitmapData.Scan0, imageData.Length);
			}
			finally
			{
				bitmap.UnlockBits(bitmapData);
			}
			return bitmap;
		}

		public static Image CreateBitmapFromRaw([NotNull] bool[,] imageData, int pixelSize = 2)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			var size = FirmwareImageProcessor.GetImageSize(imageData);
			var result = new Bitmap(size.Width * pixelSize, size.Height * pixelSize);
			using (var gfx = Graphics.FromImage(result))
			{
				gfx.Clear(Color.Black);
				for (var col = 0; col < size.Width; col++)
				{
					for (var row = 0; row < size.Height; row++)
					{
						if (!imageData[col, row])
							continue;

						if (pixelSize <= 1)
						{
							result.SetPixel(col, row, Color.White);
						}
						else
						{
							gfx.FillRectangle(Brushes.White, col * pixelSize, row * pixelSize, pixelSize, pixelSize);
						}
					}
				}
			}
			return result;
		}

		public static bool[,] CreateRawFromBitmap([NotNull] Bitmap sourceImage, byte pixelMark = 0xFF)
		{
			if (sourceImage == null) throw new ArgumentNullException("sourceImage");

			var width = sourceImage.Width;
			var height = sourceImage.Height;

			var result = new bool[width, height];
			for (var col = 0; col < width; col++)
			{
				for (var row = 0; row < height; row++)
				{
					var pixel = sourceImage.GetPixel(col, row);
					result[col, row] = pixel.R == pixelMark && pixel.G == pixelMark && pixel.B == pixelMark;
				}
			}
			return result;
		}

		public static Bitmap Create1BitBitmapFromRaw(bool[,] imageData)
		{
			if (imageData == null) throw new ArgumentNullException("imageData");

			var size = FirmwareImageProcessor.GetImageSize(imageData);
			var result = new Bitmap(size.Width, size.Height, PixelFormat.Format1bppIndexed);
			var bitmapData = result.LockBits(new Rectangle(0, 0, size.Width, size.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

			try
			{
				var scanLine = bitmapData.Scan0;
				for (var y = 0; y < size.Height; y++, scanLine += bitmapData.Stride)
				{
					var line = new byte[bitmapData.Stride];
					for (var x = 0; x < size.Width; x++)
					{
						if (imageData[x, y] == false) continue;
						line[x / 8] |= s_bitMasks[x % 8];
					}
					Marshal.Copy(line, 0, scanLine, bitmapData.Stride);
				}
			}
			finally
			{
				result.UnlockBits(bitmapData);
			}

			return result;
		}

		public static Bitmap ConvertTo1Bit
		(
			[NotNull] Bitmap sourceBitmap, 
			MonochromeConversionMode mode = MonochromeConversionMode.FloydSteinbergDithering, 
			int threshold = 127
		)
		{
			if (sourceBitmap == null) throw new ArgumentNullException("sourceBitmap");

			switch (mode)
			{
				case MonochromeConversionMode.ThresholdBased: return ThresholdBasedMonochrome(sourceBitmap, threshold);
				case MonochromeConversionMode.FloydSteinbergDithering: return FloydSteinbergDithering(sourceBitmap);
				default: throw new ArgumentOutOfRangeException("mode", mode, null);
			}
		}

		private static Bitmap FloydSteinbergDithering([NotNull] Bitmap sourceBitmap)
		{
			var result = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format1bppIndexed);
			var data = new sbyte[sourceBitmap.Width, sourceBitmap.Height];
			var inputData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

			try
			{
				var scanLine = inputData.Scan0;
				var line = new byte[inputData.Stride];
				for (var y = 0; y < inputData.Height; y++, scanLine += inputData.Stride)
				{
					Marshal.Copy(scanLine, line, 0, line.Length);
					for (var x = 0; x < sourceBitmap.Width; x++)
					{
						data[x, y] = (sbyte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
					}
				}
			}
			finally
			{
				sourceBitmap.UnlockBits(inputData);
			}

			var outputData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
			try
			{
				var scanLine = outputData.Scan0;
				for (var y = 0; y < outputData.Height; y++, scanLine += outputData.Stride)
				{
					var line = new byte[outputData.Stride];
					for (var x = 0; x < sourceBitmap.Width; x++)
					{
						var j = data[x, y] > 0;
						if (j)
						{
							line[x / 8] |= s_bitMasks[x % 8];
						}
						var error = (sbyte)(data[x, y] - (j ? 32 : -32));
						if (x < sourceBitmap.Width - 1)
						{
							data[x + 1, y] += (sbyte)(7 * error / 16);
						}
						if (y < sourceBitmap.Height - 1)
						{
							if (x > 0) data[x - 1, y + 1] += (sbyte)(3 * error / 16);
							data[x, y + 1] += (sbyte)(5 * error / 16);
							if (x < sourceBitmap.Width - 1)
							{
								data[x + 1, y + 1] += (sbyte)(1 * error / 16);
							}
						}
					}
					Marshal.Copy(line, 0, scanLine, outputData.Stride);
				}
			}
			finally
			{
				result.UnlockBits(outputData);
			}

			return result;
		}

		private static Bitmap ThresholdBasedMonochrome(Bitmap sourceBitmap, int threshold)
		{
			BitmapData inputData = null;
			try
			{
				inputData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

				var imageData = new bool[sourceBitmap.Width, sourceBitmap.Height];
				var scanLine = inputData.Scan0;
				var line = new byte[inputData.Stride];
				for (var y = 0; y < inputData.Height; y++, scanLine += inputData.Stride)
				{
					Marshal.Copy(scanLine, line, 0, line.Length);
					for (var x = 0; x < sourceBitmap.Width; x++)
					{
						imageData[x, y] = GetGray(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) >= threshold;
					}
				}

				return Create1BitBitmapFromRaw(imageData);
			}
			finally
			{
				if (inputData != null) sourceBitmap.UnlockBits(inputData);
			}
		}

		public static Bitmap LoadBitmapFromFile([NotNull] string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			using (var imageStream = new MemoryStream(File.ReadAllBytes(filePath)))
			{
				return (Bitmap)Image.FromStream(imageStream);
			}
		}

		public static Bitmap ScaleBitmapIfNecessary(Bitmap image, Size desizeSize)
		{
			return image.Width > desizeSize.Width || image.Height > desizeSize.Height
				? FitToSize(image, desizeSize)
				: image;
		}

		public static Bitmap FitToSize(Image sourceBitmap, Size desireSize)
		{
			var scalingRatioX = (float)sourceBitmap.Width / desireSize.Width;
			var scalingRatioY = (float)sourceBitmap.Height / desireSize.Height;
			var scalingRatio = Math.Max(scalingRatioX, scalingRatioY);

			var output = new Bitmap(desireSize.Width, desireSize.Height);
			using (var gfx = Graphics.FromImage(output))
			{
				gfx.CompositingQuality = CompositingQuality.HighQuality;
				gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gfx.SmoothingMode = SmoothingMode.AntiAlias;

				var rect = new RectangleF(0, 0, sourceBitmap.Width / scalingRatio, sourceBitmap.Height / scalingRatio);
				{
					rect.X = desireSize.Width / 2f - rect.Width / 2f;
					rect.Y = desireSize.Height / 2f - rect.Height / 2f;
				}
				gfx.DrawImage(sourceBitmap, rect);
			}
			return output;
		}

		private static double GetGray(byte r, byte g, byte b)
		{
			return r * 0.299 + g * 0.587 + b * 0.114;
		}

		private static double GetGreyLevel(byte r, byte g, byte b)
		{
			return GetGray(r, g, b) / 255;
		}
	}
}
