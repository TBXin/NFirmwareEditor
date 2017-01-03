using System.Drawing;
using System.Linq;
using NToolbox.Models;

namespace NToolbox.Services
{
	internal static class ChartPreviewService
	{
		internal static Bitmap CreatePowerCurvePreview(ArcticFoxConfiguration.PowerCurve powerCurve, Size size)
		{
			const float percentMaxValue = 250;

			var actualMaxTime = powerCurve.Points.Max(x => x.Time) / 10f;
			var actualWidth = size.Width - 1;
			var actualHeight = size.Height - 1;

			var bitmap = new Bitmap(size.Width, size.Height);
			using (var gfx = Graphics.FromImage(bitmap))
			{
				gfx.Clear(Color.White);

				var chartPoints = new PointF[powerCurve.Points.Length];
				for (var i = 0; i < powerCurve.Points.Length; i++)
				{
					var powerPoint = powerCurve.Points[i];
					var xRaw = powerPoint.Time / 10f;
					var yRaw = powerPoint.Percent;

					// 4 is a sum of 2 pixel paddings from left and right.
					var x = xRaw * (actualWidth - 4) / actualMaxTime;
					var y = actualHeight - yRaw * actualHeight / percentMaxValue;

					// Realize padding from left by 2 pixels.
					chartPoints[i] = new PointF(x + 2, y);
				}

				DrawGrid(gfx, 10, 5, actualWidth, actualHeight);
				gfx.DrawLines(new Pen(Color.YellowGreen, 1), chartPoints);
				gfx.DrawRectangle(Pens.LightGray, 0, 0, actualWidth, actualHeight);
			}
			return bitmap;
		}

		internal static Bitmap CreateTFRCurvePreview(ArcticFoxConfiguration.TFRTable tfrTable, Size size)
		{
			const float percentMaxValue = 4;

			var actualMinTemperature = tfrTable.Points.Min(x => x.Temperature);
			var actualMaxTemperature = tfrTable.Points.Max(x => x.Temperature) - actualMinTemperature;
			var actualWidth = size.Width - 1;
			var actualHeight = size.Height - 1;

			var bitmap = new Bitmap(size.Width, size.Height);
			using (var gfx = Graphics.FromImage(bitmap))
			{
				gfx.Clear(Color.White);

				var chartPoints = new PointF[tfrTable.Points.Length];
				for (var i = 0; i < tfrTable.Points.Length; i++)
				{
					var powerPoint = tfrTable.Points[i];
					var xRaw = powerPoint.Temperature - actualMinTemperature;
					var yRaw = powerPoint.Factor / 10000f;

					// 4 is a sum of 2 pixel paddings from left and right.
					var x = xRaw * (actualWidth - 4) / actualMaxTemperature;
					var y = actualHeight - yRaw * actualHeight / percentMaxValue;

					// Realize padding from left by 2 pixels.
					chartPoints[i] = new PointF(x + 2, y);
				}

				DrawGrid(gfx, 10, 5, actualWidth, actualHeight);
				gfx.DrawLines(new Pen(Color.YellowGreen, 1), chartPoints);
				gfx.DrawRectangle(Pens.LightGray, 0, 0, actualWidth, actualHeight);
			}
			return bitmap;
		}

		private static void DrawGrid(Graphics gfx, int hLines, int vLines, int width, int height)
		{
			var hStep = (float)width / hLines;
			var vStep = (float)height / vLines;

			var linePen = new Pen(Color.FromArgb(240, 240, 240), 1);
			for (var x = hStep; x < width; x += hStep) gfx.DrawLine(linePen, x, 0, x, height);
			for (var y = vStep; y < height; y += vStep) gfx.DrawLine(linePen, 0, y, width, y);
		}
	}
}
