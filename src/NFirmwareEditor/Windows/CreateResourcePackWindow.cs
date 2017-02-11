using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NCore.UI;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;
using NFirmwareEditor.Storages;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class CreateResourcePackWindow : EditorDialogWindow
	{
		private readonly ResourcePacksStorage m_resourcePackStorage;
		private readonly List<ExportedImage> m_exportedImages;

		public CreateResourcePackWindow()
		{
			InitializeComponent();
			OkButton.Click += OkButton_Click;
		}

		public CreateResourcePackWindow
		(
			[NotNull] ResourcePacksStorage resourcePackStorage,
			[NotNull] IEnumerable<FirmwareDefinition> definitions,
			[NotNull] string definition, 
			[NotNull] List<ExportedImage> exportedImages,
			[CanBeNull] ResourcePackFile existedResourcePack
		) : this()
		{
			if (resourcePackStorage == null) throw new ArgumentNullException("resourcePackStorage");
			if (definitions == null) throw new ArgumentNullException("definitions");
			if (string.IsNullOrEmpty(definition)) throw new ArgumentNullException("definition");
			if (exportedImages == null) throw new ArgumentNullException("exportedImages");

			m_resourcePackStorage = resourcePackStorage;
			m_exportedImages = exportedImages.ToList();

			definitions.ForEach(x => DefinitionComboBox.Items.Add(x));
			if (existedResourcePack == null)
			{
				DefinitionComboBox.Text = definition;
				AuthorTextBox.Text = Environment.UserName;
			}
			else
			{
				NameTextBox.Text = existedResourcePack.Name;
				VersionTextBox.Text = existedResourcePack.Version;
				AuthorTextBox.Text = existedResourcePack.Author;
				DefinitionComboBox.Text = existedResourcePack.Definition;
				DescriptionTextBox.Text = existedResourcePack.Description;
			}
			flowLayoutPanel1.SuspendLayout();
			foreach (var exportedImage in exportedImages.Select(x => x.Data))
			{
				flowLayoutPanel1.Controls.Add(CreateGrid(exportedImage));
			}
			flowLayoutPanel1.ResumeLayout();
		}

		private PixelGrid CreateGrid(bool[,] imageData)
		{
			var imageSize = imageData.GetSize();
			var blockSize = imageSize.Height > 64 ? 1 : 2;
			return new PixelGrid
			{
				Width = flowLayoutPanel1.Width,
				Height = 132,
				Margin = new Padding(1, 1, 1, 0),
				BlockSize = blockSize,
				ShowGrid = false,
				Data = imageData,
				BackColor = Color.Black,
				BlockInnerBorderPen = Pens.Transparent,
				BlockOuterBorderPen = Pens.Transparent,
				ActiveBlockBrush = Brushes.White,
				InactiveBlockBrush = Brushes.Black
			};
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(DefinitionComboBox.Text))
			{
				InfoBox.Show("Selected definition first.");
				return;
			}

			string fileName;
			using (var sf = new SaveFileDialog { Filter = Consts.ExportResourcePackFilter, FileName = NameTextBox.Text.Nvl("new_resource_pack") + Consts.ResourcePackFileExtensionWoAsterisk })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			var resourcePack = new ResourcePack(DefinitionComboBox.Text, m_exportedImages)
			{
				Name = NameTextBox.Text,
				Author = AuthorTextBox.Text,
				Version = VersionTextBox.Text,
				Description = DescriptionTextBox.Text
			};

			try
			{
				m_resourcePackStorage.Save(fileName, resourcePack);
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during saving resource pack\n" + ex.Message);
			}
		}
	}
}
