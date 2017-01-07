using System.Diagnostics;
using NCore.UI;

namespace NToolbox.Windows
{
	internal partial class AboutWindow : EditorDialogWindow
	{
		public AboutWindow()
		{
			InitializeComponent();

			NfeProjectLinkLabel.Click += (s, e) => OpenUrl(NfeProjectLinkLabel.Text);
			NfeReleasesLinkLabel.Click += (s, e) => OpenUrl(NfeReleasesLinkLabel.Text);
			NfeIssuesLinkLabel.Click += (s, e) => OpenUrl(NfeIssuesLinkLabel.Text);
			NfeLatestBuildLinkLabel.Click += (s, e) => OpenUrl("https://ci.appveyor.com/project/TBXin/NFirmwareEditor/branch/master/artifacts");

			ArcticFoxProjectLinkLabel.Click += (s, e) => OpenUrl(ArcticFoxProjectLinkLabel.Text);
			ArcticFoxReleasesLinkLabel.Click += (s, e) => OpenUrl(ArcticFoxReleasesLinkLabel.Text);
			ArcticFoxIssuesLinkLabel.Click += (s, e) => OpenUrl(ArcticFoxIssuesLinkLabel.Text);
			ArcticFoxLatestBuildLinkLabel.Click += (s, e) => OpenUrl("https://www.dropbox.com/sh/lyk6n0nb4zh6o5y/AAABduZkmH2QUbIkztmZZ59ka/nightly?dl=0");

			WebsiteLinkLabel.Click += (s, e) => OpenUrl(WebsiteLinkLabel.Text);
		}

		private void OpenUrl(string url)
		{
			Process.Start(url);
		}
	}
}
