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
		private readonly Configuration m_configuration;

		public OptionsWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			ImageEditorModeComboBox.Items.AddRange(new object[]
			{
				new ImageEditorModeComboBoxItem("LMB sets the pixel, RMB resets", ImageEditorMouseMode.LeftSetRightUnset),
				new ImageEditorModeComboBoxItem("LMB sets or resets the pixel", ImageEditorMouseMode.LeftSetUnset)
			});

			OkButton.Click += OkButton_Click;
		}

		public OptionsWindow([NotNull] Configuration configuration) : this()
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			m_configuration = configuration;

			LoadSettings();
		}

		private void LoadSettings()
		{
			foreach (var item in ImageEditorModeComboBox.Items.Cast<ImageEditorModeComboBoxItem>())
			{
				if (item.Mode != m_configuration.ImageEditorMouseMode) continue;

				ImageEditorModeComboBox.SelectedItem = item;
				break;
			}
			CreateBackupCheckBox.Checked = m_configuration.CreateBackupBeforeSaving;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			var editorModeItem = ImageEditorModeComboBox.SelectedItem as ImageEditorModeComboBoxItem;
			if (editorModeItem == null) return;

			m_configuration.ImageEditorMouseMode = editorModeItem.Mode;
			m_configuration.CreateBackupBeforeSaving = CreateBackupCheckBox.Checked;
			DialogResult = DialogResult.OK;
		}

		private class ImageEditorModeComboBoxItem
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="T:System.Object"/> class.
			/// </summary>
			public ImageEditorModeComboBoxItem(string name, ImageEditorMouseMode mode)
			{
				if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

				Name = name;
				Mode = mode;
			}

			private string Name { get; set; }

			public ImageEditorMouseMode Mode { get; private set; }

			public override string ToString()
			{
				return Name;
			}
		}
	}
}
