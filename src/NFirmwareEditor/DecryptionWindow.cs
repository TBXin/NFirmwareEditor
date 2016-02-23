using System;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Firmware;

namespace NFirmwareEditor
{
	public partial class DecryptionWindow : Form
	{
		public DecryptionWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
		}

		private void SelectSourceButton_Click(object sender, System.EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = @"Firmware file|*.bin" })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				SourceEncryptedTextBox.Text = op.FileName;
				SourceDecryptedTextBox.Text = null;
			}
		}

		private void SelectDecryptedSourceButton_Click(object sender, EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = @"Firmware file|*.bin" })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				SourceEncryptedTextBox.Text = null;
				SourceDecryptedTextBox.Text = op.FileName;
			}
		}

		private void SelectDestinationButton_Click(object sender, System.EventArgs e)
		{
			using (var sf = new SaveFileDialog { Filter = @"Firmware file|*.bin" })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				DestinationTextBox.Text = sf.FileName;
			}
		}

		private void EncryptDecryptButton_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(SourceEncryptedTextBox.Text) && string.IsNullOrEmpty(SourceDecryptedTextBox.Text))
			{
				InfoBox.Show("Specify source file first.");
				return;
			}
			if (string.IsNullOrEmpty(DestinationTextBox.Text))
			{
				InfoBox.Show("Specify destination file first.");
				return;
			}

			try
			{
				var source = string.Empty;
				var sourceEncrypted = false;

				if (!string.IsNullOrEmpty(SourceEncryptedTextBox.Text))
				{
					source = SourceEncryptedTextBox.Text;
					sourceEncrypted = true;
				}
				if (!string.IsNullOrEmpty(SourceDecryptedTextBox.Text))
				{
					source = SourceDecryptedTextBox.Text;
					sourceEncrypted = false;
				}

				var firmware = FirmwareEncoder.ReadFile(source, sourceEncrypted);
				FirmwareEncoder.WriteFile(DestinationTextBox.Text, firmware, !sourceEncrypted);
				InfoBox.Show(string.Format("Firmware successfully {0}!", sourceEncrypted ? "decrypted" : "encrypted"));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during firmware encryption / decryption.\n" + ex.Message);
			}
		}
	}
}
