using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NCore.USB;

namespace NToolbox.Windows
{
	public partial class ScreenshooterWindow : EditorDialogWindow
	{
		private readonly HidConnector m_connector = new HidConnector();
		private bool m_isBroadcasting;

		public ScreenshooterWindow()
		{
			InitializeComponent();

			TakeScreenshotButton.Click += TakeScreenshotButton_Click;
			BroadcastButton.Click += BroadcastButton_Click;
			SaveScreenshotButton.Click += SaveScreenshotButton_Click;
		}

		private void TakeScreenshotButton_Click(object sender, EventArgs e)
		{
			SetButtonState(false);
			var screenshot = TakeScreenshot();
			if (screenshot != null)
			{
				ShowScreenshot(screenshot);
			}
			SetButtonState(true);
		}

		private void BroadcastButton_Click(object sender, EventArgs e)
		{
			if (m_isBroadcasting)
			{
				m_isBroadcasting = false;
			}
			else
			{
				SetButtonState(false);
				BroadcastButton.Enabled = true;
				BroadcastButton.Text = @"Stop broadcast";
				m_isBroadcasting = true;
				new Thread(() =>
				{
					while (m_isBroadcasting)
					{
						var screenshot = TakeScreenshot();
						if (screenshot == null) break;

						UpdateUI(() => ShowScreenshot(screenshot));
					}

					m_isBroadcasting = false;
					UpdateUI(() =>
					{
						SetButtonState(true);
						BroadcastButton.Text = @"Start broadcast";
					});
				})
				{
					IsBackground = true
				}.Start();
			}
		}

		private void SaveScreenshotButton_Click(object sender, EventArgs e)
		{
			if (TakeScreenshotBeforeSaveCheckBox.Checked || ScreenPictureBox.Image == null)
			{
				var screenshot = TakeScreenshot();
				if (screenshot == null) return;

				ShowScreenshot(screenshot);
			}

			if (ScreenPictureBox.Image == null)
			{
				InfoBox.Show("Something went wrong!");
				return;
			}

			using (var containerImage = new Bitmap(ScreenPictureBox.Width + 4, ScreenPictureBox.Height + 4))
			{
				using (var gfx = Graphics.FromImage(containerImage))
				{
					gfx.DrawImage(ScreenPictureBox.Image, 2, 2, ScreenPictureBox.Image.Width, ScreenPictureBox.Image.Height);
				}

				using (var sf = new SaveFileDialog { FileName = string.Format("{0:yyyy.MM.dd HH.mm.ss}", DateTime.Now), Filter = @"Portable Network Graphics|*.png" })
				{
					if (sf.ShowDialog() != DialogResult.OK) return;

					using (var exportImage = EnlargePixelSize(containerImage, (int)PixelSizeUpDown.Value))
					{
						exportImage.Save(sf.FileName, ImageFormat.Png);
					}
				}
			}
		}

		[CanBeNull]
		private Image TakeScreenshot(bool ignoreErrors = false)
		{
			try
			{
				var data = m_connector.Screenshot();
				if (data == null) throw new InvalidOperationException("Invalid screenshot data!");

				return CreateBitmapFromBytesArray(64, 128, data);
			}
			catch (Exception)
			{
				if (ignoreErrors) return null;

				InfoBox.Show
				(
					"An error occurred during taking screenshot..." +
					"\n\n" +
					"To continue, please activate or reconnect your device."
				);
				return null;
			}
		}

		private void ShowScreenshot([NotNull] Image screenshot)
		{
			if (screenshot == null) throw new ArgumentNullException("screenshot");
			if (ScreenPictureBox.Image != null)
			{
				ScreenPictureBox.Image.Dispose();
				ScreenPictureBox.Image = null;
			}
			ScreenPictureBox.Image = screenshot;
		}

		private void SetButtonState(bool enabled)
		{
			TakeScreenshotButton.Enabled = SaveScreenshotButton.Enabled = BroadcastButton.Enabled = enabled;
		}

		public Image CreateBitmapFromBytesArray(int width, int height, [NotNull] byte[] imageData)
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
	}
}
