using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;

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
			OkButton.Click += OkButton_Click;
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

		private Patch GetPatch()
		{
			return new Patch
			{
				Name = NameTextBox.Text,
				Version = VersionTextBox.Text,
				Author = AuthorTextBox.Text,
				Definition = DefinitionComboBox.Text,
				Description = DescriptionTextBox.Text,
				DataString = Environment.NewLine + DataTextBox.Text
			};
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
			OkButton.Enabled = true;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			var patch = GetPatch();
			using (var sf = new SaveFileDialog { Filter = Consts.PatchFilter, FileName = patch.Name.Nvl("new_patch") + ".patch"})
			{
				if (sf.ShowDialog() != DialogResult.OK) return;

				try
				{
					m_patchManager.SavePatch(sf.FileName, patch);
					DialogResult = DialogResult.OK;
				}
				catch (Exception ex)
				{
					InfoBox.Show("An error occured during saving patch\n" + ex.Message);
				}
			}
		}
	}
}
