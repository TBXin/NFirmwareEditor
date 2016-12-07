using System;
using System.Reflection;
using System.Threading;
using NLog;

namespace NCore
{
	public static class Trace
	{
		private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

		public static void Info(Exception ex, string format, params object[] args)
		{
			s_logger.Info(ex, format, args);
		}

		public static void Info(string format, params object[] args)
		{
			Log(LogLevel.Info, format, args);
		}

		public static void Warn(string format, params object[] args)
		{
			Log(LogLevel.Warn, format, args);
		}

		public static void Warn(Exception exception)
		{
			s_logger.Warn(exception);
		}

		public static void Warn(Exception exception, string message)
		{
			s_logger.Warn(exception, message);
		}

		public static void Warn(Exception exception, string format, params object[] args)
		{
			s_logger.Warn(exception, format, args);
		}

		public static void Error(string format, params object[] args)
		{
			Log(LogLevel.Error, format, args);
		}

		public static void ErrorException(string message, Exception exception)
		{
			s_logger.Error(exception, message);
		}

		public static void CurrentDomain_UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
		{
			HandleUnhandledException(e.ExceptionObject as Exception);
			if (!e.IsTerminating) return;

			Info("Application is terminating due to an unhandled exception in a secondary thread.");
		}

		public static void Application_UnhandledThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
		{

			var exception = e.Exception;
			HandleUnhandledException(exception);
		}

		private static void Log(LogLevel level, string format, params object[] args)
		{
			s_logger.Log(level, string.Format(format, args));
		}

		private static void HandleUnhandledException(Exception exception)
		{
			var message = "Unhandled exception";
			try
			{
				var assemblyName = Assembly.GetExecutingAssembly().GetName();
				message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
			}
			catch (Exception e)
			{
				ErrorException("Exception in unhandled exception handler", e);
			}
			finally
			{
				ErrorException(message, exception);
			}
		}
	}
}
