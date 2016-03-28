using System.Collections.Generic;
using System.Windows.Forms;
using NFirmware;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	public partial class PatchCreatorWindow : Form
	{
		private readonly FirmwareEncoder m_encoder = new FirmwareEncoder();

		public PatchCreatorWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			SelectSource1Button.Click += SelectSourceButton_Click;
			SelectSource2Button.Click += SelectSourceButton_Click;
		}

		public PatchCreatorWindow(IEnumerable<string> firmwareDefinitions) : this()
		{
			foreach (var firmwareDefinition in firmwareDefinitions)
			{
				DefinitionComboBox.Items.Add(firmwareDefinition);
			}
		}

		private void SelectSourceButton_Click(object sender, System.EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				if (sender == SelectSource1Button) Source1TextBox.Text = op.FileName;
				if (sender == SelectSource2Button) Source2TextBox.Text = op.FileName;
			}

			if (string.IsNullOrEmpty(Source1TextBox.Text) || string.IsNullOrEmpty(Source2TextBox.Text)) return;

			var file1 = m_encoder.ReadFile(Source1TextBox.Text, DecryptSourceButton.Checked);
			var file2 = m_encoder.ReadFile(Source2TextBox.Text, DecryptSourceButton.Checked);
		}
	}
}
