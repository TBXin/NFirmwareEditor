using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class CreateResourcePackWindow : Form
	{
		private readonly ResourcePackManager m_resourcePackManager;
		private readonly string m_definition;
		private readonly List<ExportedImage> m_exportedImages;

		public CreateResourcePackWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			OkButton.Click += OkButton_Click;
		}

		public CreateResourcePackWindow
		(
			[NotNull] ResourcePackManager resourcePackManager, 
			[NotNull] string definition, 
			[NotNull] List<ExportedImage> exportedImages
		) : this()
		{
			if (resourcePackManager == null) throw new ArgumentNullException("resourcePackManager");
			if (string.IsNullOrEmpty(definition)) throw new ArgumentNullException("definition");
			if (exportedImages == null) throw new ArgumentNullException("exportedImages");

			m_resourcePackManager = resourcePackManager;
			m_definition = definition;
			m_exportedImages = exportedImages.ToList();

			AuthorTextBox.Text = Environment.UserName;
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
			string fileName;
			using (var sf = new SaveFileDialog { Filter = Consts.ExportResourcePackFilter, FileName = NameTextBox.Text.Nvl("new_resource_pack") + Consts.ResourcePackFileExtensionWoAsterisk })
			{
				if (sf.ShowDialog() != DialogResult.OK) return;
				fileName = sf.FileName;
			}

			var resourcePack = new ResourcePack(m_definition, m_exportedImages)
			{
				Name = NameTextBox.Text,
				Author = AuthorTextBox.Text,
				Version = VersionTextBox.Text,
				Description = DescriptionTextBox.Text
			};

			try
			{
				m_resourcePackManager.SaveToFile(fileName, resourcePack);
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				InfoBox.Show("An error occured during saving resource pack\n" + ex.Message);
			}
		}
	}
}
