using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore;
using NFirmware;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Models;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows.Tabs
{
	internal partial class PatchesTabPage : UserControl, IEditorTabPage
	{
		private readonly PatchManager m_patchManager;
		private readonly IDictionary<string, bool> m_checkedFirmwares = new Dictionary<string, bool>();

		private Firmware m_firmware;
		private IEnumerable<Patch> m_suitablePatches;
		private IEditorTabPageHost m_host;
		private ApplicationConfiguration m_configuration;

		public PatchesTabPage([NotNull] PatchManager patchManager)
		{
			if (patchManager == null) throw new ArgumentNullException("patchManager");

			m_patchManager = patchManager;
			InitializeComponent();

			PatchListView.Resize += (s, e) =>
			{
				NameColumnHeader.Width = PatchListView.ClientRectangle.Width -
				                         FileNameColumnHeader.Width -
				                         VersionColumnHeader.Width -
				                         InstalledColumnHeader.Width -
				                         CompatibleColumnHeader.Width - 1;
			};
			PatchListView.SelectedIndexChanged += PatchListView_SelectedIndexChanged;
			PatchListView.ItemChecked += PatchListView_ItemChecked;
			PatchListView.ColumnClick += ListViewItemComparer.ListViewColumnClick;
			ApplyPatchesButton.Click += ApplyPatchesButton_Click;
			RollbackPatchesButton.Click += RollbackPatchesButton_Click;
			OpenInEditorButton.Click += OpenInEditorButton_Click;
			CheckForUpdatesButton.Click += CheckForUpdatesButton_Click;
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

		public bool IsDirty { get; set; }

		public string Title
		{
			get { return "Patches"; }
		}

		public void Initialize(IEditorTabPageHost host, ApplicationConfiguration configuration)
		{
			m_host = host;
			m_configuration = configuration;
		}

		public void OnActivate()
		{
			DescriptionTextBox.BackColor = Color.White;
			DescriptionTextBox.ReadOnly = true;
			ConflictsTextBox.BackColor = Color.White;
			ConflictsTextBox.ReadOnly = true;
		}

		public void OnFirmwareLoaded(Firmware firmware)
		{
			m_firmware = firmware;
			m_suitablePatches = m_patchManager.LoadPatchesForFirmware(firmware.Definition).OrderBy(x => x.Name);

			PatchListView.Fill(m_suitablePatches.Select(patch =>
			{
				patch.IsApplied = m_patchManager.IsPatchApplied(patch, m_firmware);
				patch.IsCompatible = m_patchManager.IsPatchCompatible(patch, m_firmware) || patch.IsApplied;
				return new ListViewItem(new[]
				{
					patch.Name,
					patch.FileName,
					patch.Version,
					patch.IsApplied ? "Yes" : "No",
					patch.IsCompatible ? "Yes" : "No"
				})
				{ Tag = patch };
			}));
			ReloadPatchesButton.Enabled = true;

			if (m_configuration.CheckForApplicationUpdates)
			{
				bool updatesAlreadyChecked;
				m_checkedFirmwares.TryGetValue(firmware.Definition.Name, out updatesAlreadyChecked);

				if (!updatesAlreadyChecked)
				{
					m_checkedFirmwares[firmware.Definition.Name] = true;
					CheckForUpdatesWithUserInteraction(false);
				}
			}
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

			ApplyPatchesButton.Enabled = false;
			RollbackPatchesButton.Enabled = false;
			OpenInEditorButton.Enabled = false;
			CheckForUpdatesButton.Enabled = false;
			ReloadPatchesButton.Enabled = false;
		}

		private void CheckForUpdatesWithUserInteraction(bool notifyWhenNoUpdatesAvailable)
		{
			Invoke(new Action(() =>
			{
				CheckForUpdatesButton.Enabled = false;
				PatchListView.Focus();
			}));
			var checkForUpdates = new Action(() =>
			{
				var newPatches = UpdatesManager.CheckForNewPatches(m_firmware.Definition, m_suitablePatches);
				Invoke(new Action(() =>
				{
					CheckForUpdatesButton.Enabled = false;
					PatchListView.Focus();

					CheckForUpdatesButton.Enabled = true;
					if (newPatches == null || newPatches.Count == 0)
					{
						if (notifyWhenNoUpdatesAvailable)
						{
							InfoBox.Global.Show("No updates available!");
						}
						return;
					}

					using (var newPatchesWindow = new PatchUpdatesAvailableWindow(m_firmware.Definition, newPatches))
					{
						if (newPatchesWindow.ShowDialog() != DialogResult.OK) return;
						ReloadPatchesButton_Click(null, EventArgs.Empty);
					}
				}));
			});
			checkForUpdates.BeginInvoke(null, null);
		}

		private void PatchListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			OpenInEditorButton.Enabled = SelectedPatch != null;
			if (SelectedPatch == null) return;

			var sb = new StringBuilder();
			{
				sb.AppendLine("Author: " + SelectedPatch.Author);
				sb.AppendLine("Version: " + SelectedPatch.Version);
				sb.AppendLine();
				sb.AppendLine(SelectedPatch.Description);
			}
			DescriptionTextBox.Text = sb.ToString();

			var conflicts = m_patchManager.CheckConflicts(SelectedPatch, m_suitablePatches);
			sb = new StringBuilder();
			foreach (var conflict in conflicts)
			{
				sb.AppendLine(conflict.ToString());
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
				InfoBox.Global.Show("Selected patches were already installed.");
				return;
			}

			var result = m_patchManager.BulkOperation(candidates, p => m_patchManager.ApplyPatch(p, m_firmware));
			if (result.ProceededPatches.Count > 0) m_host.ReloadFirmware(this);

			var sb = new StringBuilder();
			if (result.ProceededPatches.Count > 0)
			{
				sb.AppendLine("Patching is completed.");
				sb.AppendLine();
				sb.AppendLine("List of installed patches:");
				foreach (var patch in result.ProceededPatches)
				{
					sb.AppendLine(" - " + patch);
				}
				IsDirty = true;
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
					sb.AppendLine(" - " + patch);
				}
			}
			InfoBox.Global.Show(sb.ToString());
		}
		private void RollbackPatchesButton_Click(object sender, EventArgs e)
		{
			if (!CheckedPatches.Any()) return;

			var candidates = CheckedPatches.Where(x => x.IsApplied).ToList();
			PatchListView.CheckedItems.ForEach(x => x.Checked = false);
			PatchListView.Focus();
			if (!candidates.Any())
			{
				InfoBox.Global.Show("Selected patches are not installed.");
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
					sb.AppendLine(" - " + patch);
				}
				IsDirty = true;
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
					sb.AppendLine(" - " + patch);
				}
			}
			InfoBox.Global.Show(sb.ToString());
		}

		private void OpenInEditorButton_Click(object sender, EventArgs e)
		{
			if (SelectedPatch == null) return;

			var ex = Safe.Execute(() => Process.Start(SelectedPatch.FilePath));
			if (ex != null) InfoBox.Global.Show("An error occured during opening patch file.\n" + ex.Message);
		}

		private void CheckForUpdatesButton_Click(object sender, EventArgs e)
		{
			CheckForUpdatesWithUserInteraction(true);
		}

		private void ReloadPatchesButton_Click(object sender, EventArgs e)
		{
			var selectedItem = SelectedPatch;

			OnWorkspaceReset();
			OnFirmwareLoaded(m_firmware);
			CheckForUpdatesButton.Enabled = true;

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
