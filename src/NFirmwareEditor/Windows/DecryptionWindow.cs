using System;
using System.IO;
using System.Windows.Forms;
using NFirmware;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	public partial class DecryptionWindow : Form
	{
		private readonly FirmwareEncoder m_encoder = new FirmwareEncoder();
		private bool m_sourceEncrypted;
		private string m_source = string.Empty;

		public DecryptionWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
		}

		private void SelectEncryptedSourceButton_Click(object sender, EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				m_source = op.FileName;
				m_sourceEncrypted = true;

				SourceEncryptedTextBox.Text = m_source;
				SourceDecryptedTextBox.Text = null;
				SelectDestinationButton.Enabled = true;
			}
		}

		private void SelectDecryptedSourceButton_Click(object sender, EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				m_source = op.FileName;
				m_sourceEncrypted = false;

				SourceEncryptedTextBox.Text = null;
				SourceDecryptedTextBox.Text = m_source;
				SelectDestinationButton.Enabled = true;
			}
		}

		private void SelectDestinationButton_Click(object sender, EventArgs e)
		{
			using (var sf = new SaveFileDialog { Filter = Consts.FirmwareFilter, FileName = GetDestinationFileName() })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				DestinationTextBox.Text = sf.FileName;
			}
		}

		private void EncryptDecryptButton_Click(object sender, EventArgs e)
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

				if (!string.IsNullOrEmpty(SourceEncryptedTextBox.Text))
				{
					source = SourceEncryptedTextBox.Text;
				}
				if (!string.IsNullOrEmpty(SourceDecryptedTextBox.Text))
				{
					source = SourceDecryptedTextBox.Text;
				}

				var firmware = m_encoder.ReadFile(source, m_sourceEncrypted);
				m_encoder.WriteFile(DestinationTextBox.Text, firmware, !m_sourceEncrypted);
				InfoBox.Show(string.Format("Firmware successfully {0}!", m_sourceEncrypted ? Consts.Decrypted : Consts.Encrypted));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during firmware encryption / decryption.\n" + ex.Message);
			}
		}

		private string GetDestinationFileName()
		{
			if (string.IsNullOrEmpty(m_source)) return m_source;

			var result = Path.GetFileNameWithoutExtension(m_source)
				.Replace("_" + Consts.Encrypted, string.Empty)
				.Replace("_" + Consts.Decrypted, string.Empty);

			return result + "_" + (m_sourceEncrypted ? Consts.Decrypted : Consts.Encrypted);
		}
	}
}
