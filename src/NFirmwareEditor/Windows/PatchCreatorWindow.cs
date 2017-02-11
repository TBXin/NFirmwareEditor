using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows
{
	internal partial class PatchCreatorWindow : EditorDialogWindow
	{
		private readonly FirmwareLoader m_loader;
		private readonly PatchManager m_patchManager;
		private readonly IEnumerable<FirmwareDefinition> m_firmwareDefinitions;

		private bool m_isFirstDiffCreated;

		public PatchCreatorWindow()
		{
			InitializeComponent();

			SelectSource1Button.Click += SelectSourceButton_Click;
			SelectSource2Button.Click += SelectSourceButton_Click;
			CreateDiffLink.LinkClicked += CreateDiffLink_LinkClicked;
			OkButton.Click += OkButton_Click;
		}

		public PatchCreatorWindow
		(
			[NotNull] FirmwareLoader firmwareLoader, 
			[NotNull] PatchManager patchManager, 
			[NotNull] IEnumerable<FirmwareDefinition> firmwareDefinitions
		) : this()
		{
			if (firmwareLoader == null) throw new ArgumentNullException("firmwareLoader");
			if (patchManager == null) throw new ArgumentNullException("patchManager");
			if (firmwareDefinitions == null) throw new ArgumentNullException("firmwareDefinitions");

			m_loader = firmwareLoader;
			m_patchManager = patchManager;
			m_firmwareDefinitions = firmwareDefinitions;
			AuthorTextBox.Text = Environment.UserName;
			foreach (var firmwareDefinition in m_firmwareDefinitions)
			{
				DefinitionComboBox.Items.Add(firmwareDefinition.Name);
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

			var file1 = m_loader.LoadFile(Source1TextBox.Text);
			var file2 = m_loader.LoadFile(Source2TextBox.Text);

			var definition = m_loader.DetermineDefinition(file1, m_firmwareDefinitions);
			if (definition != null) DefinitionComboBox.SelectedItem = definition.Name;

			DataTextBox.Text = m_patchManager.CreateDiff(file1, file2);
			OkButton.Enabled = true;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			var patch = GetPatch();
			using (var sf = new SaveFileDialog { Filter = Consts.PatchFilter, FileName = patch.Name.Nvl("new_patch") + Consts.PatchFileExtensionWoAsterisk})
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
