using System;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class OptionsWindow : EditorDialogWindow
	{
		private readonly ApplicationConfiguration m_configuration;

		public OptionsWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			ImageEditorModeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<ImageEditorMouseMode>("LMB sets the pixel, RMB resets", ImageEditorMouseMode.LeftSetRightUnset),
				new NamedItemContainer<ImageEditorMouseMode>("LMB sets or resets the pixel", ImageEditorMouseMode.LeftSetUnset)
			});

			BackupModeComboBox.Items.AddRange(new object[]
			{
				new NamedItemContainer<BackupCreationMode>("Disabled (do not create backups)", BackupCreationMode.Disabled),
				new NamedItemContainer<BackupCreationMode>("Simple (overwrites backup file on each save)", BackupCreationMode.Simple),
				new NamedItemContainer<BackupCreationMode>("Extended (stores backups with unique names in the separate directory)", BackupCreationMode.Extended)
			});

			OkButton.Click += OkButton_Click;
		}

		public OptionsWindow([NotNull] ApplicationConfiguration configuration) : this()
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			m_configuration = configuration;

			LoadSettings();
		}

		private void LoadSettings()
		{
			foreach (var item in ImageEditorModeComboBox.Items.Cast<NamedItemContainer<ImageEditorMouseMode>>())
			{
				if (item.Data != m_configuration.ImageEditorMouseMode) continue;

				ImageEditorModeComboBox.SelectedItem = item;
				break;
			}
			foreach (var item in BackupModeComboBox.Items.Cast<NamedItemContainer<BackupCreationMode>>())
			{
				if (item.Data != m_configuration.BackupCreationMode) continue;

				BackupModeComboBox.SelectedItem = item;
				break;
			}
			CheckForApplicationUpdatesCheckBox.Checked = m_configuration.CheckForApplicationUpdates;
			CheckForPatchesUpdatesCheckBox.Checked = m_configuration.CheckForPatchesUpdates;
		}

		private void SaveSettings()
		{
			var editorModeItem = ImageEditorModeComboBox.SelectedItem as NamedItemContainer<ImageEditorMouseMode>;
			if (editorModeItem != null)
			{
				m_configuration.ImageEditorMouseMode = editorModeItem.Data;
			}
			var backupModeItem = BackupModeComboBox.SelectedItem as NamedItemContainer<BackupCreationMode>;
			if (backupModeItem != null)
			{
				m_configuration.BackupCreationMode = backupModeItem.Data;
			}
			m_configuration.CheckForApplicationUpdates = CheckForApplicationUpdatesCheckBox.Checked;
			m_configuration.CheckForPatchesUpdates = CheckForPatchesUpdatesCheckBox.Checked;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			SaveSettings();
			DialogResult = DialogResult.OK;
		}
	}
}
