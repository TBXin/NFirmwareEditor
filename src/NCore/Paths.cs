using System.Drawing;
using System.IO;
using System.Reflection;

namespace NCore
{
	public static class Paths
	{
		static Paths()
		{
			var assemblyLocation = string.Empty;
			try
			{
				assemblyLocation = Assembly.GetEntryAssembly().Location ?? string.Empty;
				ApplicationIcon = Icon.ExtractAssociatedIcon(assemblyLocation);
				IsIconAvailable = true;
			}
			catch
			{
				IsIconAvailable = false;
			}
			ApplicationDirectory = string.IsNullOrEmpty(assemblyLocation) ? assemblyLocation : Directory.GetParent(assemblyLocation).FullName;
		}

		public static string ApplicationDirectory { get; private set; }

		public static bool IsIconAvailable { get; private set; }

		public static Icon ApplicationIcon { get; private set; }
	}
}
