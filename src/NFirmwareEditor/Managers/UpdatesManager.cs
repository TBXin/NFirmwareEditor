using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Models;
using NLog;

namespace NFirmwareEditor.Managers
{
	internal class UpdatesManager
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		private readonly string m_currentVersion;
		private readonly TimeSpan m_checkForUpdatesInterval;
		private readonly Timer m_checkForUpdatesTimer;

		private IEnumerable<Updatable<FirmwareDefinition>> m_definitions = new Updatable<FirmwareDefinition>[0];

		public UpdatesManager(string currentVersion, TimeSpan checkForUpdatesInterval)
		{
			if (string.IsNullOrEmpty(currentVersion)) throw new ArgumentNullException("currentVersion");

			m_currentVersion = currentVersion;
			m_checkForUpdatesInterval = checkForUpdatesInterval;
			m_checkForUpdatesTimer = new Timer(CheckForUpdatesCallback);
		}

		public event Action<UpdatesInfo> UpdatesAvailable;

		public void SetDefinitions([NotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (definitions == null) throw new ArgumentNullException("definitions");
			m_definitions = definitions.Select(x => new Updatable<FirmwareDefinition>
			{
				FileName = x.FileName,
				Sha = x.Sha,
				Entity = x
			});
		}

		public void StartChecking()
		{
			m_checkForUpdatesTimer.Change(TimeSpan.Zero, m_checkForUpdatesInterval);
		}

		public void StopChecking()
		{
			m_checkForUpdatesTimer.Change(Timeout.Infinite, Timeout.Infinite);
		}

		[CanBeNull]
		public ReleaseInfo CheckForReleases()
		{
			s_logger.Info("Checking for application updates...");
			var latestRelease = GitHubApi.GetLatestRelease();
			if (latestRelease == null || latestRelease.Assets.Length == 0)
			{
				s_logger.Info("No application updates found.");
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

			s_logger.Info("New application version is available: " + latestRelease.Tag);
			return new ReleaseInfo
			{
				Version = latestRelease.Tag,
				Description = latestRelease.Description,
				DownloadUrl = latestRelease.Assets[0].DownloadUrl
			};
		}

		[CanBeNull, ItemNotNull]
		private List<GitHubApi.GitHubFileInfo> CheckForDefinitions()
		{
			s_logger.Info("Checking for definitions updates...");
			var definifiosFiles = GitHubApi.GetFiles("Definitions");
			return definifiosFiles == null
				? null
				: GitHubApi.GetEntityForUpdate(definifiosFiles, m_definitions).ToList();
		}

		private void CheckForUpdatesCallback(object state)
		{
			var latestRelease = CheckForReleases();
			var newDefinitions = CheckForDefinitions();

			if (latestRelease == null && (newDefinitions == null || !newDefinitions.Any()))
			{
				return;
			}

			StopChecking();
			OnUpdatesAvailable(new UpdatesInfo
			{
				Release = latestRelease,
				Definitions = newDefinitions
			});
		}

		protected virtual void OnUpdatesAvailable(UpdatesInfo release)
		{
			var handler = UpdatesAvailable;
			if (handler != null) handler(release);
		}
	}

	internal class UpdatesInfo
	{
		[CanBeNull]
		public ReleaseInfo Release { get; set; }

		[CanBeNull, ItemNotNull]
		public IEnumerable<GitHubApi.GitHubFileInfo> Definitions { get; set; }
	}

	internal class ReleaseInfo
	{
		public string Version { get; set; }

		public string Description { get; set; }

		public string DownloadUrl { get; set; }
	}
}
