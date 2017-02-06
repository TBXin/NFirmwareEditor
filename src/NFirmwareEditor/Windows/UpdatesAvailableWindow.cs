using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using JetBrains.Annotations;
using NCore.UI;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Windows
{
	internal partial class UpdatesAvailableWindow : EditorDialogWindow
	{
		private readonly ReleaseInfo m_release;

		public UpdatesAvailableWindow([NotNull] ReleaseInfo release)
		{
			if (release == null) throw new ArgumentNullException("release");

			m_release = release;
			InitializeComponent();
			InitializeControls();
		}

		private void InitializeControls()
		{
			NewVersionLabel.Text = m_release.Version;
			ChangesTextBox.Text = m_release.Description;
			ChangesTextBox.ReadOnly = true;
			ChangesTextBox.BackColor = Color.White;

			ViewHomePageLinkLabel.Click += (s, e) =>
			{
				Process.Start(Consts.HomePage);
			};

			ViewAllReleasesLinkLabel.Click += (s, e) =>
			{
				Process.Start(Consts.ReleasesPage);
			};

			DownloadButton.Click += (s, e) =>
			{
				Process.Start(m_release.DownloadUrl);
				DialogResult = DialogResult.OK;
			};
		}
	}
}
