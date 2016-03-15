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
using NFirmwareEditor.Properties;
using NFirmwareEditor.UI;

namespace NFirmwareEditor
{
	public partial class MainWindow : Form
	{
		private const int ImagedListBoxItemMaxHeight = 24 * 2;
		private const int ImagedListBoxItemImageMargin = 6;

		private readonly ConfigurationManager m_configurationManager = new ConfigurationManager();
		private readonly FirmwareDefinitionManager m_firmwareDefinitionManager = new FirmwareDefinitionManager();
		private readonly FirmwareLoader m_loader = new FirmwareLoader(new FirmwareEncoder());
		private readonly ClipboardManager m_clipboardManager = new ClipboardManager();

		private Configuration m_configuration;
		private IEnumerable<FirmwareDefinition> m_definitions; 
		private Firmware m_firmware;

		private Dictionary<int, Image> m_block1ImageCache = new Dictionary<int, Image>();
		private Dictionary<int, Image> m_block2ImageCache = new Dictionary<int, Image>();

		private bool m_imageListBoxIsUpdating;

		public MainWindow()
		{
			InitializeComponent();
			InitializeControls();

			Icon = Paths.ApplicationIcon;
			LoadSettings();
		}

		public IDictionary<int, Image> ImageCacheForImageListBox
		{
			get { return Block1ImageListBox.Visible ? m_block1ImageCache : m_block2ImageCache; }
		}

		public IDictionary<int, Image> ImageCacheForStringListBox
		{
			get { return Block1StringCheckBox.Checked ? m_block1ImageCache : m_block2ImageCache; }
		}

		public IEnumerable<FirmwareImageMetadata> CurrentImageBlockForStrings
		{
			get { return Block1StringListBox.Visible ? m_firmware.Block1Images : m_firmware.Block2Images; }
		}

		[NotNull]
		public ListBox ImageListBox
		{
			get { return Block1ImageListBox.Visible ? Block1ImageListBox : Block2ImageListBox; }
		}

		[NotNull]
		public ListBox StringListBox
		{
			get { return Block1StringCheckBox.Checked ? Block1StringListBox : Block2StringListBox; }
		}

		[CanBeNull]
		public FirmwareImageMetadata LastSelectedImageMetadata
		{
			get
			{
				if (ImageListBox.Items.Count == 0 || ImageListBox.SelectedIndices.Count == 0) return null;

				var item = ImageListBox.Items[ImageListBox.SelectedIndices[ImageListBox.SelectedIndices.Count - 1]] as ImagedListBoxItem<FirmwareImageMetadata>;
				return item != null ? item.Value : null;
			}
		}

		[CanBeNull]
		public FirmwareStringMetadata LastSelectedStringMetadata
		{
			get
			{
				return StringListBox.Items.Count == 0 || StringListBox.SelectedIndices.Count == 0
					? null
					: StringListBox.Items[StringListBox.SelectedIndices[StringListBox.SelectedIndices.Count - 1]] as FirmwareStringMetadata;
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
					var metadata = ImageListBox.Items[selectedIndex] as ImagedListBoxItem<FirmwareImageMetadata>;
					if (metadata == null) continue;

					result.Add(metadata.Value);
				}
				return result;
			}
		}

		private void InitializeControls()
		{
			ImagePreviewPixelGrid.BlockInnerBorderPen = StringPrewviewPixelGrid.BlockInnerBorderPen = Pens.Transparent;
			ImagePreviewPixelGrid.BlockOuterBorderPen = StringPrewviewPixelGrid.BlockOuterBorderPen = Pens.Transparent;
			ImagePreviewPixelGrid.ActiveBlockBrush = StringPrewviewPixelGrid.ActiveBlockBrush = Brushes.White;
			ImagePreviewPixelGrid.InactiveBlockBrush = StringPrewviewPixelGrid.InactiveBlockBrush = Brushes.Black;

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

			MainTabControl.Selected += (s, e) =>
			{
				if (e.TabPage != StringsTabPage) return;

				StringListBox.Focus();
				StringListBox_SelectedValueChanged(StringListBox, EventArgs.Empty);
			};

			Block1ImageListBox.DrawMode = DrawMode.OwnerDrawVariable;
			Block1ImageListBox.MeasureItem += ImageListBox_MeasureItem;
			Block1ImageListBox.DrawItem += ImageListBox_DrawItem;

			Block2ImageListBox.DrawMode = DrawMode.OwnerDrawVariable;
			Block2ImageListBox.MeasureItem += ImageListBox_MeasureItem;
			Block2ImageListBox.DrawItem += ImageListBox_DrawItem;
		}

