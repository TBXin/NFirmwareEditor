using System;
using System.Windows.Forms;
using NCore;
using NToolbox.Models;
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

			var startupMode = StartupMode.None;
			if (args != null && args.Length > 0)
			{
				var arg = args[0].ToLowerInvariant();
				switch (arg)
				{
					case "/minimized":
						startupMode = StartupMode.Minimized;
						break;
					case "/afconfig":
						startupMode = StartupMode.ArcticFoxConfiguration;
						break;
					case "/monitor":
						startupMode = StartupMode.DeviceMonitor;
						break;
					case "/updater":
						startupMode = StartupMode.FirmwareUpdater;
						break;
				}
			}

			using (var spi = new SingleInstanceProvider("NFE Toolbox © Reiko Kitsune"))
			{
				if (spi.IsCreated)
				{
					spi.ShowFirstInstance();
					return;
				}
				Application.Run(new MainWindow(startupMode));
			}
		}
	}
}
