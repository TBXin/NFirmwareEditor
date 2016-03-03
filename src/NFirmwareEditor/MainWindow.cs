using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Properties;

namespace NFirmwareEditor
{
	public partial class MainWindow : Form
	{
		private readonly ConfigurationManager m_configurationManager = new ConfigurationManager();
		private readonly FirmwareDefinitionManager m_firmwareDefinitionManager = new FirmwareDefinitionManager();
		private readonly FirmwareLoader m_loader = new FirmwareLoader(new FirmwareEncoder());
		private readonly ClipboardManager m_clipboardManager = new ClipboardManager();

		private Configuration m_configuration;
		private Firmware m_firmware;

		public MainWindow()
		{
			InitializeComponent();
			InitializeControls();

			Icon = Paths.ApplicationIcon;
			LoadSettings();
		}

		[NotNull]
		public ListBox ImagesListBox
		{
			get { return Block1ImagesListBox.Visible ? Block1ImagesListBox : Block2ImagesListBox; }
		}

		[CanBeNull]
		public FirmwareImageMetadata LastSelectedImageMetadata
		{
			get
			{
				return ImagesListBox.Items.Count == 0 || ImagesListBox.SelectedIndices.Count == 0
					? null
					: ImagesListBox.Items[ImagesListBox.SelectedIndices[ImagesListBox.SelectedIndices.Count - 1]] as FirmwareImageMetadata;
			}
		}

		[NotNull]
		public List<FirmwareImageMetadata> SelectedImageMetadata
		{
			get
			{
				if (ImagesListBox.SelectedIndices.Count == 0) return new List<FirmwareImageMetadata>();

				var result = new List<FirmwareImageMetadata>();
				foreach (int selectedIndex in ImagesListBox.SelectedIndices)
				{
					var metadata = ImagesListBox.Items[selectedIndex] as FirmwareImageMetadata;
					if (metadata == null) continue;

					result.Add(metadata);
				}
				return result;
			}
		}

		[CanBeNull]
		public FirmwareDefinition SelectedDefinition
		{
			get { return DefinitionsComboBox.SelectedItem as FirmwareDefinition; }
		}

		private void InitializeControls()
		{
			PreviewPixelGrid.BlockInnerBorderPen = Pens.Transparent;
			PreviewPixelGrid.BlockOuterBorderPen = Pens.Transparent;
			PreviewPixelGrid.ActiveBlockBrush = Brushes.White;
			PreviewPixelGrid.InactiveBlockBrush = Brushes.Black;
		}

		private void ResetWorkspace()
		{
			Block1ImagesListBox.Items.Clear();
			Block2ImagesListBox.Items.Clear();
			ImagePixelGrid.Data = new bool[5, 5];
			StatusLabel.Text = null;
		}

		private void LoadSettings()
		{
			m_configuration = m_configurationManager.Load();
			WindowState = m_configuration.MainWindowMaximaged ? FormWindowState.Maximized : FormWindowState.Normal;
			Width = m_configuration.MainWindowWidth;
			Height = m_configuration.MainWindowHeight;

			var definitions = m_firmwareDefinitionManager.Load();
			foreach (var definition in definitions)
			{
				DefinitionsComboBox.Items.Add(definition);
			}
			if (definitions.Count > 0)
			{
				var savedDefinition = definitions.FirstOrDefault(x => x.Name.Equals(m_configuration.LastUsedDefinition));
				DefinitionsComboBox.SelectedItem = savedDefinition ?? definitions[0];
			}

			GridSizeUpDown.Value = m_configuration.GridSize;
			ShowGridCheckBox.Checked = m_configuration.ShowGid;
		}

		private void OpenDialogAndReadFirmwareOnOk(Func<string, FirmwareDefinition, Firmware> readFirmwareDelegate)
		{
			if (SelectedDefinition == null)
			{
				InfoBox.Show("Select firmware definition first.");
				return;
			}

			string firmwareFile;
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				firmwareFile = op.FileName;
			}

