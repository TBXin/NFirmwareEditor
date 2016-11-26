using System;
using System.Threading;
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
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
			Application.ThreadException += UnhandledThreadExceptionHandler;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
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
