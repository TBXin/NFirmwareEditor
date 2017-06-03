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
		private const int W96H16BufferLength = 96 * 16 / 8;

		private readonly Size m_smallScreenSize = new Size(96, 16);
		private readonly Size m_bigScreenSize = new Size(64, 128);
		private readonly ToolboxConfiguration m_configuration;

		private Size? m_initialWindowSize;
		private Size m_screenSize;
		private bool m_isBroadcasting;

		public ScreenshooterWindow([NotNull] ToolboxConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			m_configuration = configuration;

			InitializeComponent();
			InitializeControls();
		}

		private void InitializeControls()
		{
			TabMultiPanel.SelectedPage = ControlPage;

			ControlLinkLabel.LinkClicked += TabLinkLabel_Click;
			OptionsLinkLabel.LinkClicked += TabLinkLabel_Click;

			TakeScreenshotButton.LinkClicked += TakeScreenshotButton_Click;
			BroadcastButton.LinkClicked += BroadcastButton_Click;
			SaveScreenshotButton.LinkClicked += SaveScreenshotButton_Click;

			TakeScreenshotBeforeSaveCheckBox.Checked = m_configuration.TakeScreenshotBeforeSave;
			TakeScreenshotBeforeSaveCheckBox.CheckedChanged += (s, e) => m_configuration.TakeScreenshotBeforeSave = TakeScreenshotBeforeSaveCheckBox.Checked;
			PixelSizeUpDown.ValueChanged += PixelSizeUpDown_ValueChanged;

			Load += (s, e) =>
			{
				m_initialWindowSize = Size;
				ResizeWindow();
			};
			Closing += (s, e) => m_isBroadcasting = false;
			PixelSizeUpDown.SetValue(m_configuration.PixelSizeMultiplier);
		}

		private void TabLinkLabel_Click(object sender, EventArgs e)
		{
			var link = sender as LinkLabel;
			if (link == null) return;

			var activeLinkColor = Color.FromArgb(90, 146, 221);
			var inactiveLinkColor = Color.FromArgb(200, 200, 200);

			ControlLinkLabel.LinkColor = OptionsLinkLabel.LinkColor = inactiveLinkColor;
			link.LinkColor = activeLinkColor;

			ActiveTabPanel.Left = link.Left;
			ActiveTabPanel.Width = link.Width;

			if (link == ControlLinkLabel) TabMultiPanel.SelectedPage = ControlPage;
			if (link == OptionsLinkLabel) TabMultiPanel.SelectedPage = OptionsPage;
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
				BroadcastButton.Links[0].Enabled = true;
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
			if (TakeScreenshotBeforeSaveCheckBox.Checked || ScreenPictureBox.BackgroundImage == null)
			{
				var screenshot = TakeScreenshot();
				if (screenshot == null) return;

				ShowScreenshot(screenshot);
			}

			if (ScreenPictureBox.BackgroundImage == null)
			{
				InfoBox.Show("Something went wrong!");
				return;
			}

			using (var containerImage = new Bitmap(m_screenSize.Width + ScreenshotMargin * 2, m_screenSize.Height + ScreenshotMargin * 2))
			{
				using (var gfx = Graphics.FromImage(containerImage))
				{
					gfx.Clear(Color.Black);
					gfx.DrawImage(ScreenPictureBox.BackgroundImage, ScreenshotMargin, ScreenshotMargin, m_screenSize.Width, m_screenSize.Height);
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
		private Bitmap TakeScreenshot(bool ignoreErrors = false)
		{
			try
			{
				var data = HidConnector.Instance.Screenshot();
				if (data == null) throw new InvalidOperationException("Invalid screenshot data!");

				if (data.Any(x => x != 0))
				{
					m_screenSize = data.Skip(W96H16BufferLength).Any(x => x != 0)
						? m_bigScreenSize
						: m_smallScreenSize;
				}
				else if (m_screenSize.IsEmpty)
				{
					return new Bitmap(1, 1);
				}

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

		private void ShowScreenshot([NotNull] Bitmap screenshot)
		{
			if (screenshot == null) throw new ArgumentNullException("screenshot");

			ResizeWindow();

			var prevImage = ScreenPictureBox.BackgroundImage;
			using (screenshot)
			{
				ScreenPictureBox.BackgroundImage = EnlargePixelSize(screenshot, Math.Min(m_configuration.PixelSizeMultiplier, 4));
			}

			if (prevImage != null)
			{
				prevImage.Dispose();
			}
		}

		private void SetButtonState(bool enabled)
		{
			PixelSizeUpDown.Enabled = TakeScreenshotBeforeSaveCheckBox.Enabled = enabled;
			TakeScreenshotButton.Links[0].Enabled = SaveScreenshotButton.Links[0].Enabled = BroadcastButton.Links[0].Enabled = enabled;
		}

		private void ResizeWindow()
		{
			// Do not change window size before window appear.
			if (!m_initialWindowSize.HasValue) return;
			// Do no change window size if zoom is big enough.
			if (m_configuration.PixelSizeMultiplier > 4) return;
			// Clean screen buffer.
			if (ScreenPictureBox.BackgroundImage != null)
			{
				ScreenPictureBox.BackgroundImage.Dispose();
				ScreenPictureBox.BackgroundImage = null;
			}

			var desiredWidth = m_configuration.PixelSizeMultiplier * m_screenSize.Width - m_screenSize.Width;
			var desiredHeight = m_configuration.PixelSizeMultiplier * m_screenSize.Height - m_screenSize.Height;

			var prevSize = Size;
			Size = new Size
			(
				m_initialWindowSize.Value.Width + desiredWidth,
				m_initialWindowSize.Value.Height + desiredHeight
			);

			Location = new Point
			(
				Location.X - (Size.Width - prevSize.Width) / 2,
				Location.Y - (Size.Height - prevSize.Height) / 2
			);

			if (TabMultiPanel.SelectedPage == ControlPage)
			{
				ActiveTabPanel.Left = ControlLinkLabel.Left;
			}
			if (TabMultiPanel.SelectedPage == OptionsPage)
			{
				ActiveTabPanel.Left = OptionsLinkLabel.Left;
			}
		}

		public Bitmap CreateBitmapFromBytesArray(int width, int height, [NotNull] byte[] imageData)
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

		private void PixelSizeUpDown_ValueChanged(object sender, EventArgs e)
		{
			m_configuration.PixelSizeMultiplier = (int)PixelSizeUpDown.Value;
			ResizeWindow();
		}
	}
}
