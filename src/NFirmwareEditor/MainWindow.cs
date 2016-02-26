using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Firmware;

namespace NFirmwareEditor
{
	public partial class MainWindow : Form
	{
		private const string Title = "NFirmwareEditor";
		private byte[] m_firmware;
		private bool[,] m_buffer;

		public MainWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			InititalizeControls();
			var definitions = FirmwareDefinitionManager.Load();
			foreach (var definition in definitions)
			{
				DefinitionsComboBox.Items.Add(definition);
			}
			if (definitions.Count > 0)
			{
				DefinitionsComboBox.SelectedItem = definitions[0];
			}
		}

		private void OpenEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk(fileName => FirmwareEncoder.ReadFile(fileName));
		}

		private void OpenDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDialogAndReadFirmwareOnOk(fileName => FirmwareEncoder.ReadFile(fileName, false));
		}

		private void SaveEncryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDiaglogAndSaveFirmwareOnOk((filePath, data) => FirmwareEncoder.WriteFile(filePath, data));
		}

		private void SaveDecryptedMenuItem_Click(object sender, EventArgs e)
		{
			OpenDiaglogAndSaveFirmwareOnOk((filePath, data) => FirmwareEncoder.WriteFile(filePath, data, false));
		}

		private void OpenDialogAndReadFirmwareOnOk(Func<string, byte[]> readFirmwareDelegate)
		{
			string firmwareFile;
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				firmwareFile = op.FileName;
			}

			InititalizeControls();
			LoadFirmware(readFirmwareDelegate, firmwareFile);
			EnumerateFirmwareImages();
		}

		private void OpenDiaglogAndSaveFirmwareOnOk(Action<string, byte[]> writeFirmwareDelegate)
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
				StatusLabel.Text = @"Firmware saved successfully to: " + firmwareFile;
			}
			catch (Exception ex)
			{
				InfoBox.Show("Can't save firmware file.\n" + ex.Message);
			}
		}

		private void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void BlockImagesListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			var metadata = ((ListBox)sender).SelectedItem as ImageMetadata;
			if (metadata == null) return;

			StatusLabel.Text = string.Format("Image: {0}x{1}", metadata.Width, metadata.Height);
			try
			{
				ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.ReadImage(m_firmware, metadata);
			}
			catch (Exception)
			{
				InfoBox.Show("Invalid image data. Possibly firmware definition is incompatible with loaded firmware.");
			}
		}

		private void GridSizeUpDown_ValueChanged(object sender, EventArgs e)
		{
			ImagePixelGrid.BlockSize = (int)GridSizeUpDown.Value;
		}

		private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ImagePixelGrid.ShowGrid = ShowGridCheckBox.Checked;
		}

		private void InititalizeControls()
		{
			Block1ImagesListBox.Items.Clear();
			ImagePixelGrid.Data = new bool[5, 5];
			StatusLabel.Text = null;

			PreviewPixelGrid.BlockInnerBorderPen = Pens.Transparent;
			PreviewPixelGrid.BlockOuterBorderPen = Pens.Transparent;
			PreviewPixelGrid.ActiveBlockBrush = Brushes.White;
			PreviewPixelGrid.InactiveBlockBrush = Brushes.Black;
		}

		private void LoadFirmware(Func<string, byte[]> readFirmwareDelegate, string firmwareFile)
		{
			try
			{
				m_firmware = readFirmwareDelegate(firmwareFile);

				SaveEncryptedMenuItem.Enabled = true;
				SaveDecryptedMenuItem.Enabled = true;
				EditMenuItem.Enabled = true;
				Text = Title + @" - " + firmwareFile;
				StatusLabel.Text = @"Firmware loaded successfully.";
			}
			catch (Exception ex)
			{
				InfoBox.Show("Can't open file.\n" + ex.Message);
			}
		}

		private void EnumerateFirmwareImages()
		{
			var definition = DefinitionsComboBox.SelectedItem as FirmwareDefinition;
			if (definition == null)
			{
				InfoBox.Show("Select definition first.");
				return;
			}

			try
			{
				var images = FirmwareImageProcessor.EnumerateImages(m_firmware, definition);
				FillImagesListBox(Block2ImagesListBox, images.Block2Images, true);
				FillImagesListBox(Block1ImagesListBox, images.Block1Images, true);
			}
			catch (Exception)
			{
				InfoBox.Show("Unable to enumerate images. Possibly firmware definition is incompatible with loaded firmware.");
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

		private void ImagePixelGrid_DataUpdated(bool[,] data)
		{
			var metadata = (Block1CheckBox.Checked ? Block1ImagesListBox : Block2ImagesListBox).SelectedItem as ImageMetadata;
			if (metadata == null) return;

			FirmwareImageProcessor.WriteImage(m_firmware, data, metadata);
			PreviewPixelGrid.Data = data;
		}

		private void ClearAllPixelsButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.ClearAllPixelsImage(m_firmware, ImagePixelGrid.Data, metadata);
		}

		private void InvertButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.InvertImage(m_firmware, ImagePixelGrid.Data, metadata);
		}

		private void CopyButton_Click(object sender, EventArgs e)
		{
			m_buffer = ImagePixelGrid.Data;
		}

		private void PasteButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null || m_buffer == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.PasteImage(m_firmware, ImagePixelGrid.Data, m_buffer, metadata);
		}

		private void ShiftLeftButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.ShiftImageLeft(m_firmware, ImagePixelGrid.Data, metadata);
		}

		private void ShiftRightButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.ShiftImageRight(m_firmware, ImagePixelGrid.Data, metadata);
		}

		private void ShiftUpButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.ShiftImageUp(m_firmware, ImagePixelGrid.Data, metadata);
		}

		private void ShiftDownButton_Click(object sender, EventArgs e)
		{
			var metadata = Block1ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			ImagePixelGrid.Data = PreviewPixelGrid.Data = FirmwareImageProcessor.ShiftImageDown(m_firmware, ImagePixelGrid.Data, metadata);
		}

		private void encryptDecryptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var decryptionWindow = new DecryptionWindow())
			{
				decryptionWindow.ShowDialog();
			}
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

		private void AboutMenuItem_Click(object sender, EventArgs e)
		{
			InfoBox.Show("NFirmwareEditor v1.0\n\nFirmware resource editor for vape devices such as:\nEvic VTC Mini, Cuboid, RX200, PresaTC75W and so on...\n\nReikoKitsune © 2016");
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
				clearAllPixelsToolStripMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.I))
			{
				invertToolStripMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.C))
			{
				copyToolStripMenuItem.PerformClick();
				return true;
			}
			if (keyData.HasFlag(Keys.V))
			{
				pasteToolStripMenuItem.PerformClick();
				return true;
			}

			var key = keyData &= ~Keys.Control;
			if (key == Keys.Up)
			{
				shiftUpToolStripMenuItem.PerformClick();
				return true;
			}
			if (key == Keys.Down)
			{
				shiftDownToolStripMenuItem.PerformClick();
				return true;
			}
			if (key == Keys.Left)
			{
				shiftLeftToolStripMenuItem.PerformClick();
				return true;
			}
			if (key == Keys.Right)
			{
				shiftRightToolStripMenuItem.PerformClick();
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void BlockCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == Block1CheckBox) Block2CheckBox.Checked = !Block1CheckBox.Checked;
			if (sender == Block2CheckBox) Block1CheckBox.Checked = !Block2CheckBox.Checked;

			Block1ImagesListBox.Visible = Block1CheckBox.Checked;
			Block2ImagesListBox.Visible = Block2CheckBox.Checked;

			if (Block1ImagesListBox.Visible)
			{
				Block1ImagesListBox.Focus();
				BlockImagesListBox_SelectedValueChanged(Block1ImagesListBox, EventArgs.Empty);
			}

			if (Block2ImagesListBox.Visible)
			{
				Block2ImagesListBox.Focus();
				BlockImagesListBox_SelectedValueChanged(Block2ImagesListBox, EventArgs.Empty);
			}
		}
	}
}
