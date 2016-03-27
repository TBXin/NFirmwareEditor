using System.Drawing;
using System.IO;
using System.Reflection;

namespace NFirmwareEditor.Core
{
	internal static class Paths
	{
		private const string PatchDirectoryName = "Patches";
		private const string SettingsFileName = "settings.xml";
		private const string DefinitionsFileName = "definitions.xml";

		static Paths()
		{
			var assemblyLocation = Assembly.GetExecutingAssembly().Location;
			ApplicationIcon = Icon.ExtractAssociatedIcon(assemblyLocation);
			ApplicationDirectory = Directory.GetParent(assemblyLocation).FullName;

			SettingsFile = Path.Combine(ApplicationDirectory, SettingsFileName);
			DefinitionsFile = Path.Combine(ApplicationDirectory, DefinitionsFileName);
			PatchDirectory = Path.Combine(ApplicationDirectory, PatchDirectoryName);
		}

		public static string ApplicationDirectory { get; private set; }

		public static Icon ApplicationIcon { get; private set; }

		public static string SettingsFile { get; private set; }

		public static string DefinitionsFile { get; private set; }

		public static string PatchDirectory { get; private set; }
	}
}
