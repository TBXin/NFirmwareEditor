using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class ImageEditorTabPage : UserControl, IEditorTabPage
	{
		private readonly ResourcePackManager m_resourcePackManager;
		private readonly ClipboardManager m_clipboardManager = new ClipboardManager();
		private readonly StringFormat m_listBoxStringFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

		private Configuration m_configuration;
		private Firmware m_firmware;
		private BlockType m_currentBlock = BlockType.Block1;
		private bool m_imageListBoxIsUpdating;

		public ImageEditorTabPage(ResourcePackManager resourcePackManager)
		{
			m_resourcePackManager = resourcePackManager;
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

		[NotNull]
		public IEnumerable<FirmwareImageMetadata> ImageBlockMetadatas
		{
			get
			{
				switch (m_currentBlock)
				{
					case BlockType.Block1: return m_firmware.Block1Images;
					case BlockType.Block2: return m_firmware.Block2Images;
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

		public void Initialize(IEditorTabPageHost host, Configuration configuration)
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

			ImagePixelGrid.BlockSize = m_configuration.GridSize;
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
			ResizeButton.Click += ResizeButton_Click;

			CopyButton.Click += CopyButton_Click;
			PasteButton.Click += PasteButton_Click;
			BitmapImportButton.Click += BitmapImportButton_Click;

			FlipHorizontalButton.Click += FlipHorizontalButton_Click;
			FlipVerticalButton.Click += FlipVerticalButton_Click;

			ShiftLeftButton.Click += ShiftLeftButton_Click;
			ShiftRightButton.Click += ShiftRightButton_Click;
			ShiftUpButton.Click += ShiftUpButton_Click;
			ShiftDownButton.Click += ShiftDownButton_Click;

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
			Block1ImageRadioButton.Enabled = false;
			Block2ImageRadioButton.Enabled = false;
			Block1ImageRadioButton.Checked = false;
			Block2ImageRadioButton.Checked = false;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = new bool[5, 5];
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			Block1ImageRadioButton.Enabled = true;
			Block1ImageRadioButton.Checked = true;
			Block2ImageRadioButton.Enabled = m_firmware.Block2Images.Any();

			Block2ImageListBox.Fill(m_firmware.Block2Images.Select(x => new ImagedItem<FirmwareImageMetadata>(x, x.Index, string.Format("0x{0:X2}", x.Index))), true);
			Block1ImageListBox.Fill(m_firmware.Block1Images.Select(x => new ImagedItem<FirmwareImageMetadata>(x, x.Index, string.Format("0x{0:X2}", x.Index))), true);
		}

		public void OnActivate()
		{
		}

		public bool OnHotkey(Keys keyData)
		{
			if (!keyData.HasFlag(Keys.Control)) return false;

			var key = keyData & ~Keys.Control;
			switch (key)
			{
				case Keys.N:
					ClearAllPixelsButton.PerformClick();
					return true;
				case Keys.I:
					InverseButton.PerformClick();
					return true;
				case Keys.R:
					ResizeButton.PerformClick();
					return true;
				case Keys.C:
					CopyButton.PerformClick();
					return true;
				case Keys.V:
					PasteButton.PerformClick();
					return true;
				case Keys.A:
					ImageListBox.BeginUpdate();
					ImageListBox.SelectedIndices.Clear();
					ImageListBox.SelectedIndices.AddRange(Enumerable.Range(0, ImageListBox.Items.Count));
					ImageListBox.EndUpdate();
					return true;
				case Keys.Up:
					ShiftUpButton.PerformClick();
					return true;
				case Keys.Down:
					ShiftDownButton.PerformClick();
					return true;
				case Keys.Left:
					ShiftLeftButton.PerformClick();
					return true;
				case Keys.Right:
					ShiftRightButton.PerformClick();
					return true;
			}
			return false;
		}
		#endregion

		private void ImportResourcePack([NotNull] ResourcePack resourcePack)
		{
			if (resourcePack == null) throw new ArgumentNullException("resourcePack");
			if (resourcePack.Images == null || resourcePack.Images.Count == 0) return;

			var originalImageIndices = resourcePack.Images.Select(x => x.Index).ToList();
			var importedImages = resourcePack.Images.Select(x => x.Data).ToList();

			ImportImages(originalImageIndices, importedImages, true);
		}

		private void ImportImages([NotNull] IList<int> originalImageIndices, [NotNull] IList<bool[,]> importedImages, bool importResourcePack)
		{
			if (importedImages == null) throw new ArgumentNullException("importedImages");
			if (originalImageIndices == null) throw new ArgumentNullException("originalImageIndices");
			if (importedImages.Count == 0) return;

			var minimumImagesCount = Math.Min(originalImageIndices.Count, importedImages.Count);

			originalImageIndices = originalImageIndices.Take(minimumImagesCount).ToList();
			importedImages = importedImages.Take(minimumImagesCount).ToList();

			ImageImportMode importMode;
			using (var importWindow = new ImportImageWindow(m_firmware, originalImageIndices, importedImages, importResourcePack))
			{
				if (importWindow.ShowDialog() != DialogResult.OK) return;

				importMode = importWindow.GetImportMode();
				importedImages = importWindow.GetImportedImages().ToList();
			}

			for (var i = 0; i < minimumImagesCount; i++)
			{
				var index = i;
				if (importMode == ImageImportMode.Block1)
				{
					ProcessImage(x => importedImages[index], m_firmware.Block1Images.First(x => x.Index == originalImageIndices[index]));
				}
				else if (importMode == ImageImportMode.Block2)
				{
					ProcessImage(x => importedImages[index], m_firmware.Block2Images.First(x => x.Index == originalImageIndices[index]));
				}
				else
				{
					ProcessImage(x => importedImages[index], m_firmware.Block1Images.First(x => x.Index == originalImageIndices[index]));
					ProcessImage(x => importedImages[index], m_firmware.Block2Images.First(x => x.Index == originalImageIndices[index]));
				}
			}

			ImageCacheManager.RebuildImageCache(m_firmware);
			ImageListBox.Invalidate();
			ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
		}

		private bool[,] ProcessImage(Func<bool[,], bool[,]> imageDataProcessor, FirmwareImageMetadata imageMetadata, bool rebuildCache = false)
		{
			var processedData = imageDataProcessor(ImagePixelGrid.Data);
			var processedImageSize = processedData.GetSize();
			var imageSizeChanged = imageMetadata.Width != processedImageSize.Width || imageMetadata.Height != processedImageSize.Height;

			imageMetadata.Width = (byte)processedImageSize.Width;
			imageMetadata.Height = (byte)processedImageSize.Height;

			m_firmware.WriteImage(processedData, imageMetadata);

			if (imageSizeChanged || rebuildCache)
			{
				ImageCacheManager.RebuildImageCache(m_firmware);
				ImageListBox.Invalidate();
			}
			else
			{
				var cachedImage = FirmwareImageProcessor.CreateBitmap(processedData);
				ImageCacheManager.SetImage(imageMetadata.Index, imageMetadata.BlockType, cachedImage);

				var updateCache = new Action(() =>
				{
					ImageListBox.Invoke(new Action(() =>
					{
						var itemRect = ImageListBox.GetItemRectangle(imageMetadata.Index - 1);
						ImageListBox.Invalidate(itemRect);
					}));
				});
				updateCache.BeginInvoke(null, null);
			}

			return processedData;
		}

		private void UpdateImageStatusLabel([CanBeNull] FirmwareImageMetadata metadata)
		{
			if (metadata == null) return;

			ImageSizeLabel.Text = string.Format("Image: {0}x{1}, Offset: 0x{2:X4}, Length: {3} bytes", metadata.Width, metadata.Height, metadata.DataOffset, metadata.DataLength + 2);
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
				ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
			UpdateImageStatusLabel(LastSelectedImageMetadata);
		}

		private void ImageListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (m_imageListBoxIsUpdating || LastSelectedImageMetadata == null) return;

			UpdateImageStatusLabel(LastSelectedImageMetadata);
			try
			{
				var image = m_firmware.ReadImage(LastSelectedImageMetadata);
				var imageSize = image.GetSize();

				ImagePreviewPixelGrid.BlockSize = imageSize.Height > 64 ? 1 : 2;
				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = image;
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
				ProcessImage(x => FirmwareImageProcessor.ResizeImage(x, newSize), LastSelectedImageMetadata, true);
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
			ImportImages(SelectedImageMetadata.Select(x => x.Index).ToList(), copiedImages, false);
		}

		private void BitmapImportButton_Click(object sender, EventArgs eventArgs)
		{
			if (LastSelectedImageMetadata == null) return;

			using (var op = new OpenFileDialog { Filter = Consts.BitmapImportFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				try
				{
					var bitmapFile = op.FileName;
					using (var bitmap = (Bitmap)Image.FromFile(bitmapFile, true))
					{
						if (bitmap.Width > Consts.MaximumImageWidthAndHeight || bitmap.Height > Consts.MaximumImageWidthAndHeight)
						{
							InfoBox.Show("Image is too big. Image width and height must be lower or equals to {0} pixels.", Consts.MaximumImageWidthAndHeight);
							return;
						}
						var imageData = FirmwareImageProcessor.ImportBitmap(bitmap);
						ProcessImage(x => imageData, LastSelectedImageMetadata, true);
						ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
					}
				}
				catch (Exception ex)
				{
					InfoBox.Show("Unable to import bitmap image.\n" + ex.Message);
				}
			}
		}

		private void ImageEditorHotkeyInformationButton_Click(object sender, EventArgs e)
		{
			new HotkeyHelpWindow().ShowDialog();
		}

		private void ExportContextMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			string fileName;
			using (var sf = new SaveFileDialog { Filter = Consts.ExportResourcePackFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			var images = SelectedImageMetadata.Select(x =>
			{
				var imageData = m_firmware.ReadImage(x);
				var imageSize = imageData.GetSize();
				return new ExportedImage(x.Index, imageSize, imageData);
			}).ToList();
			var resourcePack = new ResourcePack(m_firmware.Definition.Name, images);
			m_resourcePackManager.SaveToFile(fileName, resourcePack);
		}

		private void ImportContextMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			string fileName;
			using (var op = new OpenFileDialog { Filter = Consts.ExportResourcePackFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			var resourcePack = m_resourcePackManager.LoadFromFile(fileName);
			if (resourcePack == null || string.IsNullOrEmpty(resourcePack.Definition)) return;
			if (resourcePack.Definition != m_firmware.Definition.Name)
			{
				InfoBox.Show("Selected resource pack is incompatible with the loaded firmware.\nResource pack is designed for: " + resourcePack.Definition + "\nOpend firmware is: " + m_firmware.Definition.Name);
				return;
			}

			ImportResourcePack(resourcePack);
		}

		private void ImageListBox_DrawItem(object sender, DrawItemEventArgs e)
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

			try
			{
				var imageScale = 1f;
				var image = ImageCacheManager.GetImage(item.ImageCacheIndex, item.Value.BlockType);

				var greatestDimension = Math.Max(image.Width, image.Height);
				if (greatestDimension > Consts.ImageListBoxItemMaxHeight) imageScale = (float)greatestDimension / Consts.ImageListBoxItemMaxHeight;

				var resultWidth = image.Width / imageScale;
				var resultHeight = image.Height / imageScale;

				e.Graphics.DrawImage(image, e.Bounds.X + Consts.ImageListBoxItemImageMargin, e.Bounds.Y + (int)(e.Bounds.Height / 2f - resultHeight / 2f), resultWidth, resultHeight);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}

			var stringRectX = e.Bounds.X + Consts.ImageListBoxItemMaxHeight + Consts.ImageListBoxItemImageMargin * 2;
			e.Graphics.DrawString(itemText, e.Font, new SolidBrush(e.ForeColor), new RectangleF(stringRectX, e.Bounds.Y, e.Bounds.Width - stringRectX - Consts.ImageListBoxItemImageMargin, e.Bounds.Height), m_listBoxStringFormat);
			e.DrawFocusRectangle();
		}

		private static void ImageListBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = Consts.ImageListBoxItemMaxHeight + Consts.ImageListBoxItemImageMargin;

			var listBox = sender as ListBox;

			if (listBox == null) return;
			if (e.Index < 0) return;

			var item = listBox.Items[e.Index] as ImagedItem<FirmwareImageMetadata>;
			if (item == null) return;

			try
			{
				var cachedImage = ImageCacheManager.GetImage(item.ImageCacheIndex, item.Value.BlockType);
				e.ItemHeight = Math.Min(e.ItemHeight, cachedImage.Height + Consts.ImageListBoxItemImageMargin);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}
		}
	}
}
