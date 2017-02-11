using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Storages;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class ImageEditorTabPage : UserControl, IEditorTabPage
	{
		private readonly IEnumerable<FirmwareDefinition> m_definitions;
		private readonly ResourcePacksStorage m_resourcePackStorage;
		private readonly ClipboardManager m_clipboardManager = new ClipboardManager();
		private readonly StringFormat m_listBoxStringFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

		private ApplicationConfiguration m_configuration;
		private Firmware m_firmware;
		private BlockType m_currentBlock = BlockType.Block1;
		private bool m_imageListBoxIsUpdating;

		public ImageEditorTabPage(IEnumerable<FirmwareDefinition> definitions, ResourcePacksStorage resourcePackStorage)
		{
			m_definitions = definitions;
			m_resourcePackStorage = resourcePackStorage;

			InitializeComponent();
			InitializeControls();
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
		public bool IsDirty { get; set; }

		public string Title
		{
			get { return "Images"; }
		}

		public void Initialize(IEditorTabPageHost host, ApplicationConfiguration configuration)
		{
			m_configuration = configuration;
			GridSizeUpDown.Value = m_configuration.GridSize;
			ShowGridCheckBox.Checked = m_configuration.ShowGid;
			ImagePixelGrid.BlockSize = m_configuration.GridSize;
			ImagePixelGrid.SingleMouseButtonMode = configuration.ImageEditorMouseMode == ImageEditorMouseMode.LeftSetUnset;
		}

		public void OnWorkspaceReset()
		{
			Block1ImageListBox.Items.Clear();
			Block2ImageListBox.Items.Clear();
			Block1ImageRadioButton.Enabled = false;
			Block2ImageRadioButton.Enabled = false;
			Block1ImageRadioButton.Checked = false;
			Block2ImageRadioButton.Checked = false;

			ImageStatusLabel.Text = null;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = new bool[0, 0];
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			Block1ImageRadioButton.Enabled = m_firmware.Block1Images.Any();
			Block2ImageRadioButton.Enabled = m_firmware.Block2Images.Any();

			if (Block2ImageRadioButton.Enabled) Block2ImageRadioButton.Checked = true;
			if (Block1ImageRadioButton.Enabled) Block1ImageRadioButton.Checked = true;

			Block2ImageListBox.Fill(m_firmware.Block2Images.Values.Select(x => new ImagedItem<FirmwareImageMetadata>(x, x.Index, string.Format("0x{0:X2}", x.Index))), true);
			Block1ImageListBox.Fill(m_firmware.Block1Images.Values.Select(x => new ImagedItem<FirmwareImageMetadata>(x, x.Index, string.Format("0x{0:X2}", x.Index))), true);
		}

		public void OnActivate()
		{
		}

		public bool OnHotkey(Keys keyData)
		{
			if (ModifierKeys.HasFlag(Keys.Control))
			{
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
					case Keys.Z:
						ImagePixelGrid.Undo();
						return true;
				}
			}
			if (ModifierKeys.HasFlag(Keys.Shift))
			{
				var key = keyData & ~(Keys.Control | Keys.Shift);
				switch (key)
				{
					case Keys.Z:
						ImagePixelGrid.Redo();
						return true;
				}
			}

			return false;
		}
		#endregion

		private void InitializeControls()
		{
			ImageStatusLabel.Text = null;

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
			ResizeButton.Click += ResizeButton_Click;

			CopyButton.Click += CopyButton_Click;
			PasteButton.Click += PasteButton_Click;
			BitmapImportButton.Click += BitmapImportButton_Click;

			ShiftLeftButton.Click += ShiftLeftButton_Click;
			ShiftRightButton.Click += ShiftRightButton_Click;
			ShiftUpButton.Click += ShiftUpButton_Click;
			ShiftDownButton.Click += ShiftDownButton_Click;

			FlipHorizontalButton.Click += FlipHorizontalButton_Click;
			FlipVerticalButton.Click += FlipVerticalButton_Click;

			UndoButton.Click += UndoButton_Click;
			RedoButton.Click += RedoButton_Click;

			RotateAntiClockwiseButton.Click += RotateAntiClockwiseButton_Click;
			RotateClockwiseButton.Click += RotateClockwiseButton_Click;

			ImageEditorHotkeyInformationButton.Click += ImageEditorHotkeyInformationButton_Click;

			CopyContextMenuItem.Click += CopyButton_Click;
			PasteContextMenuItem.Click += PasteButton_Click;
			ImportFontMenuItem.Click += ImportFontMenuItem_Click;
			ExportBitmapMenuItem.Click += ExportBitmapMenuItem_Click;
			ExportResourcePackContextMenuItem.Click += ExportResourcePackContextMenuItem_Click;
			UpdateResourcePackContextMenuItem.Click += UpdateResourcePackContextMenuItem_Click;
		}

		private void ImportImages([NotNull] IList<int> originalImageIndices, [NotNull] IList<bool[,]> importedImages)
		{
			if (importedImages == null) throw new ArgumentNullException("importedImages");
			if (originalImageIndices == null) throw new ArgumentNullException("originalImageIndices");
			if (importedImages.Count == 0) return;

			var minimumImagesCount = Math.Min(originalImageIndices.Count, importedImages.Count);

			originalImageIndices = originalImageIndices.Take(minimumImagesCount).ToList();
			importedImages = importedImages.Take(minimumImagesCount).ToList();

			ImageImportMode importMode;
			bool allowResizeOriginalImages;

			using (var importWindow = new PreviewResourcePackWindow(m_firmware, originalImageIndices, importedImages, false, m_currentBlock))
			{
				importWindow.Text = Consts.ApplicationTitleWoVersion + @" - Paste image(s)";
				importWindow.ImportButtonText = "Paste";
				if (importWindow.ShowDialog() != DialogResult.OK) return;

				importMode = importWindow.GetImportMode();
				allowResizeOriginalImages = importWindow.AllowResizeOriginalImages;
			}

			for (var i = 0; i < minimumImagesCount; i++)
			{
				var index = i;
				var originalImageIndex = originalImageIndices[index];
				var importedImage = importedImages[index];

				if (importMode == ImageImportMode.Block1)
				{
					ImportBlockImage(m_firmware.Block1Images, originalImageIndex, importedImage, allowResizeOriginalImages);
				}
				else if (importMode == ImageImportMode.Block2)
				{
					ImportBlockImage(m_firmware.Block2Images, originalImageIndex, importedImage, allowResizeOriginalImages);
				}
				else
				{
					ImportBlockImage(m_firmware.Block1Images, originalImageIndex, importedImage, allowResizeOriginalImages);
					ImportBlockImage(m_firmware.Block2Images, originalImageIndex, importedImage, allowResizeOriginalImages);
				}
			}

			ImageCacheManager.RebuildCache(m_firmware);
			ImageListBox.Invalidate();
			ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
		}

		private void ImportBlockImage(IDictionary<int, FirmwareImageMetadata> blockMetadataDictionary, int imageIndex, bool[,] importedImage, bool allowResizeOriginalImages)
		{
			FirmwareImageMetadata imageMetadata;
			if (!blockMetadataDictionary.TryGetValue(imageIndex, out imageMetadata)) return;

			if (allowResizeOriginalImages)
			{
				ProcessImage(x => importedImage, imageMetadata);
			}
			else
			{
				ProcessImage(x => FirmwareImageProcessor.PasteImage(imageMetadata.CreateImage(), importedImage), imageMetadata);
			}
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
				ImageCacheManager.RebuildCache(m_firmware);
				ImageListBox.Invalidate();
			}
			else
			{
				var cachedImage = BitmapProcessor.CreateBitmapFromRaw(processedData);
				ImageCacheManager.SetGlyphImage(imageMetadata.Index, imageMetadata.BlockType, cachedImage);
				ImageCacheManager.RebuildStringImageCache(m_firmware, imageMetadata.BlockType);
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

			IsDirty = true;
			return processedData;
		}

		private void UpdateImageStatusLabel([CanBeNull] FirmwareImageMetadata metadata)
		{
			if (metadata == null) return;

			ImageStatusLabel.Text = string.Format
			(
				"Image: 0x{0:X2}, Size: {1}x{2}, Reference: 0x{3:X4}, Data: 0x{4:X4}, Length: {5} bytes",
				metadata.Index,
				metadata.Width,
				metadata.Height,
				metadata.ImageReferenceOffset,
				metadata.DataOffset,
				metadata.DataLength + 2
			);

			var selectedItemsCount = ImageListBox.SelectedIndices.Count;
			if (selectedItemsCount > 1)
			{
				if (!ImageListBoxStatusStrip.Visible) ImageListBoxStatusStrip.Visible = true;
				ImageListBoxStatusLabel.Text = @"Selected: " + selectedItemsCount;
			}
			else
			{
				ImageListBoxStatusStrip.Visible = false;
			}
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
			else ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
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
				ImagePixelGrid.ClearHistory();
			}
			catch (Exception)
			{
				InfoBox.Global.Show("Invalid image data. Possibly firmware definition is incompatible with loaded firmware.");
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

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Clear, LastSelectedImageMetadata);
		}

		private void InvertButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Invert, LastSelectedImageMetadata);
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

		private void FlipHorizontalButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.FlipHorizontal, LastSelectedImageMetadata);
		}

		private void FlipVerticalButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.FlipVertical, LastSelectedImageMetadata);
		}

		private void UndoButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Undo();
		}

		private void RedoButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Redo();
		}

		private void RotateClockwiseButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(x => FirmwareImageProcessor.Rotate(x, true), LastSelectedImageMetadata);
		}

		private void RotateAntiClockwiseButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(x => FirmwareImageProcessor.Rotate(x, false), LastSelectedImageMetadata);
		}

		private void ShiftLeftButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftLeft, LastSelectedImageMetadata);
		}

		private void ShiftRightButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftRight, LastSelectedImageMetadata);
		}

		private void ShiftUpButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftUp, LastSelectedImageMetadata);
		}

		private void ShiftDownButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.CreateUndo();
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
			ImportImages(SelectedImageMetadata.Select(x => x.Index).ToList(), copiedImages);
		}

		private void ImportFontMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			using (var importFontWindow = new ImportFontWindow(SelectedImageMetadata))
			{
				if (importFontWindow.ShowDialog() != DialogResult.OK) return;

				var importedData = importFontWindow.GetImportedData();
				foreach (var imageDataTuple in importedData)
				{
					var metadata = imageDataTuple.Item1;
					var imageData = imageDataTuple.Item2;

					ProcessImage(x => imageData, metadata);
				}
				ImageCacheManager.RebuildCache(m_firmware);
				ImageListBox.Invalidate();
				ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
			}
		}

		private void BitmapImportButton_Click(object sender, EventArgs eventArgs)
		{
			if (LastSelectedImageMetadata == null) return;

			try
			{
				using (var imageConverterWindow = new ImageConverterWindow(true, LastSelectedImageMetadata.Width, LastSelectedImageMetadata.Height))
				{
					if (imageConverterWindow.ShowDialog() != DialogResult.OK) return;

					using (var monochrome = imageConverterWindow.GetConvertedImage())
					{
						var imageData = BitmapProcessor.CreateRawFromBitmap(monochrome);
						ImagePixelGrid.CreateUndo();
						ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(x => FirmwareImageProcessor.PasteImage(x, imageData), LastSelectedImageMetadata, true);
					}
				}
			}
			catch (Exception ex)
			{
				InfoBox.Global.Show("Unable to import bitmap image.\n" + ex.Message);
			}
		}

		private void ImageEditorHotkeyInformationButton_Click(object sender, EventArgs e)
		{
			new HotkeyHelpWindow().ShowDialog();
		}

		private void ExportBitmapMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			string directoryPath;
			using (var fb = new FolderBrowserDialog())
			{
				if (fb.ShowDialog() != DialogResult.OK) return;
				directoryPath = fb.SelectedPath;
			}

			var exportData = SelectedImageMetadata.Select(x => new { Metadata = x, ImageData = m_firmware.ReadImage(x) });
			foreach (var data in exportData)
			{
				try
				{
					using (var image = BitmapProcessor.Create1BitBitmapFromRaw(data.ImageData))
					{
						var fileName = Path.Combine(directoryPath, "0x" + data.Metadata.Index.ToString("X2") + Consts.BitmapFileExtensionWoAsterisk);
						image.Save(fileName, ImageFormat.Bmp);
					}
				}
				catch
				{
					// Ignore
				}
			}
		}

		private void ExportResourcePackContextMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			var images = SelectedImageMetadata.Select(x =>
			{
				var imageData = m_firmware.ReadImage(x);
				var imageSize = imageData.GetSize();
				return new ExportedImage(x.Index, imageSize, imageData);
			}).ToList();

			using (var createResourcePackWindow = new CreateResourcePackWindow(m_resourcePackStorage, m_definitions, m_firmware.Definition.Name, images, null))
			{
				createResourcePackWindow.ShowDialog();
			}
		}

		private void UpdateResourcePackContextMenuItem_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0) return;

			string fileName;
			using (var op = new OpenFileDialog { Filter = Consts.ExportResourcePackFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			var resourcePack = m_resourcePackStorage.TryLoad(fileName);
			var images = SelectedImageMetadata.Select(x =>
			{
				var imageData = m_firmware.ReadImage(x);
				var imageSize = imageData.GetSize();
				return new ExportedImage(x.Index, imageSize, imageData);
			}).ToList();

			using (var createResourcePackWindow = new CreateResourcePackWindow(m_resourcePackStorage, m_definitions, m_firmware.Definition.Name, images, resourcePack))
			{
				createResourcePackWindow.ShowDialog();
			}
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
				var image = ImageCacheManager.GetGlyphImage(item.ImageCacheIndex, item.Value.BlockType);

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
				var cachedImage = ImageCacheManager.GetGlyphImage(item.ImageCacheIndex, item.Value.BlockType);
				e.ItemHeight = Math.Min(e.ItemHeight, cachedImage.Height + Consts.ImageListBoxItemImageMargin);
			}
			catch (ObjectDisposedException)
			{
				// Ignore
			}
		}
	}
}
