using System;
using System.IO;
using System.Windows.Forms;
using NCore;
using NCore.UI;
using NFirmware;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	internal partial class DecryptionWindow : EditorDialogWindow
	{
		private readonly JoyetechEncryption m_encoder;
		private readonly FirmwareLoader m_loader;

		private string m_sourceFileName;
		private bool m_sourceEncrypted;
		private byte[] m_source;

		public DecryptionWindow()
		{
			InitializeComponent();

			m_encoder = new JoyetechEncryption();
			m_loader = new FirmwareLoader();

			SelectSourceButton.Click += SelectSourceButton_Click;
			SourceTextBox.TextChanged += (s, e) =>
			{
				SelectDestinationButton.Enabled = !string.IsNullOrEmpty(SourceTextBox.Text);
			};
			SelectDestinationButton.Click += SelectDestinationButton_Click;
			DestinationTextBox.TextChanged += (s, e) =>
			{
				EncryptDecryptButton.Enabled = !string.IsNullOrEmpty(DestinationTextBox.Text);
			};

			EncryptDecryptButton.Click += EncryptDecryptButton_Click;
		}

		private void SelectSourceButton_Click(object sender, EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				m_sourceFileName = op.FileName;
				m_source = File.ReadAllBytes(m_sourceFileName);
				m_sourceEncrypted = m_loader.IsFirmwareEncrypted(m_source);

				SourceGroupBox.Text = string.Format("Source: ({0})", m_sourceEncrypted ? Consts.Encrypted : Consts.Decrypted);
				DestinationGroupBox.Text = string.Format("Destination: ({0})", !m_sourceEncrypted ? Consts.Encrypted : Consts.Decrypted);
				DestinationTextBox.Text = string.Empty;
				EncryptDecryptButton.Text = !m_sourceEncrypted ? "Encrypt" : "Decrypt";
				SourceTextBox.Text = m_sourceFileName;
				SourceTextBox.ScrollToEnd();
				SelectDestinationButton.Enabled = true;
			}
		}

		private void SelectDestinationButton_Click(object sender, EventArgs e)
		{
			using (var sf = new SaveFileDialog { Filter = Consts.FirmwareFilter, FileName = GetDestinationFileName() })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;

				DestinationTextBox.Text = sf.FileName;
				DestinationTextBox.ScrollToEnd();
			}
		}

		private void EncryptDecryptButton_Click(object sender, EventArgs e)
		{
			try
			{
				File.WriteAllBytes(DestinationTextBox.Text, m_encoder.Encode(m_source));
				InfoBox.Show(string.Format("Firmware successfully {0}!", m_sourceEncrypted ? Consts.Decrypted : Consts.Encrypted));
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occurred during firmware encryption / decryption.\n" + ex.Message);
			}
			finally
			{
				SourceTextBox.Text = DestinationTextBox.Text = string.Empty;
			}
		}

		private string GetDestinationFileName()
		{
			if (string.IsNullOrEmpty(m_sourceFileName)) return m_sourceFileName;

			var result = Path.GetFileNameWithoutExtension(m_sourceFileName)
				.Replace("_" + Consts.Encrypted, string.Empty)
				.Replace("_" + Consts.Decrypted, string.Empty);

			return result + "_" + (m_sourceEncrypted ? Consts.Decrypted : Consts.Encrypted);
		}
	}
}
