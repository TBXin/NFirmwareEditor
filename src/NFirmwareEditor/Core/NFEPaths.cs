using System;
using System.Drawing;
using System.IO;
using JetBrains.Annotations;
using NCore;

namespace NFirmwareEditor.Core
{
	internal static class NFEPaths
	{
		private const string DefinitionsDirectoryName = "Definitions";
		private const string PatchDirectoryName = "Patches";
		private const string ResourcePackDirectoryName = "ResourcePacks";
		private const string SettingsFileName = "settings.xml";
		private const string DefinitionsFileName = "definitions.xml";

		static NFEPaths()
		{
			ApplicationDirectory = ApplicationService.ApplicationDirectory;
			ApplicationIcon = ApplicationService.ApplicationIcon;

			SettingsFile = Path.Combine(ApplicationDirectory, SettingsFileName);
			DefinitionsFile = Path.Combine(ApplicationDirectory, DefinitionsFileName);

			DefinitionsDirectory = Path.Combine(ApplicationDirectory, DefinitionsDirectoryName);
			PatchDirectory = Path.Combine(ApplicationDirectory, PatchDirectoryName);
			ResourcePackDirectory = Path.Combine(ApplicationDirectory, ResourcePackDirectoryName);
		}

		public static string ApplicationDirectory { get; private set; }

		public static Icon ApplicationIcon { get; private set; }

		public static string SettingsFile { get; private set; }

		public static string DefinitionsFile { get; private set; }

		public static string DefinitionsDirectory { get; private set; }

		public static string PatchDirectory { get; private set; }

		public static string ResourcePackDirectory { get; private set; }

		public static void EnsureDirectoryExists([NotNull] string directoryPath)
		{
			if (string.IsNullOrEmpty(directoryPath)) throw new ArgumentNullException("directoryPath");
			if (Directory.Exists(directoryPath)) return;

			Directory.CreateDirectory(directoryPath);
		}

		[CanBeNull]
		public static string ValidateInputArgs([CanBeNull] string[] args)
		{
			if (args == null || args.Length != 1) return null;

			var filePath = args[0];
			return File.Exists(filePath) ? filePath : null;
		}
	}
}