		private void ImageListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			var listBox = sender as ListBox;

			if (listBox == null) return;
			if (e.Index < 0) return;

			var item = listBox.Items[e.Index] as ImagedListBoxItem<FirmwareImageMetadata>;
			if (item == null) return;

			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.InterpolationMode = InterpolationMode.Low;
			e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
			e.DrawBackground();

			var itemText = item.ToString();
			var textSize = e.Graphics.MeasureString(itemText, e.Font);

			try
			{
				if (item.Image != null)
				{
					if (item.Image.Width > ImagedListBoxItemMaxHeight || item.Image.Height > ImagedListBoxItemMaxHeight)
					{
						e.Graphics.DrawImage(item.Image, e.Bounds.X + ImagedListBoxItemImageMargin, e.Bounds.Y + ImagedListBoxItemImageMargin, ImagedListBoxItemMaxHeight, ImagedListBoxItemMaxHeight);
					}
					else
					{
						e.Graphics.DrawImage(item.Image, e.Bounds.X + ImagedListBoxItemImageMargin, e.Bounds.Y + (int)(e.Bounds.Height / 2f - item.Image.Height / 2f), item.Image.Width, item.Image.Height);
					}
				}
			}
			catch(ObjectDisposedException)
			{
			}

			e.Graphics.DrawString(itemText, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + ImagedListBoxItemMaxHeight + ImagedListBoxItemImageMargin * 2, e.Bounds.Y + (int)(e.Bounds.Height / 2f - textSize.Height / 2f));
			e.DrawFocusRectangle();
		}

		private void ImageListBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = ImagedListBoxItemMaxHeight + ImagedListBoxItemImageMargin * 2;
		}

		private void ResetWorkspace()
		{
			Block1ImageListBox.Items.Clear();
			Block2ImageListBox.Items.Clear();
			Block1StringListBox.Items.Clear();
			Block2StringListBox.Items.Clear();
			RemoveStringEditControls();
			ImagePixelGrid.Data = StringPrewviewPixelGrid.Data = new bool[5, 5];
			StatusLabel.Text = null;

			Block1ImageCheckBox.Checked = true;
			Block1StringCheckBox.Checked = true;
			MainTabControl.SelectedIndex = 0;

			SaveEncryptedMenuItem.Enabled = false;
			SaveDecryptedMenuItem.Enabled = false;
			EditMenuItem.Enabled = false;
		}

		private void LoadSettings()
		{
			m_configuration = m_configurationManager.Load();

			Size = new Size(m_configuration.MainWindowWidth, m_configuration.MainWindowHeight);
			WindowState = m_configuration.MainWindowMaximaged ? FormWindowState.Maximized : FormWindowState.Normal;

			m_definitions = m_firmwareDefinitionManager.Load();
			foreach (var definition in m_definitions)
			{
				var firmwareDefinition = definition;
				OpenEncryptedManualMenuItem.DropDownItems.Add(definition.Name, OpenEncryptedManualMenuItem.Image, (s, e) =>
				{
					OpenDialogAndReadFirmwareOnOk(firmwareDefinition.Name, fileName => m_loader.LoadEncrypted(fileName, firmwareDefinition));
				});
				OpenDecryptedManualMenuItem.DropDownItems.Add(definition.Name, OpenDecryptedManualMenuItem.Image, (s, e) =>
				{
					OpenDialogAndReadFirmwareOnOk(firmwareDefinition.Name, fileName => m_loader.LoadDecrypted(fileName, firmwareDefinition));
				});
			}

			GridSizeUpDown.Value = m_configuration.GridSize;
			ShowGridCheckBox.Checked = m_configuration.ShowGid;
		}

		private void RebuildImageCache([NotNull] Firmware firmware)
		{
			if (firmware == null) throw new ArgumentNullException("firmware");

			var block1ImageCache = new Dictionary<int, Image>();
			foreach (var imageMetadata in firmware.Block1Images)
			{
				try
				{
					var imageData = firmware.ReadImage(imageMetadata);
					var image = FirmwareImageProcessor.CreateImage(imageData);
					block1ImageCache[imageMetadata.Index] = image;
				}
				catch
				{
					block1ImageCache[imageMetadata.Index] = new Bitmap(1, 1);
				}
			}
			block1ImageCache.Add(0, new Bitmap(1, 16));

			var block2ImageCache = new Dictionary<int, Image>();
			foreach (var imageMetadata in firmware.Block2Images)
			{
				try
				{
					var imageData = firmware.ReadImage(imageMetadata);
					var image = FirmwareImageProcessor.CreateImage(imageData);
					block2ImageCache[imageMetadata.Index] = image;
				}
				catch
				{
					block2ImageCache[imageMetadata.Index] = new Bitmap(1, 1);
				}
			}
			block2ImageCache.Add(0, new Bitmap(1, 16));

			var previousCache1 = m_block1ImageCache;
			var previousCache2 = m_block2ImageCache;

			m_block1ImageCache = block1ImageCache;
			m_block2ImageCache = block2ImageCache;

			foreach (var pair in previousCache1)
			{
				pair.Value.Dispose();
			}

			foreach (var pair in previousCache2)
			{
				pair.Value.Dispose();
			}
		}

		private void OpenDialogAndReadFirmwareOnOk(string firmwareName, Func<string, Firmware> readFirmwareDelegate)
		{
			string firmwareFile;
			using (var op = new OpenFileDialog { Title = string.Format("Select \"{0}\" firmware file ...", firmwareName), Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				firmwareFile = op.FileName;
			}

			ResetWorkspace();
			try
			{
				m_firmware = readFirmwareDelegate(firmwareFile);
				if (m_firmware == null)
				{
					throw new InvalidOperationException("No one definition is not appropriate for the selected firmware file.");
				}

				RebuildImageCache(m_firmware);
				FillListBox(Block2ImageListBox, m_firmware.Block2Images.Select(x => new ImagedListBoxItem<FirmwareImageMetadata>(x, m_block2ImageCache[x.Index], string.Format("0x{0:X2}", x.Index))), true);
				FillListBox(Block1ImageListBox, m_firmware.Block1Images.Select(x => new ImagedListBoxItem<FirmwareImageMetadata>(x, m_block1ImageCache[x.Index], string.Format("0x{0:X2}", x.Index))), true);
				FillListBox(Block2StringListBox, m_firmware.Block2Strings, false);
				FillListBox(Block1StringListBox, m_firmware.Block1Strings, false);

				SaveEncryptedMenuItem.Enabled = true;
				SaveDecryptedMenuItem.Enabled = true;
				EditMenuItem.Enabled = true;

				Text = string.Format("{0} - {1}", Consts.ApplicationTitle, firmwareFile);
				StatusLabel.Text = string.Format("Firmware loaded successfully. Used definition: {0}.", m_firmware.Definition.Name);
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to load firmware.\n{0}", ex.Message);
			}
		}

		private void OpenDialogAndSaveFirmwareOnOk(Action<string, Firmware> writeFirmwareDelegate)
		{
			if (m_firmware == null) return;

			string firmwareFile;
			using (var sf = new SaveFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				firmwareFile = sf.FileName;
			}

			try
			{
				writeFirmwareDelegate(firmwareFile, m_firmware);
				StatusLabel.Text = @"Firmware successfully saved to the file: " + firmwareFile;
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to save firmware.\n{0}", ex.Message);
			}
		}

		private void FillListBox(ListBox listBox, IEnumerable<object> items, bool selectFirstItem)
		{
			listBox.Items.Clear();
			listBox.BeginUpdate();
			foreach (var item in items)
			{
				listBox.Items.Add(item);
			}
			listBox.EndUpdate();

			if (selectFirstItem && listBox.Items.Count > 0)
			{
				listBox.SelectedIndex = 0;
			}
		}

		private bool[,] ProcessImage(Func<bool[,], bool[,]> imageDataProcessor, FirmwareImageMetadata imageMetadata)
		{
			var processedData = imageDataProcessor(ImagePixelGrid.Data);
			m_firmware.WriteImage(processedData, imageMetadata);

			var updateCache = new Action(() =>
			{
				var cachedImage = FirmwareImageProcessor.CreateImage(processedData);
				ImageCacheForImageListBox[imageMetadata.Index] = cachedImage;

				try
				{
					var item = ImageListBox.Items[imageMetadata.Index - 1] as ImagedListBoxItem<FirmwareImageMetadata>;
					if (item != null)
					{
						if(item.Image != null) item.Image.Dispose();

						item.Image = cachedImage;
						ImageListBox.Invoke(new Action(() =>
						{
							var itemRect = ImageListBox.GetItemRectangle(imageMetadata.Index - 1);
							ImageListBox.Invalidate(itemRect);
						}));
					}

					foreach (var icb in CharLayoutPanel.Controls.OfType<ImagedComboBox>())
					{
						icb.Items[imageMetadata.Index - 1].Image.Dispose();
						icb.Items[imageMetadata.Index - 1].Image = cachedImage;
						icb.Invalidate();
					}
				}
				catch
				{
					// Ignore
				}
			});
			updateCache.BeginInvoke(null, null);

			return processedData;
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_configurationManager.Save(m_configuration);
		}

		private void MainWindow_SizeChanged(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Maximized)
			{
				m_configuration.MainWindowMaximaged = true;
			}
			else if (WindowState == FormWindowState.Normal)
			{
				m_configuration.MainWindowMaximaged = false;
				m_configuration.MainWindowWidth = Width;
				m_configuration.MainWindowHeight = Height;
			}
		}

		private void BlockImageCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var currentListBoxSelectedIndices = ImageListBox.SelectedIndices.ToList();

			if (sender == Block1ImageCheckBox) Block2ImageCheckBox.Checked = !Block1ImageCheckBox.Checked;
			if (sender == Block2ImageCheckBox) Block1ImageCheckBox.Checked = !Block2ImageCheckBox.Checked;

			Block1ImageListBox.Visible = Block1ImageCheckBox.Checked;
			Block2ImageListBox.Visible = Block2ImageCheckBox.Checked;

			ImageListBox.Focus();
			if (currentListBoxSelectedIndices.Count != 0)
			{
				m_imageListBoxIsUpdating = true;
				ImageListBox.BeginUpdate();
				ImageListBox.SelectedIndices.Clear();
				ImageListBox.SelectedIndices.AddRange(currentListBoxSelectedIndices.Where(x => ImageListBox.Items.Count > x));
				ImageListBox.EndUpdate();

				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = LastSelectedImageMetadata != null
					? m_firmware.ReadImage(LastSelectedImageMetadata)
					: new bool[5, 5];

				m_imageListBoxIsUpdating = false;
			}
			else
			{
				ImageListBox_SelectedValueChanged(ImageListBox, EventArgs.Empty);
			}
		}

		private void BlockStringCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == Block1StringCheckBox) Block2StringCheckBox.Checked = !Block1StringCheckBox.Checked;
			if (sender == Block2StringCheckBox) Block1StringCheckBox.Checked = !Block2StringCheckBox.Checked;

			Block1StringListBox.Visible = Block1StringCheckBox.Checked;
			Block2StringListBox.Visible = Block2StringCheckBox.Checked;

			StringListBox.Focus();
		}

		private void ImageListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (m_imageListBoxIsUpdating || LastSelectedImageMetadata == null) return;

			StatusLabel.Text = string.Format("Image: {0}x{1}", LastSelectedImageMetadata.Width, LastSelectedImageMetadata.Height);
			try
			{
				ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = m_firmware.ReadImage(LastSelectedImageMetadata);
			}
			catch (Exception)
			{
				InfoBox.Show("Invalid image data. Possibly firmware definition is incompatible with loaded firmware.");
			}
		}

		private void StringListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (LastSelectedStringMetadata == null) return;

			try
			{
				var firmwareString = m_firmware.ReadString(LastSelectedStringMetadata);
				RemoveStringEditControls();
				CreateStringEditControls(firmwareString, ImageCacheForStringListBox);
				UpdateStringPreview();
			}
			catch
			{
				InfoBox.Show("Invalid string data.");
			}
		}

		private void RemoveStringEditControls()
		{
			foreach (var icb in CharLayoutPanel.Controls.OfType<ImagedComboBox>())
			{
				icb.SelectedValueChanged -= Icb_SelectedValueChanged;
			}
			CharLayoutPanel.Controls.Clear();
		}

		private void CreateStringEditControls(byte[] firmwareString, IDictionary<int, Image> imageCache)
		{
			var nullByteFound = false;
			for (var i = 0; i < firmwareString.Length; i++)
			{
				if (i == firmwareString.Length - 1 && firmwareString[i] == 0x00) continue;

				var stringChar = firmwareString[i];
				var nullItem = new ImagedComboBox.ImagedComboBoxItem((byte)0, imageCache[0], "NULL");
				var icb = new ImagedComboBox
				{
					Width = 200,
					ItemHeight = 30,
					BackColor = Color.Black,
					ForeColor = Color.White,
					Tag = new Tuple<FirmwareStringMetadata, int>(LastSelectedStringMetadata, i)
				};
				if (i > 0) icb.Items.Add(nullItem);
				var selectedItem = nullItem;
				foreach (var imageMetadata in CurrentImageBlockForStrings)
				{
					var item = new ImagedComboBox.ImagedComboBoxItem((byte)imageMetadata.Index, imageCache[imageMetadata.Index], string.Format("0x{0:X2}", imageMetadata.Index));
					icb.Items.Add(item);
					if (imageMetadata.Index == stringChar)
					{
						selectedItem = item;
					}
				}
				icb.SelectedItem = selectedItem;
				icb.SelectedValueChanged += Icb_SelectedValueChanged;
				icb.Enabled = !nullByteFound;
				CharLayoutPanel.Controls.Add(icb);
				nullByteFound = (byte)selectedItem.Value == 0x00;
			}
		}

		private void Icb_SelectedValueChanged(object sender, EventArgs e)
		{
			var icb = sender as ImagedComboBox;
			if (icb == null) return;

			var tag = icb.Tag as Tuple<FirmwareStringMetadata, int>;
			var item = icb.SelectedItem as ImagedComboBox.ImagedComboBoxItem;

			if (tag == null) return;
			if (item == null) return;

			var value = (byte)item.Value;
			var idx = CharLayoutPanel.Controls.IndexOf(icb);

			m_firmware.WriteChar(value, tag.Item2, tag.Item1);
			UpdateStringPreview();

			if (value == 0x00)
			{
				for (var i = idx + 1; i < CharLayoutPanel.Controls.Count; i++)
				{
					var relatedIcb = CharLayoutPanel.Controls[i] as ImagedComboBox;
					if (relatedIcb == null) continue;

					relatedIcb.SelectedIndex = 0;
					relatedIcb.Enabled = false;
				}
			}
			else if (idx + 1 < CharLayoutPanel.Controls.Count)
			{
				var relatedIcb = CharLayoutPanel.Controls[idx + 1] as ImagedComboBox;
				if (relatedIcb == null) return;

				if (relatedIcb.SelectedIndex == 0 && !relatedIcb.Enabled)
				{
					relatedIcb.Enabled = true;
				}
			}
		}

		private void UpdateStringPreview()
		{
			if (LastSelectedStringMetadata == null) return;

			var firmwareString = m_firmware.ReadString(LastSelectedStringMetadata);
			var charMetadatas = new List<FirmwareImageMetadata>();
			foreach (var charIndex in firmwareString)
			{
				var metadata = CurrentImageBlockForStrings.FirstOrDefault(x => x.Index == charIndex);
				if (metadata != null) charMetadatas.Add(metadata);
			}
			var images = m_firmware.ReadImages(charMetadatas).ToList();
			var data = FirmwareImageProcessor.MergeImages(images);

			StringPrewviewPixelGrid.Data = data;
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
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Clear, LastSelectedImageMetadata);
		}

		private void InvertButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Invert, LastSelectedImageMetadata);
		}

		private void FlipHorizontalButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.FlipHorizontal, LastSelectedImageMetadata);
		}

		private void FlipVerticalButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.FlipVertical, LastSelectedImageMetadata);
		}

		private void ShiftLeftButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftLeft, LastSelectedImageMetadata);
		}

		private void ShiftRightButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftRight, LastSelectedImageMetadata);
		}

		private void ShiftUpButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftUp, LastSelectedImageMetadata);
		}

		private void ShiftDownButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null || MainTabControl.SelectedTab != ImagesTabPage) return;
			ImagePixelGrid.Data = ImagePreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftDown, LastSelectedImageMetadata);
		}

		private void CopyButton_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0 || MainTabControl.SelectedTab != ImagesTabPage) return;

			var images = m_firmware.ReadImages(SelectedImageMetadata).ToList();
			m_clipboardManager.SetData(images);
		}

		private void PasteButton_Click(object sender, EventArgs e)
		{
			if (SelectedImageMetadata.Count == 0 || MainTabControl.SelectedTab != ImagesTabPage) return;

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

		private void OpenEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk("Select firmware file...", fileName => m_loader.TryLoadEncrypted(fileName, m_definitions));
		}

		private void OpenDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk("Select firmware file...", fileName => m_loader.TryLoadDecrypted(fileName, m_definitions));
		}

		private void SaveEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndSaveFirmwareOnOk((filePath, firmware) => m_loader.SaveEncrypted(filePath, firmware));
		}

		private void SaveDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndSaveFirmwareOnOk((filePath, firmware) => m_loader.SaveDecrypted(filePath, firmware));
		}

		private void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ClearAllPixelsMenuItem_Click(object sender, EventArgs e)
		{
			ClearAllPixelsButton_Click(null, null);
		}

		private void InvertMenuItem_Click(object sender, EventArgs e)
		{
			InvertButton_Click(null, null);
		}

		private void FlipHorizontalMenuItem_Click(object sender, EventArgs e)
		{
			FlipHorizontalButton_Click(null, null);
		}

		private void FlipVerticalMenuItem_Click(object sender, EventArgs e)
		{
			FlipVerticalButton_Click(null, null);
		}

		private void CopyMenuItem_Click(object sender, EventArgs e)
		{
			CopyButton_Click(null, null);
		}

		private void PasteMenuItem_Click(object sender, EventArgs e)
		{
			PasteButton_Click(null, null);
		}

		private void ShiftUpMenuItem_Click(object sender, EventArgs e)
		{
			ShiftUpButton_Click(null, null);
		}

		private void ShiftDownMenuItem_Click(object sender, EventArgs e)
		{
			ShiftDownButton_Click(null, null);
		}

		private void ShiftLeftMenuItem_Click(object sender, EventArgs e)
		{
			ShiftLeftButton_Click(null, null);
		}

		private void ShiftRightMenuItem_Click(object sender, EventArgs e)
		{
			ShiftRightButton_Click(null, null);
		}

		private void EncryptDecryptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var decryptionWindow = new DecryptionWindow())
			{
				decryptionWindow.ShowDialog();
			}
		}

		private void AboutMenuItem_Click(object sender, EventArgs e)
		{
			InfoBox.Show(Resources.AboutMessage, Consts.ApplicationVersion);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (!keyData.HasFlag(Keys.Control)) return base.ProcessCmdKey(ref msg, keyData);

			if (keyData.HasFlag(Keys.Shift) && keyData.HasFlag(Keys.S))
			{
				SaveDecryptedMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.S))
			{
				SaveEncryptedMenuItem.PerformClick();
				return true;
			}

			if (keyData.HasFlag(Keys.N))
			{
				ClearAllPixelsMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.I))
			{
				InvertMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.C))
			{
				CopyMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.V))
			{
				PasteMenuItem.PerformClick();
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
				ShiftUpMenuItem.PerformClick();
				return true;
			}
			if (key == Keys.Down)
			{
				ShiftDownMenuItem.PerformClick();
				return true;
			}
			if (key == Keys.Left)
			{
				ShiftLeftMenuItem.PerformClick();
				return true;
			}
			if (key == Keys.Right)
			{
				ShiftRightMenuItem.PerformClick();
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void CopyContextMenuItem_Click(object sender, EventArgs e)
		{
			CopyButton_Click(null, null);
		}

		private void PasteContextMenuItem_Click(object sender, EventArgs e)
		{
			PasteButton_Click(null, null);
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

			var lastSelectedItem = LastSelectedImageMetadata;
			ImageListBox.SelectedIndices.Clear();
			ImageListBox.SelectedItem = lastSelectedItem;
		}
	}
}
