using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	public partial class ImageEditorTabPage : UserControl, IEditorTabPage
	{
		private const int ImagedListBoxItemMaxHeight = 24 * 2;
		private const int ImagedListBoxItemImageMargin = 6;

		private readonly ClipboardManager m_clipboardManager = new ClipboardManager();

		private Configuration m_configuration;
		private Firmware m_firmware;
		private BlockType m_currentBlock = BlockType.Block1;
		private bool m_imageListBoxIsUpdating;

		public ImageEditorTabPage()
		{
			InitializeComponent();
		}

		[NotNull]
		public ListBox ImageListBox
		{
			get
			{
				switch (m_currentBlock)
				{
					case BlockType.Block1: return Block1ImageListBox;
					case BlockType.Block2: return Block2ImageListBox;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		[CanBeNull]
		public FirmwareImageMetadata LastSelectedImageMetadata
		{
			get
			{
				if (ImageListBox.Items.Count == 0 || ImageListBox.SelectedIndices.Count == 0) return null;

				var item = ImageListBox.Items[ImageListBox.SelectedIndices[ImageListBox.SelectedIndices.Count - 1]] as ImagedItem<FirmwareImageMetadata>;
				return item != null ? item.Value : null;
			}
		}

		[NotNull]
		public List<FirmwareImageMetadata> SelectedImageMetadata
		{
			get
			{
				if (ImageListBox.SelectedIndices.Count == 0) return new List<FirmwareImageMetadata>();

				var result = new List<FirmwareImageMetadata>();
				foreach (int selectedIndex in ImageListBox.SelectedIndices)
				{
					var metadata = ImageListBox.Items[selectedIndex] as ImagedItem<FirmwareImageMetadata>;
					if (metadata == null) continue;

					result.Add(metadata.Value);
				}
				return result;
			}
		}

		#region Implementation of IEditorTabPage
		public string Title
		{
			get { return "Images"; }
		}

		public void Initialize(Configuration configuration)
		{
			m_configuration = configuration;
			GridSizeUpDown.Value = m_configuration.GridSize;
			ShowGridCheckBox.Checked = m_configuration.ShowGid;

			ImagePreviewPixelGrid.BlockInnerBorderPen = Pens.Transparent;
			ImagePreviewPixelGrid.BlockOuterBorderPen = Pens.Transparent;
			ImagePreviewPixelGrid.ActiveBlockBrush = Brushes.White;
			ImagePreviewPixelGrid.InactiveBlockBrush = Brushes.Black;

			Block1ImageRadioButton.CheckedChanged += BlockImageRadioButton_CheckedChanged;
			Block2ImageRadioButton.CheckedChanged += BlockImageRadioButton_CheckedChanged;

			ImagePixelGrid.CursorPositionChanged += location =>
			{
				CursorPositionLabel.Text = location.HasValue
					? string.Format("X: {0}, Y:{1}", location.Value.X + 1, location.Value.Y + 1)
					: string.Empty;
			};

			ImagePixelGrid.DataUpdated += data =>
			{
				if (LastSelectedImageMetadata == null) return;

				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(r => data, LastSelectedImageMetadata);
			};

			Block1ImageListBox.SelectedValueChanged += ImageListBox_SelectedValueChanged;
			Block1ImageListBox.DrawMode = DrawMode.OwnerDrawVariable;
			Block1ImageListBox.MeasureItem += ImageListBox_MeasureItem;
			Block1ImageListBox.DrawItem += ImageListBox_DrawItem;

			Block2ImageListBox.SelectedValueChanged += ImageListBox_SelectedValueChanged;
			Block2ImageListBox.DrawMode = DrawMode.OwnerDrawVariable;
			Block2ImageListBox.MeasureItem += ImageListBox_MeasureItem;
			Block2ImageListBox.DrawItem += ImageListBox_DrawItem;

			GridSizeUpDown.ValueChanged += GridSizeUpDown_ValueChanged;
			ShowGridCheckBox.CheckedChanged += ShowGridCheckBox_CheckedChanged;

			ClearAllPixelsButton.Click += ClearAllPixelsButton_Click;
			InverseButton.Click += InvertButton_Click;

			FlipHorizontalButton.Click += FlipHorizontalButton_Click;
			FlipVerticalButton.Click += FlipVerticalButton_Click;
			ResizeButton.Click += ResizeButton_Click;

			ShiftLeftButton.Click += ShiftLeftButton_Click;
			ShiftRightButton.Click += ShiftRightButton_Click;
			ShiftUpButton.Click += ShiftUpButton_Click;
			ShiftDownButton.Click += ShiftDownButton_Click;
			CopyButton.Click += CopyButton_Click;
			PasteButton.Click += PasteButton_Click;
			ImageEditorHotkeyInformationButton.Click += ImageEditorHotkeyInformationButton_Click;

			CopyContextMenuItem.Click += CopyButton_Click;
			PasteContextMenuItem.Click += PasteButton_Click;
			ImportContextMenuItem.Click += ImportContextMenuItem_Click;
			ExportContextMenuItem.Click += ExportContextMenuItem_Click;
		}

		public void OnWorkspaceReset()
		{
			Block1ImageListBox.Items.Clear();
			Block2ImageListBox.Items.Clear();
			Block1ImageRadioButton.Checked = true;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = new bool[5, 5];
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			Block2ImageListBox.Fill(m_firmware.Block2Images.Select(x => new ImagedItem<FirmwareImageMetadata>(x, x.Index, string.Format("0x{0:X2}", x.Index))), true);
			Block1ImageListBox.Fill(m_firmware.Block1Images.Select(x => new ImagedItem<FirmwareImageMetadata>(x, x.Index, string.Format("0x{0:X2}", x.Index))), true);
		}

		public void OnActivate()
		{
		}

		public bool OnHotkey(Keys keyData)
		{
			if (!keyData.HasFlag(Keys.Control)) return false;

			if (keyData.HasFlag(Keys.N))
			{
				ClearAllPixelsButton.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.I))
			{
				InverseButton.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.C))
			{
				CopyButton.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.V))
			{
				PasteButton.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.A))
			{
				ImageListBox.SelectedIndices.Clear();
				ImageListBox.SelectedIndices.AddRange(Enumerable.Range(0, ImageListBox.Items.Count));
				return true;
			}

			var key = keyData &= ~Keys.Control;
			if (key == Keys.Up)
			{
				ShiftUpButton.PerformClick();
				return true;
			}
			if (key == Keys.Down)
			{
				ShiftDownButton.PerformClick();
				return true;
			}
			if (key == Keys.Left)
			{
				ShiftLeftButton.PerformClick();
				return true;
			}
			if (key == Keys.Right)
			{
				ShiftRightButton.PerformClick();
				return true;
			}
			return false;
		}
		#endregion

		private bool[,] ProcessImage(Func<bool[,], bool[,]> imageDataProcessor, FirmwareImageMetadata imageMetadata)
		{
			var processedData = imageDataProcessor(ImagePixelGrid.Data);
			m_firmware.WriteImage(processedData, imageMetadata);

			var updateCache = new Action(() =>
			{
				var cachedImage = FirmwareImageProcessor.CreateBitmap(processedData);
				ImageCacheManager.SetImage(imageMetadata.Index, imageMetadata.BlockType, cachedImage);

				ImageListBox.Invoke(new Action(() =>
				{
					var itemRect = ImageListBox.GetItemRectangle(imageMetadata.Index - 1);
					ImageListBox.Invalidate(itemRect);
				}));
			});
			updateCache.BeginInvoke(null, null);

			return processedData;
		}

		private void BlockImageRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			var currentListBoxSelectedIndices = ImageListBox.SelectedIndices.ToList();
			if (sender == Block1ImageRadioButton)
			{
				m_currentBlock = BlockType.Block1;
				Block1ImageListBox.Visible = true;
				Block2ImageListBox.Visible = false;
			}
			if (sender == Block2ImageRadioButton)
			{
				m_currentBlock = BlockType.Block2;
				Block1ImageListBox.Visible = false;
				Block2ImageListBox.Visible = true;
			}

			ImageListBox.Focus();
			if (currentListBoxSelectedIndices.Count != 0)
			{
				m_imageListBoxIsUpdating = true;
				ImageListBox.BeginUpdate();
				ImageListBox.SelectedIndices.Clear();
				ImageListBox.SelectedIndices.AddRange(currentListBoxSelectedIndices.Where(x => ImageListBox.Items.Count > x));
				ImageListBox.EndUpdate();

				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = LastSelectedImageMetadata != null ? m_firmware.ReadImage(LastSelectedImageMetadata) : new bool[5, 5];

				m_imageListBoxIsUpdating = false;
			}
			else
			{
				ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
			}
		}

		private void ImageListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (m_imageListBoxIsUpdating || LastSelectedImageMetadata == null) return;

			ImageSizeLabel.Text = string.Format("Image: {0}x{1}", LastSelectedImageMetadata.Width, LastSelectedImageMetadata.Height);
			try
			{
				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = m_firmware.ReadImage(LastSelectedImageMetadata);
			}
			catch (Exception)
			{
				InfoBox.Show("Invalid image data. Possibly firmware definition is incompatible with loaded firmware.");
			}
		}

		private void GridSizeUpDown_ValueChanged(object sender, EventArgs e)
		{
			ImagePixelGrid.BlockSize = m_configuration.GridSize = (int)GridSizeUpDown.Value;
		}

		private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ImagePixelGrid.ShowGrid = m_configuration.ShowGid = ShowGridCheckBox.Checked;
		}

		private void ClearAllPixelsButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Clear, LastSelectedImageMetadata);
		}

		private void InvertButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Invert, LastSelectedImageMetadata);
		}

		private void FlipHorizontalButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.FlipHorizontal, LastSelectedImageMetadata);
		}

		private void FlipVerticalButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.FlipVertical, LastSelectedImageMetadata);
		}

		private void ResizeButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			using (var rw = new ResizeImageWindow(LastSelectedImageMetadata.Width, LastSelectedImageMetadata.Height))
			{
				if (rw.ShowDialog() != DialogResult.OK) return;

				var newSize = rw.NewSize;
				LastSelectedImageMetadata.Width = (byte)newSize.Width;
				LastSelectedImageMetadata.Height = (byte)newSize.Height;
				ProcessImage(x => FirmwareImageProcessor.ResizeImage(x, newSize), LastSelectedImageMetadata);
				ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
			}
		}

		private void ShiftLeftButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftLeft, LastSelectedImageMetadata);
		}

		private void ShiftRightButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftRight, LastSelectedImageMetadata);
		}

		private void ShiftUpButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftUp, LastSelectedImageMetadata);
		}

		private void ShiftDownButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftDown, LastSelectedImageMetadata);
		}

		private void CopyButton_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			var images = m_firmware.ReadImages(SelectedImageMetadata).ToList();
			m_clipboardManager.SetData(images);
		}

		private void PasteButton_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			var copiedImages = m_clipboardManager.GetData();
			if (copiedImages.Count == 0) return;
			if (copiedImages.Count == 1)
			{
				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(data => FirmwareImageProcessor.PasteImage(data, copiedImages[0]), LastSelectedImageMetadata);
			}
			else
			{
				var originalImages = m_firmware.ReadImages(SelectedImageMetadata).ToList();

				var originalImagesCount = originalImages.Count;
				var copiedImagesCount = copiedImages.Count;
				var minimumImagesCount = Math.Min(originalImagesCount, copiedImagesCount);

				originalImages = originalImages.Take(minimumImagesCount).ToList();
				copiedImages = copiedImages.Take(minimumImagesCount).ToList();

				using (var importWindow = new ImportImageWindow(originalImages, copiedImages, originalImagesCount, copiedImagesCount))
				{
					if (importWindow.ShowDialog() != DialogResult.OK) return;
				}

				var updatedImage = new bool[5, 5];
				for (var i = 0; i < minimumImagesCount; i++)
				{
					var index = i;
					updatedImage = ProcessImage(x => FirmwareImageProcessor.PasteImage(originalImages[index], copiedImages[index]), SelectedImageMetadata[index]);
				}
				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = updatedImage;
			}
		}

		private void ImageEditorHotkeyInformationButton_Click(object sender, EventArgs e)
		{
			var sb = new StringBuilder();
			{
				sb.AppendLine("{0, -15} - {1}", "Copy", "Ctrl + C");
				sb.AppendLine("{0, -15} - {1}", "Paste", "Ctrl + V");
				sb.AppendLine("{0, -15} - {1}", "Clear", "Ctrl + C");
				sb.AppendLine("{0, -15} - {1}", "Invert", "Ctrl + I");
				sb.AppendLine("{0, -15} - {1}", "Shift Up", "Ctrl + Up");
				sb.AppendLine("{0, -15} - {1}", "Shift Down", "Ctrl + Down");
				sb.AppendLine("{0, -15} - {1}", "Shift Left", "Ctrl + Left");
				sb.AppendLine("{0, -15} - {1}", "Shift Right", "Ctrl + Right");
			}
			InfoBox.Show(sb.ToString());
		}

		private void ExportContextMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			string fileName;
			using (var sf = new SaveFileDialog { Filter = Consts.ExportImageFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			var images = SelectedImageMetadata.Select(x =>
			{
				var imageData = m_firmware.ReadImage(x);
				return new ExportedImage(x.Index, imageData);
			}).ToList();
			ImageExportManager.Export(fileName, images);
		}

		private void ImportContextMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			string fileName;
			using (var op = new OpenFileDialog { Filter = Consts.ExportImageFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			var exportedImages = ImageExportManager.Import(fileName);
			if (exportedImages.Count == 0) return;

			var originalImages = m_firmware.ReadImages(SelectedImageMetadata).ToList();
			var importedImages = exportedImages.Select(x => x.Data).ToList();

			var originalImagesCount = originalImages.Count;
			var importedImagesCount = importedImages.Count;
			var minimumImagesCount = Math.Min(originalImagesCount, importedImagesCount);

			originalImages = originalImages.Take(minimumImagesCount).ToList();
			importedImages = importedImages.Take(minimumImagesCount).ToList();

			using (var importWindow = new ImportImageWindow(originalImages, importedImages, originalImagesCount, importedImagesCount))
			{
				if (importWindow.ShowDialog() != DialogResult.OK) return;
			}

			for (var i = 0; i < minimumImagesCount; i++)
			{
				var index = i;
				ProcessImage(x => FirmwareImageProcessor.PasteImage(originalImages[index], importedImages[index]), SelectedImageMetadata[index]);
			}

			var lastSelectedItem = ImageListBox.SelectedIndices[ImageListBox.SelectedIndices.Count - 1];
			ImageListBox.SelectedIndices.Clear();
			ImageListBox.SelectedIndex = lastSelectedItem;
		}

		private static void ImageListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			var listBox = sender as ListBox;

			if (listBox == null) return;
			if (e.Index < 0) return;

			var item = listBox.Items[e.Index] as ImagedItem<FirmwareImageMetadata>;
			if (item == null) return;

			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.InterpolationMode = InterpolationMode.Low;
			e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
			e.DrawBackground();

			var itemText = item.ToString();
			var textSize = e.Graphics.MeasureString(itemText, e.Font);

			try
			{
				var image = ImageCacheManager.GetImage(item.ImageCacheIndex, item.Value.BlockType);
				if (image.Width > ImagedListBoxItemMaxHeight || image.Height > ImagedListBoxItemMaxHeight)
				{
					e.Graphics.DrawImage(image, e.Bounds.X + ImagedListBoxItemImageMargin, e.Bounds.Y + ImagedListBoxItemImageMargin, ImagedListBoxItemMaxHeight, ImagedListBoxItemMaxHeight);
				}
				else
				{
					e.Graphics.DrawImage(image, e.Bounds.X + ImagedListBoxItemImageMargin, e.Bounds.Y + (int)(e.Bounds.Height / 2f - image.Height / 2f), image.Width, image.Height);
				}
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}

			e.Graphics.DrawString(itemText, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + ImagedListBoxItemMaxHeight + ImagedListBoxItemImageMargin * 2, e.Bounds.Y + (int)(e.Bounds.Height / 2f - textSize.Height / 2f));
			e.DrawFocusRectangle();
		}

		private static void ImageListBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = ImagedListBoxItemMaxHeight + ImagedListBoxItemImageMargin * 2;
		}
	}
}
