using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace NFirmwareEditor.Managers
{
	internal class UpdatesManager
	{
		private readonly string m_currentVersion;
		private readonly TimeSpan m_checkForUpdatesInterval;
		private readonly Timer m_checkForUpdatesTimer;

		public UpdatesManager(string currentVersion, TimeSpan checkForUpdatesInterval)
		{
			if (string.IsNullOrEmpty(currentVersion)) throw new ArgumentNullException("currentVersion");

			m_currentVersion = currentVersion;
			m_checkForUpdatesInterval = checkForUpdatesInterval;
			m_checkForUpdatesTimer = new Timer(CheckForUpdatesCallback);
		}

		public event Action<GitHubRelease> UpdatesAvailable;

		public void StartChecking()
		{
			m_checkForUpdatesTimer.Change(TimeSpan.Zero, m_checkForUpdatesInterval);
		}

		public void StopChecking()
		{
			m_checkForUpdatesTimer.Change(Timeout.Infinite, Timeout.Infinite);
		}

		private void CheckForUpdatesCallback(object state)
		{
			var latestRelease = GetLatestRelease();
			if (latestRelease == null || string.Equals(m_currentVersion, latestRelease.Tag)) return;

			OnUpdatesAvailable(latestRelease);
			StopChecking();
		}

		private GitHubRelease GetLatestRelease()
		{
			try
			{
				using (var client = new WebClientWithCustomTimeout(TimeSpan.FromSeconds(10)))
				{
					var responseJson = client.DownloadString("https://api.github.com/repos/TBXin/NFirmwareEditor/releases/latest");
					var serializer = new DataContractJsonSerializer(typeof(GitHubRelease));
					var releaseObject = serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(responseJson))) as GitHubRelease;
					return releaseObject;
				}
			}
			catch
			{
				return null;
			}
		}

		private class WebClientWithCustomTimeout : WebClient
		{
			private readonly TimeSpan m_requestTimeout;

			public WebClientWithCustomTimeout(TimeSpan requestTimeout)
			{
				m_requestTimeout = requestTimeout;
				Headers["User-Agent"] = "NFirmwareEditor";
			}

			protected override WebRequest GetWebRequest(Uri address)
			{
				var request = base.GetWebRequest(address);
				if (request == null) throw new InvalidOperationException();

				request.Timeout = (int)m_requestTimeout.TotalMilliseconds;
				return request;
			}
		}

		protected virtual void OnUpdatesAvailable(GitHubRelease release)
		{
			var handler = UpdatesAvailable;
			if (handler != null) handler(release);
		}
	}

	[DataContract]
	internal class GitHubRelease
	{
		[DataMember(Name = "tag_name")]
		public string Tag { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "assets")]
		public Asset[] Assets { get; set; }

		[DataMember(Name = "body")]
		public string Description { get; set; }
	}

	[DataContract]
	internal class Asset
	{
		[DataMember(Name = "browser_download_url")]
		public string DownloadUrl { get; set; }
	}
}
