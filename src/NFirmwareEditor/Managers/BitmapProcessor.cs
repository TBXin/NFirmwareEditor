using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace NFirmwareEditor.Managers
{
	internal static class BitmapProcessor
	{
		public static Image CreateBitmap([NotNull] bool[,] imageData, int pixelSize = 2)
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

		public static Bitmap ConvertTo1Bit([NotNull] Bitmap input)
		{
			if (input == null) throw new ArgumentNullException("input");

			var masks = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
			var output = new Bitmap(input.Width, input.Height, PixelFormat.Format1bppIndexed);
			var data = new sbyte[input.Width, input.Height];
			var inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			try
			{
				var scanLine = inputData.Scan0;
				var line = new byte[inputData.Stride];
				for (var y = 0; y < inputData.Height; y++, scanLine += inputData.Stride)
				{
					Marshal.Copy(scanLine, line, 0, line.Length);
					for (var x = 0; x < input.Width; x++)
					{
						data[x, y] = (sbyte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
					}
				}
			}
			finally
			{
				input.UnlockBits(inputData);
			}
			var outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
			try
			{
				var scanLine = outputData.Scan0;
				for (var y = 0; y < outputData.Height; y++, scanLine += outputData.Stride)
				{
					var line = new byte[outputData.Stride];
					for (var x = 0; x < input.Width; x++)
					{
						var j = data[x, y] > 0;
						if (j)
							line[x / 8] |= masks[x % 8];
						var error = (sbyte)(data[x, y] - (j ? 32 : -32));
						if (x < input.Width - 1)
							data[x + 1, y] += (sbyte)(7 * error / 16);
						if (y < input.Height - 1)
						{
							if (x > 0)
								data[x - 1, y + 1] += (sbyte)(3 * error / 16);
							data[x, y + 1] += (sbyte)(5 * error / 16);
							if (x < input.Width - 1)
								data[x + 1, y + 1] += (sbyte)(1 * error / 16);
						}
					}
					Marshal.Copy(line, 0, scanLine, outputData.Stride);
				}
			}
			finally
			{
				output.UnlockBits(outputData);
			}
			return output;
		}

		private static double GetGreyLevel(byte r, byte g, byte b)
		{
			return (r * 0.299 + g * 0.587 + b * 0.114) / 255;
		}
	}
}
