namespace NToolbox.Models
{
	internal enum StartupMode
	{
		None,
		Minimized,
		ArcticFoxConfiguration,
		MyEvicConfiguration,
		DeviceMonitor,
		FirmwareUpdater
	}

	internal static class StartupArgs
	{
		internal const string Minimzed = "/minimized";
		internal const string ArcticFoxConfiguration = "/afconfig";
		internal const string DeviceMonitor = "/monitor";
		internal const string FirmwareUpdater = "/updater";

		public static StartupMode GetMode(string arg)
		{
			if (string.IsNullOrEmpty(arg)) return StartupMode.None;

			switch (arg)
			{
				case "/minimized": return StartupMode.Minimized;
				case "/afconfig": return StartupMode.ArcticFoxConfiguration;
				case "/monitor": return StartupMode.DeviceMonitor;
				case "/updater": return StartupMode.FirmwareUpdater;
				default: return StartupMode.None;
			}
		}
	}
}
