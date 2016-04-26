using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class ImageConverterWindow : EditorDialogWindow
	{
		private const int PreviewMargin = 8;

		private Bitmap m_originalBitmap;
		private Bitmap m_monochromeBitmap;
		private int m_width;
		private int m_height;
		private TextureBrush m_fontPreviewBackgroundBrush;
		private bool m_doNotUpdateMonochrome;

		public ImageConverterWindow()
		{
			InitializeComponent();
			InitializeControls();
		}

		private void InitializeControls()
		{
			SelectSourceButton.Click += SelectSourceButton_Click;
			NewWidthUpDown.ValueChanged += (s, e) =>
			{
				m_width = (int)NewWidthUpDown.Value;

				SafeExecute(() =>
				{
					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();
				});
			};
			NewHeightUpDown.ValueChanged += (s, e) =>
			{
				m_height = (int)NewHeightUpDown.Value;

				SafeExecute(() =>
				{
					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();
				});
			};
			JoyetechSizeButton.Click += (s, e) =>
			{
				NewWidthUpDown.Value = 64;
				NewHeightUpDown.Value = 40;
			};
			OkButton.Click += OkButton_Click;

			ImagePreviewSurface.Paint += ImagePreviewSurface_Paint;
			Closing += (s, e) =>
			{
				if (m_originalBitmap != null) m_originalBitmap.Dispose();
				if (m_monochromeBitmap != null) m_monochromeBitmap.Dispose();
			};
		}

		private void CreateMonochromeBitmap()
		{
			if (m_originalBitmap == null || m_doNotUpdateMonochrome) return;

			using (var scaledImage = BitmapProcessor.FitToSize(m_originalBitmap, new Size(m_width, m_height)))
			{
				if (m_monochromeBitmap != null)
				{
					m_monochromeBitmap.Dispose();
					m_monochromeBitmap = null;
				}
				m_monochromeBitmap = BitmapProcessor.ConvertTo1Bit(scaledImage);
			}
		}

		private void SelectSourceButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var op = new OpenFileDialog { Filter = Consts.BitmapImportFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			try
			{
				if (m_originalBitmap != null) m_originalBitmap.Dispose();
				m_originalBitmap = BitmapProcessor.LoadBitmapFromFile(fileName);

				if (m_originalBitmap.Width > 2048 || m_originalBitmap.Height > 2048)
				{
					if (m_monochromeBitmap != null)
					{
						m_monochromeBitmap.Dispose();
						m_monochromeBitmap = null;
					}

					SourceTextBox.Clear();
					ResizeContainerPanel.Enabled = false;

					m_doNotUpdateMonochrome = true;
					NewWidthUpDown.Value = m_width = (int)NewWidthUpDown.Minimum;
					NewHeightUpDown.Value = m_height = (int)NewHeightUpDown.Minimum;
					m_doNotUpdateMonochrome = false;

					ImagePreviewSurface.Invalidate();

					OkButton.Enabled = false;
					InfoBox.Show("Selected images is too big. Choose an image that has dimension lower than 2048x2048.");
				}
				else
				{
					SourceTextBox.Text = fileName;
					ResizeContainerPanel.Enabled = true;

					m_doNotUpdateMonochrome = true;
					NewWidthUpDown.Value = m_width = m_originalBitmap.Width;
					NewHeightUpDown.Value = m_height = m_originalBitmap.Height;
					m_doNotUpdateMonochrome = false;

					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();

					OkButton.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during opening image file!\n" + ex.Message);
			}
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var sf = new SaveFileDialog { Filter = Consts.BitmapExportFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			try
			{
				m_monochromeBitmap.Save(fileName, ImageFormat.Bmp);
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during saving image file!\n" + ex.Message);
			}
		}

		private static void SafeExecute(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				// Ignore
			}
		}

		private void ImagePreviewSurface_Paint(object sender, PaintEventArgs e)
		{
			SafeExecute(() =>
			{
				if (m_monochromeBitmap == null) return;

				var newSize = new Size(m_monochromeBitmap.Width + PreviewMargin * 2, m_monochromeBitmap.Height + PreviewMargin * 2);
				if (ImagePreviewSurface.AutoScrollMinSize != newSize) ImagePreviewSurface.AutoScrollMinSize = newSize;
			});

			e.Graphics.TranslateTransform(-ImagePreviewSurface.HorizontalScroll.Value, -ImagePreviewSurface.VerticalScroll.Value);

			if (ImagePreviewSurface.BackgroundImage != null)
			{
				if (m_fontPreviewBackgroundBrush == null)
				{
					m_fontPreviewBackgroundBrush = new TextureBrush(ImagePreviewSurface.BackgroundImage) { WrapMode = WrapMode.Tile };
				}
				e.Graphics.FillRectangle
				(
					m_fontPreviewBackgroundBrush,
					0,
					0,
					ImagePreviewSurface.Width + ImagePreviewSurface.HorizontalScroll.Value,
					ImagePreviewSurface.Height + ImagePreviewSurface.VerticalScroll.Value
				);
			}

			SafeExecute(() =>
			{
				if (m_monochromeBitmap == null) return;

				e.Graphics.DrawRectangle(Pens.LightGray, new Rectangle(PreviewMargin, PreviewMargin, m_monochromeBitmap.Width + 1, m_monochromeBitmap.Height + 1));
				e.Graphics.DrawImage(m_monochromeBitmap, new Rectangle(PreviewMargin + 1, PreviewMargin + 1, m_monochromeBitmap.Width, m_monochromeBitmap.Height));
			});
		}
	}
}
