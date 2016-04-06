using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class ResourcePacksTabPage : UserControl, IEditorTabPage
	{
		private readonly ResourcePackManager m_resourcePackManager;

		private IEnumerable<ResourcePackFile> m_allResourcePacks;
		private IEnumerable<ResourcePackFile> m_suitableResourcePacks;

		private Firmware m_firmware;

		public ResourcePacksTabPage([NotNull] ResourcePackManager resourcePackManager)
		{
			if (resourcePackManager == null) throw new ArgumentNullException("resourcePackManager");

			m_resourcePackManager = resourcePackManager;
			InitializeComponent();

			ResourcePackListView.Resize += (s, e) =>
			{
				NameColumnHeader.Width = ResourcePackListView.Width - VersionColumnHeader.Width - 1;
			};
			ResourcePackListView.SelectedIndexChanged += ResourcePackListView_SelectedIndexChanged;
			ResourcePackListView.ItemActivate += ResourcePackListView_ItemActivate;
			PreviewResourcePackButton.Click += PreviewResourcePackButton_Click;
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
		public string Title
		{
			get { return "Resource Packs"; }
		}

		public void Initialize(IEditorTabPageHost host, Configuration configuration)
		{
			m_allResourcePacks = m_resourcePackManager.LoadAll();
		}

		public void OnWorkspaceReset()
		{
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			m_suitableResourcePacks = m_allResourcePacks.Where(x => string.Equals(x.Definition, m_firmware.Definition.Name));
			ResourcePackListView.Fill(m_suitableResourcePacks.Select(resourcePack => new ListViewItem(new[]
			{
				resourcePack.Name,
				resourcePack.Version
			}) { Tag = resourcePack }));

			ReloadResourcePacksButton.Enabled = true;
		}

		public void OnActivate()
		{
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}
		#endregion

		private void PreviewResourcePack()
		{
			if (SelectedResourcePack == null) return;

			var resourcePack = m_resourcePackManager.LoadFromFile(SelectedResourcePack.FilePath);
			if (resourcePack == null || resourcePack.Images == null || resourcePack.Images.Count == 0) return;

			var originalImageIndices = resourcePack.Images.Select(x => x.Index).ToList();
			var importedImages = resourcePack.Images.Select(x => x.Data).ToList();

			ImportImages(originalImageIndices, importedImages, true);
		}

		private void ImportImages([NotNull] IList<int> originalImageIndices, [NotNull] IList<bool[,]> importedImages, bool importResourcePack)
		{
			if (importedImages == null) throw new ArgumentNullException("importedImages");
			if (originalImageIndices == null) throw new ArgumentNullException("originalImageIndices");
			if (importedImages.Count == 0) return;

			var minimumImagesCount = Math.Min(originalImageIndices.Count, importedImages.Count);

			originalImageIndices = originalImageIndices.Take(minimumImagesCount).ToList();
			importedImages = importedImages.Take(minimumImagesCount).ToList();

			using (var importWindow = new ImportImageWindow(m_firmware, originalImageIndices, importedImages, importResourcePack))
			{
				if (importWindow.ShowDialog() != DialogResult.OK) return;
				importedImages = importWindow.GetImportedImages().ToList();
			}

			for (var i = 0; i < minimumImagesCount; i++)
			{
				var index = i;
				var block1ImageMetadata = m_firmware.Block1Images.First(x => x.Index == originalImageIndices[index]);
				var block2ImageMetadata = m_firmware.Block2Images.First(x => x.Index == originalImageIndices[index]);
				var block2Image = FirmwareImageProcessor.PasteImage(new bool[block2ImageMetadata.Width, block2ImageMetadata.Height], importedImages[index]);

				m_firmware.WriteImage(importedImages[index], block1ImageMetadata);
				m_firmware.WriteImage(block2Image, block2ImageMetadata);
			}

			ImageCacheManager.RebuildImageCache(m_firmware);
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
				sb.AppendLine((SelectedResourcePack.Description ?? string.Empty).Trim().Replace("\n", Environment.NewLine));
			}
			DescriptionTextBox.Text = sb.ToString();
		}

		private void ResourcePackListView_ItemActivate(object sender, EventArgs e)
		{
			PreviewResourcePack();
		}

		private void PreviewResourcePackButton_Click(object sender, EventArgs e)
		{
			PreviewResourcePack();
		}

		private void ReloadResourcePacksButton_Click(object sender, EventArgs e)
		{
			m_allResourcePacks = m_resourcePackManager.LoadAll();

			OnWorkspaceReset();
			OnFirmwareLoaded(m_firmware);

			ResourcePackListView.Focus();
		}
	}
}
