using System.Diagnostics;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	internal partial class AboutWindow : EditorDialogWindow
	{
		public AboutWindow()
		{
			InitializeComponent();
			Icon = NFEPaths.ApplicationIcon;
			ApplicationTitleLable.Text = Consts.ApplicationTitle;

			ReikoKitsuneLinkLabel.Click += ReikoKitsuneLinkLabel_Click;
			MaelstromLinkLabel.Click += MaelstromLinkLabel_Click;
			ZingerLinkLabel.Click += ZingerLinkLabel_Click;
			WebsiteLinkLabel.Click += WebsiteLinkLabel_Click;
			ProjectPageLinkLabel.Click += ProjectPageLinkLabel_Click;
			ReleasesLinkLabel.Click += ReleasesLinkLabel_Click;
			IssuesLinkLabel.Click += IssuesLinkLabel_Click;
		}

		private void OpenUrl(string url)
		{
			Process.Start(url);
		}

		private void ReikoKitsuneLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl("https://github.com/TBXin");
		}

		private void MaelstromLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl("https://github.com/maelstrom2001");
		}

		private void ZingerLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl("http://www.ecigtalk.ru/members/u75453.html");
		}

		private void WebsiteLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl(WebsiteLinkLabel.Text);
		}

		private void ProjectPageLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl(ProjectPageLinkLabel.Text);
		}

		private void ReleasesLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl(ReleasesLinkLabel.Text);
		}

		private void IssuesLinkLabel_Click(object sender, System.EventArgs e)
		{
			OpenUrl(IssuesLinkLabel.Text);
		}
	}
}