			ResetWorkspace();
			try
			{
				m_firmware = readFirmwareDelegate(firmwareFile, SelectedDefinition);

				FillImagesListBox(Block2ImagesListBox, m_firmware.Block2Images, true);
				FillImagesListBox(Block1ImagesListBox, m_firmware.Block1Images, true);

				SaveEncryptedMenuItem.Enabled = true;
				SaveDecryptedMenuItem.Enabled = true;
				EditMenuItem.Enabled = true;

				Text = string.Format("{0} - {1}", Consts.ApplicationTitle, firmwareFile);
				StatusLabel.Text = @"Firmware loaded successfully.";
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

		private void FillImagesListBox(ListBox listBox, IEnumerable<object> items, bool selectFirstItem)
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

		private void DefinitionsComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			var definition = DefinitionsComboBox.SelectedItem as FirmwareDefinition;
			if (definition == null) return;

			m_configuration.LastUsedDefinition = definition.Name;
		}

		private void BlockCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var currentListBoxSelectedIndices = ImagesListBox.SelectedIndices.ToList();

			if (sender == Block1CheckBox) Block2CheckBox.Checked = !Block1CheckBox.Checked;
			if (sender == Block2CheckBox) Block1CheckBox.Checked = !Block2CheckBox.Checked;

			Block1ImagesListBox.Visible = Block1CheckBox.Checked;
			Block2ImagesListBox.Visible = Block2CheckBox.Checked;

			ImagesListBox.Focus();
			if (currentListBoxSelectedIndices.Count != 0)
			{
				ImagesListBox.SelectedIndices.Clear();
				ImagesListBox.SelectedIndices.AddRange(currentListBoxSelectedIndices.Where(x => ImagesListBox.Items.Count > x));
				if (ImagesListBox.SelectedIndices.Count == 0)
				{
					ImagePixelGrid.Data = PreviewPixelGrid.Data = new bool[5, 5];
				}
			}
			else
			{
				BlockImagesListBox_SelectedValueChanged(ImagesListBox, EventArgs.Empty);
			}
		}

		private void BlockImagesListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			StatusLabel.Text = string.Format("Image: {0}x{1}", LastSelectedImageMetadata.Width, LastSelectedImageMetadata.Height);
			try
			{
				ImagePixelGrid.Data = PreviewPixelGrid.Data = m_firmware.ReadImage(LastSelectedImageMetadata);
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

		private void ImagePixelGrid_DataUpdated(bool[,] data)
		{
			if (LastSelectedImageMetadata == null) return;

			m_firmware.WriteImage(data, LastSelectedImageMetadata);
			PreviewPixelGrid.Data = data;
		}

		private void ClearAllPixelsButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Clear, LastSelectedImageMetadata);
		}

		private void InvertButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.Invert, LastSelectedImageMetadata);
		}

		private void ShiftLeftButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftLeft, LastSelectedImageMetadata);
		}

		private void ShiftRightButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftRight, LastSelectedImageMetadata);
		}

		private void ShiftUpButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftUp, LastSelectedImageMetadata);
		}

		private void ShiftDownButton_Click(object sender, EventArgs e)
		{
			if (LastSelectedImageMetadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(FirmwareImageProcessor.ShiftDown, LastSelectedImageMetadata);
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
				ImagePixelGrid.Data = PreviewPixelGrid.Data = ProcessImage(data => FirmwareImageProcessor.PasteImage(data, copiedImages[0]), LastSelectedImageMetadata);
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
				ImagePixelGrid.Data = PreviewPixelGrid.Data = updatedImage;
			}
		}

		private void OpenEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk((fileName, definition) => m_loader.LoadEncrypted(fileName, definition));
		}

		private void OpenDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk((fileName, definition) => m_loader.LoadDecrypted(fileName, definition));
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

			if (keyData.HasFlag(Keys.O))
			{
				OpenEncryptedMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.E))
			{
				OpenDecryptedMenuItem.PerformClick();
				return true;
			}
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
			ImagesListBox.SelectedIndices.Clear();
			ImagesListBox.SelectedItem = lastSelectedItem;
		}

		private void ImagePixelGrid_CursorPositionChanged(Point location)
		{
			CursorPositionLabel.Text = string.Format("X: {0}, Y:{1}", location.X + 1, location.Y + 1);
		}
	}
}
