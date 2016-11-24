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

			var startupMode = StartupArgs.GetMode(args != null && args.Length > 0 ? args[0] : string.Empty);
			using (var spi = new SingleInstanceProvider("NFE Toolbox © Reiko Kitsune"))
			{
				if (spi.IsCreated)
				{
					spi.ShowFirstInstance();
					return;
				}

				ApplicationService.ApplicationName = "NFE Toolbox";
				Application.Run(new MainWindow(startupMode));
			}
		}
	}
}
