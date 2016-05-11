using System;
using System.Windows.Forms;
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
		private static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}

		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			var e = (Exception)args.ExceptionObject;
			s_logger.Fatal(e);
		}
	}
}
