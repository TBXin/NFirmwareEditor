using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore.UI;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class PatchUpdatesAvailableWindow : EditorDialogWindow
	{
		private readonly FirmwareDefinition m_definition;
		private readonly IEnumerable<GitHubApi.GitHubFileInfo> m_newPatches;

		public PatchUpdatesAvailableWindow([NotNull] FirmwareDefinition definition, [NotNull] IEnumerable<GitHubApi.GitHubFileInfo> newPatches)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			if (newPatches == null) throw new ArgumentNullException("newPatches");

			m_definition = definition;
			m_newPatches = newPatches;

			InitializeComponent();
			InitializeControls();
		}

		private void InitializeControls()
		{
			AvailableLable.Text = @"New patches for: " + m_definition.Name;
			var sb = new StringBuilder();
			foreach (var fileInfo in m_newPatches)
			{
				sb.AppendLine(fileInfo.Name);
			}
			ChangesTextBox.Text = sb.ToString();
			ChangesTextBox.ReadOnly = true;
			ChangesTextBox.BackColor = Color.White;

			DownloadButton.Click += DownloadButton_Click;
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			DownloadButton.Enabled = false;

			ChangesTextBox.AppendText(Environment.NewLine);
			ChangesTextBox.AppendText("Downloading..." + Environment.NewLine);

			var downloadAction = new Action(() =>
			{
				Parallel.ForEach(m_newPatches, patch =>
				{
					var localPath = PatchManager.GetPatchFilePath(m_definition, patch.Name);
					var directory = PatchManager.GetPatchDirectoryPath(m_definition);

					NFEPaths.EnsureDirectoryExists(directory);
					GitHubApi.DownloadFile(patch.DownloadUrl, localPath);

					UpdateUI(() => ChangesTextBox.AppendText(patch.Name + " done." + Environment.NewLine));
				});

				UpdateUI(() =>
				{
					DialogResult = DialogResult.OK;
				});
			});
			downloadAction.BeginInvoke(null, null);
		}
	}
}
