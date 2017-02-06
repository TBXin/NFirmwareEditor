using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.Storages;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class ResourcePacksTabPage : UserControl, IEditorTabPage
	{
		private readonly ResourcePacksStorage m_resourcePackStorage;

		private IEnumerable<ResourcePackFile> m_allResourcePacks;
		private IEnumerable<ResourcePackFile> m_suitableResourcePacks;

		private Firmware m_firmware;

		public ResourcePacksTabPage([NotNull] ResourcePacksStorage resourcePackStorage)
		{
			if (resourcePackStorage == null) throw new ArgumentNullException("resourcePackStorage");

			m_resourcePackStorage = resourcePackStorage;
			InitializeComponent();

			ResourcePackListView.Resize += (s, e) =>
			{
				NameColumnHeader.Width = ResourcePackListView.ClientRectangle.Width - FileNameColumnHeader.Width - VersionColumnHeader.Width - 1;
			};
			ResourcePackListView.SelectedIndexChanged += ResourcePackListView_SelectedIndexChanged;
			ResourcePackListView.ItemActivate += ResourcePackListView_ItemActivate;
			ResourcePackListView.ColumnClick += ListViewItemComparer.ListViewColumnClick;
			PreviewResourcePackButton.Click += PreviewResourcePackButton_Click;
			ImportResourcePackButton.Click += ImportResourcePackButton_Click;
			ReloadResourcePacksButton.Click += ReloadResourcePacksButton_Click;
		}

		[CanBeNull]
		public ResourcePackFile SelectedResourcePack
		{
			get
			{
				return ResourcePackListView.SelectedItems.Count == 0 ? null : ResourcePackListView.SelectedItems[0].Tag as ResourcePackFile;
			}
		}

		#region Implementation of IEditorTabPage
		public bool IsDirty { get; set; }

		public string Title
		{
			get { return "Resource Packs"; }
		}

		public void Initialize(IEditorTabPageHost host, ApplicationConfiguration configuration)
		{
			m_allResourcePacks = m_resourcePackStorage.LoadAll();
		}

		public void OnWorkspaceReset()
		{
			ResourcePackListView.Items.Clear();

			PreviewResourcePackButton.Enabled = false;
			ImportResourcePackButton.Enabled = false;
			ReloadResourcePacksButton.Enabled = false;
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			m_suitableResourcePacks = m_allResourcePacks.Where(x => x.SuitableDefinitions.Contains(m_firmware.Definition.Name)).OrderBy(x => x.Name);
			ResourcePackListView.Fill(m_suitableResourcePacks.Select(resourcePack => new ListViewItem(new[]
			{
				resourcePack.Name,
				resourcePack.FileName,
				resourcePack.Version
			}) { Tag = resourcePack }));

			ImportResourcePackButton.Enabled = true;
			ReloadResourcePacksButton.Enabled = true;
		}

		public void OnActivate()
		{
			DescriptionTextBox.BackColor = Color.White;
			DescriptionTextBox.ReadOnly = true;
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}
		#endregion

		private void PreviewResourcePack([NotNull] ResourcePack resourcePack)
		{
			if (resourcePack == null) throw new ArgumentNullException("resourcePack");
			if (resourcePack.Images == null || resourcePack.Images.Count == 0) return;

			var originalImageIndices = new List<int>();
			var importedImages = new List<bool[,]>();

			foreach (var exportedImage in resourcePack.Images)
			{
				originalImageIndices.Add(exportedImage.Index);
				importedImages.Add(exportedImage.Data);
			}

			using (var importWindow = new PreviewResourcePackWindow(m_firmware, originalImageIndices, importedImages, true))
			{
				importWindow.Text = Consts.ApplicationTitleWoVersion + @" - Resource Pack Preview";
				importWindow.ImportButtonText = "Import";
				if (importWindow.ShowDialog() != DialogResult.OK) return;

				ImportResourcePack(originalImageIndices, importedImages);
			}
		}

		private void ImportResourcePack([NotNull] IList<int> originalImageIndices, [NotNull] IList<bool[,]> importedImages)
		{
			if (importedImages == null) throw new ArgumentNullException("importedImages");
			if (originalImageIndices == null) throw new ArgumentNullException("originalImageIndices");
			if (importedImages.Count == 0) return;

			for (var i = 0; i < originalImageIndices.Count; i++)
			{
				var originalImageIndex = originalImageIndices[i];
				var importedImage = importedImages[i];

				if (m_firmware.Block1Images.Count > 0)
				{
					FirmwareImageMetadata block1ImageMetadata;
					if (m_firmware.Block1Images.TryGetValue(originalImageIndex, out block1ImageMetadata))
					{
						var block1Image = FirmwareImageProcessor.PasteImage(block1ImageMetadata.CreateImage(), importedImage);
						m_firmware.WriteImage(block1Image, block1ImageMetadata);
					}
				}

				if (m_firmware.Block2Images.Count > 0)
				{
					FirmwareImageMetadata block2ImageMetadata;
					if (m_firmware.Block2Images.TryGetValue(originalImageIndex, out block2ImageMetadata))
					{
						var block2Image = FirmwareImageProcessor.PasteImage(block2ImageMetadata.CreateImage(), importedImage);
						m_firmware.WriteImage(block2Image, block2ImageMetadata);
					}
				}
			}

			IsDirty = true;
			ImageCacheManager.RebuildCache(m_firmware);
		}

		private void ResourcePackListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			PreviewResourcePackButton.Enabled = SelectedResourcePack != null;
			if (SelectedResourcePack == null) return;

			var sb = new StringBuilder();
			{
				sb.AppendLine("Author: " + SelectedResourcePack.Author);
				sb.AppendLine("Version: " + SelectedResourcePack.Version);
				sb.AppendLine();
				sb.AppendLine(SelectedResourcePack.Description);
			}
			DescriptionTextBox.Text = sb.ToString();
		}

		private void ResourcePackListView_ItemActivate(object sender, EventArgs e)
		{
			if (SelectedResourcePack == null) return;
			var resourcePack = m_resourcePackStorage.TryLoad(SelectedResourcePack.FilePath);
			if (resourcePack == null) return;

			PreviewResourcePack(resourcePack);
		}

		private void PreviewResourcePackButton_Click(object sender, EventArgs e)
		{
			if (SelectedResourcePack == null) return;
			var resourcePack = m_resourcePackStorage.TryLoad(SelectedResourcePack.FilePath);
			if (resourcePack == null) return;

			PreviewResourcePack(resourcePack);
		}

		private void ImportResourcePackButton_Click(object sender, EventArgs e)
		{
			string fileName;
			using (var op = new OpenFileDialog { Filter = Consts.ExportResourcePackFilter })
			{
				if (op.ShowDialog() != DialogResult.OK) return;
				fileName = op.FileName;
			}

			var resourcePack = m_resourcePackStorage.TryLoad(fileName);
			if (resourcePack == null || string.IsNullOrEmpty(resourcePack.Definition)) return;
			if (!resourcePack.SuitableDefinitions.Contains(m_firmware.Definition.Name))
			{
				InfoBox.Global.Show("Selected resource pack is incompatible with the loaded firmware.\nResource pack is designed for: "
				             + resourcePack.Definition
				             + "\nOpend firmware is: "
				             + m_firmware.Definition.Name);
				return;
			}

			PreviewResourcePack(resourcePack);
		}

		private void ReloadResourcePacksButton_Click(object sender, EventArgs e)
		{
			m_allResourcePacks = m_resourcePackStorage.LoadAll();

			OnWorkspaceReset();
			OnFirmwareLoaded(m_firmware);

			ResourcePackListView.Focus();
		}
	}
}
