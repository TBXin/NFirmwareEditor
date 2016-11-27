using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NCore;
using NFirmware;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class UpdatesManager
	{
		private readonly TimeSpan m_checkForUpdatesInterval;
		private readonly Timer m_checkForUpdatesTimer;

		private string m_currentVersion;
		private IEnumerable<FirmwareDefinition> m_definitions = new FirmwareDefinition[0];

		public UpdatesManager(TimeSpan checkForUpdatesInterval)
		{
			m_checkForUpdatesInterval = checkForUpdatesInterval;
			m_checkForUpdatesTimer = new Timer(CheckForUpdatesCallback);
		}

		public event Action<UpdatesInfo> UpdatesAvailable;

		public void SetupInitialData(string applicationVersion, [NotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (string.IsNullOrEmpty(applicationVersion)) throw new ArgumentNullException("applicationVersion");
			if (definitions == null) throw new ArgumentNullException("definitions");

			m_currentVersion = applicationVersion;
			m_definitions = definitions;
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
		public static ReleaseInfo CheckForNewRelease(string applicationVersion)
		{
			Trace.Info("Checking for application updates...");
			var latestRelease = GitHubApi.GetLatestRelease();
			if (latestRelease == null || latestRelease.Assets.Length == 0)
			{
				Trace.Info("No application updates found.");
				return null;
			}

			float currentVersionFloat;
			float newVersionFloat;

			var v1 = float.TryParse(applicationVersion, NumberStyles.Float, CultureInfo.InvariantCulture, out currentVersionFloat);
			var v2 = float.TryParse(latestRelease.Tag, NumberStyles.Float, CultureInfo.InvariantCulture, out newVersionFloat);

			if (v1 == false || v2 == false)
			{
				if (string.Equals(applicationVersion, latestRelease.Tag))
				{
					Trace.Info("Your are using latest version (string checking).");
					return null;
				}
			}
			else if (currentVersionFloat >= newVersionFloat)
			{
				Trace.Info("Your are using latest version (float checking).");
				return null;
			}

			Trace.Info("New application version is available: " + latestRelease.Tag);
			return new ReleaseInfo
			{
				Version = latestRelease.Tag,
				Description = latestRelease.Description,
				DownloadUrl = latestRelease.Assets[0].DownloadUrl
			};
		}

		[CanBeNull, ItemNotNull]
		public static List<GitHubApi.GitHubFileInfo> CheckForNewDefinitions([NotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (definitions == null) throw new ArgumentNullException("definitions");

			Trace.Info("Checking for definitions updates...");
			var definitionsInRepository = GitHubApi.GetFiles("Definitions");
			if (definitionsInRepository == null)
			{
				Trace.Info("Definitions repository is not found.");
				return null;
			}

			var entitiesForUpdate = GitHubApi.GetEntitiesForUpdate(definitionsInRepository, definitions.Select(x => new Updatable<FirmwareDefinition>
			{
			    Entity = x,
			    FileName = x.FileName,
			    Sha = x.Sha
			})).ToList();

			if (entitiesForUpdate.Count == 0)
			{
				Trace.Info("No new definitions were found.");
			}
			return entitiesForUpdate;
		}

		[CanBeNull, ItemNotNull]
		public static List<GitHubApi.GitHubFileInfo> CheckForNewPatches([NotNull] FirmwareDefinition definition, [NotNull] IEnumerable<Patch> patches)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			if (patches == null) throw new ArgumentNullException("patches");

			var patchesInRepository = GitHubApi.GetFiles("Patches/" + definition.Name);
			if (patchesInRepository == null) return null;

			return GitHubApi.GetEntitiesForUpdate(patchesInRepository, patches.Select(x => new Updatable<Patch>
			{
			    Entity = x,
			    FileName = x.FileName,
			    Sha = x.Sha
			})).ToList();
		}

		private void CheckForUpdatesCallback(object state)
		{
			var latestRelease = CheckForNewRelease(m_currentVersion);
			var newDefinitions = CheckForNewDefinitions(m_definitions);

			if (latestRelease == null && (newDefinitions == null || newDefinitions.Count == 0))
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
