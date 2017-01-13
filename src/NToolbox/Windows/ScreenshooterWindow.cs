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
using NToolbox.Models;
using NToolbox.Services;

namespace NToolbox.Windows
{
	internal partial class ScreenshooterWindow : EditorDialogWindow
	{
		private const int ScreenshotMargin = 1;
		private readonly ToolboxConfiguration m_configuration;
		private bool m_isBroadcasting;
		private Size m_screenSize;

		public ScreenshooterWindow([NotNull] ToolboxConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			m_configuration = configuration;

			InitializeComponent();
			InitializeControls();
		}

		private void InitializeControls()
		{
			TakeScreenshotBeforeSaveCheckBox.Checked = m_configuration.TakeScreenshotBeforeSave;
			TakeScreenshotBeforeSaveCheckBox.CheckedChanged += (s, e) => m_configuration.TakeScreenshotBeforeSave = TakeScreenshotBeforeSaveCheckBox.Checked;
			PixelSizeUpDown.ValueChanged += (s, e) => m_configuration.PixelSizeMultiplier = (int)PixelSizeUpDown.Value;
			TakeScreenshotButton.Click += TakeScreenshotButton_Click;
			BroadcastButton.Click += BroadcastButton_Click;
			SaveScreenshotButton.Click += SaveScreenshotButton_Click;

			Closing += (s, e) => m_isBroadcasting = false;

			ScreenSizeComboBox.Items.Clear();
			ScreenSizeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<Size>("[64x128] VTC, Cuboid, etc...", new Size(64, 128)),
				new NamedItemContainer<Size>("[96x16] iStick, etc...", new Size(96, 16))
			});
			ScreenSizeComboBox.SelectedValueChanged += (s, e) =>
			{
				m_screenSize = ScreenSizeComboBox.GetSelectedItem<Size>();
				ResizeScreenPictureBox();
				PlaceScreePictureBox();
				if (ScreenPictureBox.Image != null)
				{
					ScreenPictureBox.Image.Dispose();
					ScreenPictureBox.Image = null;
				}
				m_configuration.SelectedScreenSize = ScreenSizeComboBox.SelectedIndex;
			};
			PixelSizeUpDown.SetValue(m_configuration.PixelSizeMultiplier);
			ScreenSizeComboBox.SelectedIndex = Math.Max(Math.Min(m_configuration.SelectedScreenSize, ScreenSizeComboBox.Items.Count), 0);
		}

		private void ResizeScreenPictureBox()
		{
			ScreenPictureBox.Size = new Size(m_screenSize.Width + ScreenshotMargin * 2, m_screenSize.Height + ScreenshotMargin * 2);
		}

		private void PlaceScreePictureBox()
		{
			ScreenPictureBox.Location = new Point
			(
				ScreenBordersPanel.Width / 2 - ScreenPictureBox.Width / 2,
				ScreenBordersPanel.Height / 2 - ScreenPictureBox.Height / 2
			);
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
				BroadcastButton.Text = LocalizableStrings.ScreenshooterStopBroadcast;
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
						BroadcastButton.Text = LocalizableStrings.ScreenshooterStartBroadcast;
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

			using (var containerImage = new Bitmap(m_screenSize.Width + ScreenshotMargin * 2, m_screenSize.Height + ScreenshotMargin * 2))
			{
				using (var gfx = Graphics.FromImage(containerImage))
				{
					gfx.Clear(Color.Black);
					gfx.DrawImage(ScreenPictureBox.Image, ScreenshotMargin, ScreenshotMargin, m_screenSize.Width, m_screenSize.Height);
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
				var data = HidConnector.Instance.Screenshot();
				if (data == null) throw new InvalidOperationException("Invalid screenshot data!");

				data = data.Take(m_screenSize.Width * m_screenSize.Height / 8).ToArray();
				return CreateBitmapFromBytesArray(m_screenSize.Width, m_screenSize.Height, data);
			}
			catch (Exception)
			{
				if (ignoreErrors) return null;

				InfoBox.Show(LocalizableStrings.MessageNoCompatibleUSBDevice);
				return null;
			}
		}

		private void ShowScreenshot([NotNull] Image screenshot)
		{
			if (screenshot == null) throw new ArgumentNullException("screenshot");

			var prevImage = ScreenPictureBox.Image;
			ScreenPictureBox.Image = screenshot;

			if (prevImage != null)
			{
				prevImage.Dispose();
			}
		}

		private void SetButtonState(bool enabled)
		{
			ScreenSizeComboBox.Enabled = PixelSizeUpDown.Enabled = TakeScreenshotBeforeSaveCheckBox.Enabled = enabled;
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
