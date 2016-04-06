using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
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
			foreach (var patch in m_suitableResourcePacks)
			{
				var item = new ListViewItem(new[] { patch.Name, patch.Version }) { Tag = patch };
				ResourcePackListView.Items.Add(item);
			}

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

		private void ResourcePackListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedResourcePack == null) return;

			var sb = new StringBuilder();
			{
				sb.AppendLine("Author: " + SelectedResourcePack.Author);
				sb.AppendLine("Version: " + SelectedResourcePack.Version);
				sb.AppendLine();
				sb.AppendLine(SelectedResourcePack.Description.Trim().Replace("\n", Environment.NewLine));
			}
			DescriptionTextBox.Text = sb.ToString();
		}
	}
}
