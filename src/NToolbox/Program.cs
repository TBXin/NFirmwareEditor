using System;
using System.Linq;
using System.Windows.Forms;
using NCore;
using NCore.USB;
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
			AppDomain.CurrentDomain.UnhandledException += Trace.CurrentDomain_UnhandledExceptionHandler;
			Application.ThreadException += Trace.Application_UnhandledThreadExceptionHandler;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var startupMode = StartupArgs.GetMode(args != null && args.Length > 0 ? args[0] : string.Empty);
			string[] remainingArgs = null;
			if (args != null && args.Length > 1 && startupMode != StartupMode.None)
			{
				remainingArgs = args.Skip(1).ToArray();
			}
			using (var spi = new SingleInstanceProvider("NFE Toolbox © Reiko Kitsune"))
			{
				if (spi.IsCreated)
				{
					spi.ShowFirstInstance();
					return;
				}

				HidConnector.Instance.StartUSBConnectionMonitoring();
				ApplicationService.ApplicationName = "NFE Toolbox";
				Application.Run(new MainWindow(startupMode, remainingArgs));
				HidConnector.Instance.StopUSBConnectionMonitoring();
			}
		}
	}
}
