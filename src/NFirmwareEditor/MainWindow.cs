using System;
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

		public MainWindow()
		{
			InitializeComponent();
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
			using (var op = new OpenFileDialog { Filter = @"Firmware file|*.bin" })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				firmwareFile = op.FileName;
			}

			InititalizeControls();
			LoadFirmware(readFirmwareDelegate, firmwareFile);
			ScanFirmware();
		}

		private void OpenDiaglogAndSaveFirmwareOnOk(Action<string, byte[]> writeFirmwareDelegate)
		{
			if (m_firmware == null) return;

			string firmwareFile;
			using (var sf = new SaveFileDialog { Filter = @"Firmware file|*.bin" })
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

		private void ImagesListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			var metadata = ImagesListBox.SelectedItem as ImageMetadata;
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
			ImagesListBox.Items.Clear();
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
				Text = Title + @" - " + firmwareFile;
				StatusLabel.Text = @"Firmware loaded successfully.";
			}
			catch (Exception ex)
			{
				InfoBox.Show("Can't open file.\n" + ex.Message);
			}
		}

		private void ScanFirmware()
		{
			var definition = DefinitionsComboBox.SelectedItem as FirmwareDefinition;
			if (definition == null)
			{
				InfoBox.Show("Select definition first.");
				return;
			}

			try
			{
				var images = FirmwareImageProcessor.EnumerateImages(m_firmware, definition.ImageTable.OffsetFrom, definition.ImageTable.OffsetTo);
				ImagesListBox.BeginUpdate();
				ImagesListBox.Items.Clear();
				foreach (var imageData in images)
				{
					ImagesListBox.Items.Add(imageData);
				}
				ImagesListBox.EndUpdate();
				if (ImagesListBox.Items.Count > 0) ImagesListBox.SelectedIndex = 0;
			}
			catch (Exception)
			{
				InfoBox.Show("Unable to enumerate images. Possibly firmware definition is incompatible with loaded firmware.");
			}
		}

		private void ImagePixelGrid_DataUpdated(bool[,] data)
		{
			var metadata = ImagesListBox.SelectedItem as ImageMetadata;
			if (metadata == null) return;

			FirmwareImageProcessor.WriteImage(m_firmware, data, metadata);
			PreviewPixelGrid.Data = FirmwareImageProcessor.ReadImage(m_firmware, metadata);
		}
	}
}
