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
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class PatchesTabPage : UserControl, IEditorTabPage
	{
		private readonly PatchManager m_patchManager;

		private IEnumerable<Patch> m_allPatches;
		private IEnumerable<Patch> m_suitablePatches;

		private Firmware m_firmware;
		private IEditorTabPageHost m_host;

		public PatchesTabPage([NotNull] PatchManager patchManager)
		{
			if (patchManager == null) throw new ArgumentNullException("patchManager");

			m_patchManager = patchManager;
			InitializeComponent();

			PatchListView.Resize += (s, e) =>
			{
				NameColumnHeader.Width = PatchListView.Width -
				                         VersionColumnHeader.Width -
				                         InstalledColumnHeader.Width -
				                         CompatibleColumnHeader.Width - 1;
			};
			PatchListView.SelectedIndexChanged += PatchListView_SelectedIndexChanged;
			PatchListView.ItemChecked += PatchListView_ItemChecked;
			PatchListView.ColumnClick += ListViewItemComparer.ListViewColumnClick;
			ApplyPatchesButton.Click += ApplyPatchesButton_Click;
			RollbackPatchesButton.Click += RollbackPatchesButton_Click;
			ReloadPatchesButton.Click += ReloadPatchesButton_Click;
		}


		[NotNull]
		public IEnumerable<Patch> CheckedPatches
		{
			get { return new List<Patch>((from ListViewItem item in PatchListView.CheckedItems select item.Tag).OfType<Patch>()); }
		}

		[CanBeNull]
		public Patch SelectedPatch
		{
			get { return PatchListView.SelectedItems.Count == 0 ? null : PatchListView.SelectedItems[0].Tag as Patch; }
		}

		public string Title
		{
			get { return "Patches"; }
		}

		public void Initialize(IEditorTabPageHost host, Configuration configuration)
		{
			m_host = host;
			m_allPatches = m_patchManager.LoadAll();
		}

		public void OnActivate()
		{
			DescriptionTextBox.BackColor = Color.White;
			DescriptionTextBox.ReadOnly = true;
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;

			m_suitablePatches = m_allPatches.Where(x => string.Equals(x.Definition, m_firmware.Definition.Name));
			PatchListView.Fill(m_suitablePatches.Select(patch =>
			{
				patch.IsApplied = m_patchManager.IsPatchApplied(patch, m_firmware);
				patch.IsCompatible = m_patchManager.IsPatchCompatible(patch, m_firmware) || patch.IsApplied;
				return new ListViewItem(new[]
				{
					patch.Name,
					patch.Version,
					patch.IsApplied ? "Yes" : "No",
					patch.IsCompatible ? "Yes" : "No"
				})
				{ Tag = patch };
			}));
			ReloadPatchesButton.Enabled = true;
		}

		public bool OnHotkey(Keys keyData)
		{
			return false;
		}

		public void OnWorkspaceReset()
		{
			PatchListView.Items.Clear();
			DescriptionTextBox.Clear();
			ConflictsTextBox.Clear();
		}

		private void PatchListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedPatch == null) return;

			var sb = new StringBuilder();
			{
				sb.AppendLine("Author: " + SelectedPatch.Author);
				sb.AppendLine("Version: " + SelectedPatch.Version);
				sb.AppendLine();
				sb.AppendLine((SelectedPatch.Description ?? string.Empty).Trim().Replace("\n", Environment.NewLine));
			}
			DescriptionTextBox.Text = sb.ToString();

			var conflicts = m_patchManager.CheckConflicts(SelectedPatch, m_suitablePatches);
			sb = new StringBuilder();
			foreach (var conflict in conflicts)
			{
				sb.AppendLine(conflict.Name);
			}
			ConflictsTextBox.Text = sb.ToString();
		}

		private void PatchListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			ApplyPatchesButton.Enabled = RollbackPatchesButton.Enabled = PatchListView.CheckedIndices.Count > 0;
		}

		private void ApplyPatchesButton_Click(object sender, EventArgs e)
		{
			if (!CheckedPatches.Any()) return;

			var candidates = CheckedPatches.Where(x => !x.IsApplied).ToList();
			PatchListView.CheckedItems.ForEach(x => x.Checked = false);
			PatchListView.Focus();
			if (!candidates.Any())
			{
				InfoBox.Show("Selected patches were already installed.");
				return;
			}

			var result = m_patchManager.BulkOperation(candidates, p => m_patchManager.ApplyPatch(p, m_firmware));
			//UpdatePatchStatuses();
			if (result.ProceededPatches.Count > 0) m_host.ReloadFirmware(this);

			var sb = new StringBuilder();
			if (result.ProceededPatches.Count > 0)
			{
				sb.AppendLine("Patching is completed.");
				sb.AppendLine();
				sb.AppendLine("List of installed patches:");
				foreach (var patch in result.ProceededPatches)
				{
					sb.AppendLine(" - " + patch.Name);
				}
			}
			if (result.ConflictedPatches.Count > 0)
			{
				if (result.ConflictedPatches.Count == 0)
				{
					sb.AppendLine("Patching is not completed.");
				}
				sb.AppendLine();
				sb.AppendLine("Patches that have not been installed because of conflicts:");
				foreach (var patch in result.ConflictedPatches)
				{
					sb.AppendLine(" - " + patch.Name);
				}
			}
			InfoBox.Show(sb.ToString());
		}
		private void RollbackPatchesButton_Click(object sender, EventArgs e)
		{
			if (!CheckedPatches.Any()) return;

			var candidates = CheckedPatches.Where(x => x.IsApplied).ToList();
			PatchListView.CheckedItems.ForEach(x => x.Checked = false);
			PatchListView.Focus();
			if (!candidates.Any())
			{
				InfoBox.Show("Selected patches are not installed.");
				return;
			}

			var result = m_patchManager.BulkOperation(candidates, p => m_patchManager.RollbackPatch(p, m_firmware));
			if (result.ProceededPatches.Count > 0) m_host.ReloadFirmware(this);

			var sb = new StringBuilder();
			if (result.ProceededPatches.Count > 0)
			{
				sb.AppendLine("Rollback is completed.");
				sb.AppendLine();
				sb.AppendLine("List of rollbacked patches:");
				foreach (var patch in result.ProceededPatches)
				{
					sb.AppendLine(" - " + patch.Name);
				}
			}
			if (result.ConflictedPatches.Count > 0)
			{
				if (result.ConflictedPatches.Count == 0)
				{
					sb.AppendLine("Rollback is not completed.");
				}
				sb.AppendLine();
				sb.AppendLine("Patches that have not been rollbacked because of conflicts:");
				foreach (var patch in result.ConflictedPatches)
				{
					sb.AppendLine(" - " + patch.Name);
				}
			}
			InfoBox.Show(sb.ToString());
		}

		private void ReloadPatchesButton_Click(object sender, EventArgs e)
		{
			var selectedItem = SelectedPatch;
			m_allPatches = m_patchManager.LoadAll();

			OnWorkspaceReset();
			OnFirmwareLoaded(m_firmware);

			if (selectedItem != null)
			{
				var found = false;
				foreach (ListViewItem item in PatchListView.Items)
				{
					var patch = item.Tag as Patch;
					if (patch == null || !string.Equals(selectedItem.Name, patch.Name)) continue;

					PatchListView.SelectedIndices.Add(item.Index);
					found = true;
					break;
				}
				if (!found && PatchListView.Items.Count > 0)
				{
					PatchListView.SelectedIndices.Add(0);
				}
			}

			PatchListView.Focus();
		}
	}
}
