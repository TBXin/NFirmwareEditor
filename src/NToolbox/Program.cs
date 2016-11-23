using System;
using System.Windows.Forms;
using NCore;
using NToolbox.Windows;

namespace NToolbox
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var isMinimized = false;
			var isArcticFoxConfiguration = false;
			var isDeviceMonitor = false;
			var isFirmwareUpdater = false;

			if (args != null && args.Length > 0)
			{
				isMinimized = string.Equals(args[0], "/minimized", StringComparison.OrdinalIgnoreCase);
				isArcticFoxConfiguration = string.Equals(args[0], "/afconfig", StringComparison.OrdinalIgnoreCase);
				isDeviceMonitor = string.Equals(args[0], "/monitor", StringComparison.OrdinalIgnoreCase);
				isFirmwareUpdater = string.Equals(args[0], "/updater", StringComparison.OrdinalIgnoreCase);
			}

			using (var spi = new SingleInstanceProvider("NFE Toolbox © Reiko Kitsune"))
			{
				if (spi.IsCreated)
				{
					spi.ShowFirstInstance();
					return;
				}
				Application.Run(new MainWindow(isMinimized));
			}
		}
	}
}
