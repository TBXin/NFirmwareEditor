using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class ImageConverterWindow : EditorDialogWindow
	{
		private readonly bool m_logoPreviewMode;
		private const int PreviewMargin = 8;

		private Bitmap m_originalBitmap;
		private Bitmap m_monochromeBitmap;
		private int m_width;
		private int m_height;
		private TextureBrush m_fontPreviewBackgroundBrush;
		private bool m_doNotUpdateMonochrome;

		public ImageConverterWindow(bool logoPreviewMode = false)
		{
			InitializeComponent();
			InitializeControls();

			m_logoPreviewMode = logoPreviewMode;
			if (m_logoPreviewMode)
			{
				ResizeContainerPanel.Enabled = false;

				OkButton.Text = @"Upload";
				OkButton.DialogResult = DialogResult.OK;
				OkButton.Click -= OkButton_Click;
			}
		}

		public Bitmap GetConvertedImage()
		{
			try
			{
				using (var ms = new MemoryStream())
				{
					m_monochromeBitmap.Save(ms, ImageFormat.Bmp);
					return (Bitmap)Image.FromStream(new MemoryStream(ms.ToArray()));
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during converting image.\n" + ex.Message);
				return null;
			}
		}

		private void InitializeControls()
		{
			ConversionTypeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<MonochromeConversionMode>("Floyd Steinberg Dithering", MonochromeConversionMode.FloydSteinbergDithering),
				new NamedItemContainer<MonochromeConversionMode>("Threshold based", MonochromeConversionMode.ThresholdBased)
			});
			ConversionTypeComboBox.SelectedIndex = 0;

			SelectSourceButton.Click += SelectSourceButton_Click;
			NewWidthUpDown.ValueChanged += (s, e) =>
			{
				m_width = (int)NewWidthUpDown.Value;

				Safe.Execute(() =>
				{
					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();
				});
			};
			NewHeightUpDown.ValueChanged += (s, e) =>
			{
				m_height = (int)NewHeightUpDown.Value;

				Safe.Execute(() =>
				{
					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();
				});
			};
			JoyetechSizeButton.Click += JoyetechSizeButton_Click;

			ConversionTypeComboBox.SelectedIndexChanged += (s, e) =>
			{
				var item = (NamedItemContainer<MonochromeConversionMode>)ConversionTypeComboBox.SelectedItem;
				ThresholdUpDown.Enabled = item.Data == MonochromeConversionMode.ThresholdBased;

				Safe.Execute(() =>
				{
					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();
				});
			};
			ThresholdUpDown.ValueChanged += (s, e) =>
			{
				Safe.Execute(() =>
				{
					CreateMonochromeBitmap();
					ImagePreviewSurface.Invalidate();
				});
			};

			OkButton.Click += OkButton_Click;

			ImagePreviewSurface.Paint += ImagePreviewSurface_Paint;
			Disposed += (s, e) =>
			{
				if (m_originalBitmap != null) m_originalBitmap.Dispose();
				if (m_monochromeBitmap != null) m_monochromeBitmap.Dispose();
			};
		}

		private void JoyetechSizeButton_Click(object sender, EventArgs eventArgs)
		{
			NewWidthUpDown.Value = 64;
			NewHeightUpDown.Value = 40;
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

				var mode = (NamedItemContainer<MonochromeConversionMode>)ConversionTypeComboBox.SelectedItem;
				switch (mode.Data)
				{
					case MonochromeConversionMode.ThresholdBased:
					{
						m_monochromeBitmap = BitmapProcessor.ConvertTo1Bit(scaledImage, MonochromeConversionMode.ThresholdBased, (int)ThresholdUpDown.Value);
						break;
					}
					case MonochromeConversionMode.FloydSteinbergDithering:
					{
						m_monochromeBitmap = BitmapProcessor.ConvertTo1Bit(scaledImage);
						break;
					}
					default: throw new ArgumentOutOfRangeException();
				}
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
					ConversionContainerPanel.Enabled = false;

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

					if (m_logoPreviewMode)
					{
						JoyetechSizeButton_Click(null, EventArgs.Empty);
					}
					else
					{
						ResizeContainerPanel.Enabled = true;
						ConversionContainerPanel.Enabled = true;

						m_doNotUpdateMonochrome = true;
						NewWidthUpDown.Value = m_width = m_originalBitmap.Width;
						NewHeightUpDown.Value = m_height = m_originalBitmap.Height;
						m_doNotUpdateMonochrome = false;
					}

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

		private void ImagePreviewSurface_Paint(object sender, PaintEventArgs e)
		{
			Safe.Execute(() =>
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
				e.Graphics.FillRectangle(m_fontPreviewBackgroundBrush, 0, 0, ImagePreviewSurface.Width + ImagePreviewSurface.HorizontalScroll.Value, ImagePreviewSurface.Height + ImagePreviewSurface.VerticalScroll.Value);
			}

			Safe.Execute(() =>
			{
				if (m_monochromeBitmap == null) return;

				e.Graphics.DrawRectangle(Pens.LightGray, new Rectangle(PreviewMargin, PreviewMargin, m_monochromeBitmap.Width + 1, m_monochromeBitmap.Height + 1));
				e.Graphics.DrawImage(m_monochromeBitmap, new Rectangle(PreviewMargin + 1, PreviewMargin + 1, m_monochromeBitmap.Width, m_monochromeBitmap.Height));
			});
		}
	}
}
