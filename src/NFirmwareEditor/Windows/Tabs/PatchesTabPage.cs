using System;
using System.Collections.Generic;
using System.Drawing;
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
	internal partial class PatchesTabPage : UserControl, IEditorTabPage
	{
		private readonly PatchManager m_patchManager;

		private IEnumerable<Patch> m_patches; 
		private Firmware m_firmware;

		public PatchesTabPage([NotNull] PatchManager patchManager)
		{
			if (patchManager == null) throw new ArgumentNullException("patchManager");

			m_patchManager = patchManager;
			InitializeComponent();

			PatchListView.Resize += (s, e) =>
			{
				NameColumnHeader.Width = PatchListView.Width - VersionColumnHeader.Width - InstalledColumnHeader.Width - 1;
			};
			PatchListView.SelectedIndexChanged += PatchListView_SelectedIndexChanged;

			ApplyPatchesButton.Click += ApplyPatchesButton_Click;
		}

		[NotNull]
		public IEnumerable<Patch> SelectedPatches
		{
			get { return new List<Patch>((from ListViewItem item in PatchListView.CheckedItems select item.Tag).OfType<Patch>()); }
		}

		[CanBeNull]
		public Patch LastSelectedPatch
		{
			get
			{
				if (PatchListView.SelectedItems.Count == 0) return null;
				return PatchListView.SelectedItems[PatchListView.SelectedItems.Count - 1].Tag as Patch;
			}
		}

		public string Title
		{
			get { return "Patches"; }
		}

		public void Initialize(Configuration configuration)
		{
			m_patches = m_patchManager.LoadPatches();
		}

		public void OnActivate()
		{
			DescriptionTextBox.BackColor = Color.White;
			DescriptionTextBox.ReadOnly = true;
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			var patches = m_patches.Where(x => string.Equals(x.Definition, m_firmware.Definition.Name));
			foreach (var patch in patches)
			{
				var isPatchApplied = m_patchManager.IsPatchApplied(patch, m_firmware);
				PatchListView.Items.Add(new ListViewItem(new[] { patch.Name, patch.Version, isPatchApplied ? "Yes" : "No" }) { Tag = patch });
			}
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}

		public void OnWorkspaceReset()
		{
			PatchListView.Items.Clear();
		}

		private void PatchListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (LastSelectedPatch == null) return;

			var sb = new StringBuilder();
			{
				sb.AppendLine(LastSelectedPatch.Description);
				sb.AppendLine("Author: " + LastSelectedPatch.Author);
				sb.AppendLine("Version: " + LastSelectedPatch.Version);
			}
			DescriptionTextBox.Text = sb.ToString();
		}

		private void ApplyPatchesButton_Click(object sender, EventArgs e)
		{
			foreach (var patch in SelectedPatches)
			{
				m_patchManager.ApplyPatch(patch, m_firmware);
			}

			PatchListView.CheckedItems.ForEach(x => x.Checked = false);
			InfoBox.Show("Selected patches were applied successfully.");
		}
	}
}
