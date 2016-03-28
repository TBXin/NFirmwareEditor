using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class PatchCreatorWindow : Form
	{
		private readonly PatchManager m_patchManager;
		private readonly FirmwareEncoder m_encoder = new FirmwareEncoder();

		private bool m_isFirstDiffCreated;

		public PatchCreatorWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			SelectSource1Button.Click += SelectSourceButton_Click;
			SelectSource2Button.Click += SelectSourceButton_Click;
			CreateDiffLink.LinkClicked += CreateDiffLink_LinkClicked;
		}

		public PatchCreatorWindow([NotNull] PatchManager patchManager, [NotNull] IEnumerable<string> firmwareDefinitions) : this()
		{
			if (patchManager == null) throw new ArgumentNullException("patchManager");
			if (firmwareDefinitions == null) throw new ArgumentNullException("firmwareDefinitions");

			m_patchManager = patchManager;
			foreach (var firmwareDefinition in firmwareDefinitions)
			{
				DefinitionComboBox.Items.Add(firmwareDefinition);
			}
		}

		private void SelectSourceButton_Click(object sender, EventArgs e)
		{
			using (var op = new OpenFileDialog { Filter = Consts.FirmwareFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;

				if (sender == SelectSource1Button)
				{
					Source1TextBox.Text = op.FileName;
					Source1TextBox.ScrollToEnd();
				}
				if (sender == SelectSource2Button)
				{
					Source2TextBox.Text = op.FileName;
					Source2TextBox.ScrollToEnd();
				}
			}

			CreateDiffLink.Enabled = !string.IsNullOrEmpty(Source1TextBox.Text) && !string.IsNullOrEmpty(Source2TextBox.Text);
			if (m_isFirstDiffCreated || !CreateDiffLink.Enabled) return;

			CreateDiffLink_LinkClicked(null, null);
			m_isFirstDiffCreated = true;
		}

		private void CreateDiffLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(Source1TextBox.Text) || string.IsNullOrEmpty(Source2TextBox.Text)) return;

			var file1 = m_encoder.ReadFile(Source1TextBox.Text, DecryptSourceButton.Checked);
			var file2 = m_encoder.ReadFile(Source2TextBox.Text, DecryptSourceButton.Checked);

			DataTextBox.Text = m_patchManager.CompareFiles(file1, file2);
		}
	}
}
