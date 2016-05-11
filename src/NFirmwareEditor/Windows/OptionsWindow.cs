using System;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

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
				new NamedComboBoxItem<ImageEditorMouseMode>("LMB sets the pixel, RMB resets", ImageEditorMouseMode.LeftSetRightUnset),
				new NamedComboBoxItem<ImageEditorMouseMode>("LMB sets or resets the pixel", ImageEditorMouseMode.LeftSetUnset)
			});

			BackupModeComboBox.Items.AddRange(new object[]
			{
				new NamedComboBoxItem<BackupCreationMode>("Disabled (do not create backups)", BackupCreationMode.Disabled),
				new NamedComboBoxItem<BackupCreationMode>("Simple (overwrites backup file on each save)", BackupCreationMode.Simple),
				new NamedComboBoxItem<BackupCreationMode>("Extended (stores backups with unique names in the separate directory)", BackupCreationMode.Extended)
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
			foreach (var item in ImageEditorModeComboBox.Items.Cast<NamedComboBoxItem<ImageEditorMouseMode>>())
			{
				if (item.Data != m_configuration.ImageEditorMouseMode) continue;

				ImageEditorModeComboBox.SelectedItem = item;
				break;
			}
			foreach (var item in BackupModeComboBox.Items.Cast<NamedComboBoxItem<BackupCreationMode>>())
			{
				if (item.Data != m_configuration.BackupCreationMode) continue;

				BackupModeComboBox.SelectedItem = item;
				break;
			}
			CheckForUpdatesCheckBox.Checked = m_configuration.CheckForUpdates;
		}

		private void SaveSettings()
		{
			var editorModeItem = ImageEditorModeComboBox.SelectedItem as NamedComboBoxItem<ImageEditorMouseMode>;
			if (editorModeItem != null)
			{
				m_configuration.ImageEditorMouseMode = editorModeItem.Data;
			}
			var backupModeItem = BackupModeComboBox.SelectedItem as NamedComboBoxItem<BackupCreationMode>;
			if (backupModeItem != null)
			{
				m_configuration.BackupCreationMode = backupModeItem.Data;
			}
			m_configuration.CheckForUpdates = CheckForUpdatesCheckBox.Checked;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			SaveSettings();
			DialogResult = DialogResult.OK;
		}

		private class NamedComboBoxItem<T>
		{
			public NamedComboBoxItem(string name, T data)
			{
				if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

				Name = name;
				Data = data;
			}

			private string Name { get; set; }

			public T Data { get; private set; }

			public override string ToString()
			{
				return Name;
			}
		}
	}
}
