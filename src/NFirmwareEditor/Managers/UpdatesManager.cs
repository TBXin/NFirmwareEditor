using System;
using System.Globalization;
using System.Threading;
using JetBrains.Annotations;
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

		[CanBeNull]
		public ReleaseInfo CheckForUpdates()
		{
			s_logger.Info("Checking for updates...");
			var latestRelease = GitHubApi.GetLatestRelease();
			if (latestRelease == null || latestRelease.Assets.Length == 0)
			{
				s_logger.Info("No updates found.");
				return null;
			}

			float currentVersion;
			float newVersion;

			var v1 = float.TryParse(m_currentVersion, NumberStyles.Float, CultureInfo.InvariantCulture, out currentVersion);
			var v2 = float.TryParse(latestRelease.Tag, NumberStyles.Float, CultureInfo.InvariantCulture, out newVersion);

			if (v1 == false || v2 == false)
			{
				if (string.Equals(m_currentVersion, latestRelease.Tag))
				{
					s_logger.Info("Your are using latest version (string checking).");
					return null;
				}
			}
			else if (currentVersion >= newVersion)
			{
				s_logger.Info("Your are using latest version (float checking).");
				return null;
			}

			s_logger.Info("New version available: " + latestRelease.Tag);
			return new ReleaseInfo
			{
				Version = latestRelease.Tag,
				Description = latestRelease.Description,
				DownloadUrl = latestRelease.Assets[0].DownloadUrl
			};
		}

		private void CheckForUpdatesCallback(object state)
		{
			var latestRelease = CheckForUpdates();
			if (latestRelease == null) return;

			StopChecking();
			OnUpdatesAvailable(latestRelease);
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
