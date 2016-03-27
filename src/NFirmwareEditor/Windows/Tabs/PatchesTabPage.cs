using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Windows.Tabs
{
	public partial class PatchesTabPage : UserControl, IEditorTabPage
	{
		private readonly PatchManager m_patchManager = new PatchManager();

		private IEnumerable<Patch> m_patches; 
		private Firmware m_firmware;

		public PatchesTabPage()
		{
			InitializeComponent();

			PatchListView.Resize += (s, e) =>
			{
				NameColumnHeader.Width = PatchListView.Width - VersionColumnHeader.Width - InstalledColumnHeader.Width - 1;
			};
			PatchListView.SelectedIndexChanged += PatchListView_SelectedIndexChanged;
		}

		[NotNull]
		public IEnumerable<Patch> SelectedPatches
		{
			get
			{
				var result = new List<Patch>();
				foreach (int selectedIndex in PatchListView.SelectedIndices)
				{
					var patch = PatchListView.Items[selectedIndex].Tag as Patch;
					if (patch != null) result.Add(patch);
				}
				return result;
			}
		}

		[CanBeNull]
		public Patch LastSelectedPatch
		{
			get
			{
				if (PatchListView.Items.Count == 0 || PatchListView.SelectedIndices.Count == 0) return null;

				var item = PatchListView.Items[PatchListView.SelectedIndices[PatchListView.SelectedIndices.Count - 1]];
				return item.Tag as Patch;
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

			var patches = m_patches.Where(x => x.Definition.Equals(m_firmware.Definition.Name));
			foreach (var patch in patches)
			{
				var isPatchApplied = IsPatchApplied(patch);
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

		private bool IsPatchApplied(Patch patch)
		{
			return patch.Data.All(data => m_firmware.BodyStream.ReadByte(data.Offset) == data.PatchedValue);
		}

		private void PatchListView_SelectedIndexChanged(object sender, System.EventArgs e)
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
	}
}
