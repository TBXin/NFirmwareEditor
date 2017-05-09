using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using JetBrains.Annotations;

namespace NCore
{
	public static class BitmapExtensions
	{
		public static Bitmap Resize([NotNull] this Image sourceBitmap, Size desireSize)
		{
			if (sourceBitmap == null) throw new ArgumentNullException("sourceBitmap");

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

		public static bool[,] CreateRawFromBitmap([NotNull] this Bitmap sourceImage, byte pixelMark = 0xFF)
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
	}
}
