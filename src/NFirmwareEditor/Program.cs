using System;
using System.Threading;
using System.Windows.Forms;
using NFirmware;
using NFirmwareEditor.Managers;
using NFirmwareEditor.Windows;
using NLog;

namespace NFirmwareEditor
{
	internal static class Program
	{
		private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
			Application.ThreadException += UnhandledThreadExceptionHandler;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (args != null && args.Length == 1)
			{
				if (args[0] == "/configuration")
				{
					Application.Run(new DeviceConfiguratorWindow());
					return;
				}
				if (args[0] == "/updater")
				{
					Application.Run(new FirmwareUpdaterWindow(null, new FirmwareLoader(new FirmwareEncoder())));
					return;
				}
				if (args[0] == "/monitor")
				{
					Application.Run(new DeviceMonitorWindow(new COMConnector()));
					return;
				}
			}
			Application.Run(new MainWindow(args));
		}

		private static void UnhandledThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
		{
			s_logger.Fatal(e.Exception, "UnhandledThreadException");
		}

		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			var e = (Exception)args.ExceptionObject;
			s_logger.Fatal(e, "UnhandledException");
		}
	}
}
