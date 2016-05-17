using System;
using System.Threading;
using NLog;

namespace NFirmwareEditor.Managers
{
	internal class UpdatesManager
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

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

		public event Action<ReleaseInfo> UpdatesAvailable;

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
			s_logger.Info("Checking for updates...");
			var latestRelease = GitHubApi.GetLatestRelease();
			if (latestRelease == null || string.Equals(m_currentVersion, latestRelease.Tag) || latestRelease.Assets.Length == 0)
			{
				s_logger.Info("No updates available.");
				return;
			}

			s_logger.Info("New version available: " + latestRelease.Tag);
			StopChecking();
			OnUpdatesAvailable(new ReleaseInfo
			{
				Version = latestRelease.Tag,
				Description = latestRelease.Description,
				DownloadUrl = latestRelease.Assets[0].DownloadUrl
			});
		}

		protected virtual void OnUpdatesAvailable(ReleaseInfo release)
		{
			var handler = UpdatesAvailable;
			if (handler != null) handler(release);
		}
	}

	internal class ReleaseInfo
	{
		public string Version { get; set; }

		public string Description { get; set; }

		public string DownloadUrl { get; set; }
	}
}
