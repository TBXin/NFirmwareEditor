using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore.UI;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class DefinitionUpdatesAvailableWindow : EditorDialogWindow
	{
		private readonly IEnumerable<GitHubApi.GitHubFileInfo> m_newDefinitions;

		public DefinitionUpdatesAvailableWindow([NotNull] IEnumerable<GitHubApi.GitHubFileInfo> newDefinitions)
		{
			if (newDefinitions == null) throw new ArgumentNullException("newDefinitions");

			m_newDefinitions = newDefinitions;

			InitializeComponent();
			InitializeControls();
		}

		private void InitializeControls()
		{
			var sb = new StringBuilder();
			foreach (var fileInfo in m_newDefinitions)
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
				Parallel.ForEach(m_newDefinitions, definition =>
				{
					var localPath = Path.Combine(NFEPaths.DefinitionsDirectory, definition.Name);

					NFEPaths.EnsureDirectoryExists(NFEPaths.DefinitionsDirectory);
					GitHubApi.DownloadFile(definition.DownloadUrl, localPath);

					UpdateUI(() => ChangesTextBox.AppendText(definition.Name + " done." + Environment.NewLine));
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
