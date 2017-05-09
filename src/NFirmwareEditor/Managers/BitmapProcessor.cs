using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using NCore;

namespace NFirmwareEditor.Managers
{
	internal static class BitmapProcessor
	{
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
						if (!imageData[col, row]) continue;

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
				? image.Resize(desizeSize)
				: image;
		}

		public static Image EnlargePixelSize([NotNull] Bitmap source, int pixelSize = 2)
		{
			if (source == null) throw new ArgumentNullException("source");

			var result = new Bitmap(source.Width * pixelSize, source.Height * pixelSize);
			using (var gfx = Graphics.FromImage(result))
			{
				gfx.Clear(Color.Black);
				for (var col = 0; col < source.Width; col++)
				{
					for (var row = 0; row < source.Height; row++)
					{
						var pixel = source.GetPixel(col, row);

						if (pixelSize <= 1)
						{
							result.SetPixel(col, row, pixel);
						}
						else
						{
							gfx.FillRectangle(new SolidBrush(pixel), col * pixelSize, row * pixelSize, pixelSize, pixelSize);
						}
					}
				}
			}
			return result;
		}

		public static Icon CreateIcon(Bitmap bitmap)
		{
			try
			{
				var iconPtr = bitmap.GetHicon();
				var result = Icon.FromHandle(iconPtr);
				return result;
			}
			catch
			{
				return null;
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool DestroyIcon(IntPtr handle);
	}
}
