using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace NCore
{
	public static class ApplicationService
	{
		private static string s_applicationName;
		private const string AutorunRegistryPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string LanguagePackDirectoryName = "Languages";

		static ApplicationService()
		{
			try
			{
				ApplicationExecutableLocation = Application.ExecutablePath;
				ApplicationIcon = Icon.ExtractAssociatedIcon(ApplicationExecutableLocation);
				IsIconAvailable = true;
			}
			catch
			{
				IsIconAvailable = false;
			}
			ApplicationDirectory = Directory.GetParent(ApplicationExecutableLocation).FullName;
			LanguagePacksDirectory = Path.Combine(ApplicationDirectory, LanguagePackDirectoryName);
		}

		public static string ApplicationExecutableLocation { get; private set; }

		public static string ApplicationDirectory { get; private set; }

		public static string LanguagePacksDirectory { get; private set; }

		public static bool IsIconAvailable { get; private set; }

		public static Icon ApplicationIcon { get; private set; }

		public static string ApplicationName
		{
			get
			{
				return string.IsNullOrEmpty(s_applicationName)
					? Path.GetFileName(ApplicationExecutableLocation)
					: s_applicationName;
			}
			set { s_applicationName = value; }
		}

		/// <summary>
		/// Gets the autorun state for the application.
		/// </summary>
		/// <returns>Returns true if autorun is enabled, otherwise false.</returns>
		public static bool GetAutorunState(string args = null)
		{
			try
			{
				var currentVersionRun = Registry.CurrentUser.OpenSubKey(AutorunRegistryPath, true);
				if (currentVersionRun == null)
				{
					//Trace.Error("Registry key '{0}' does not exists!", AutorunRegistryPath);
					return false;
				}
				using (currentVersionRun)
				{
					var desiredValue = GetAutorunValue(args);
					var value = currentVersionRun.GetValue(ApplicationName);
					var result = value != null && value.ToString().Equals(desiredValue, StringComparison.OrdinalIgnoreCase);
					//Trace.Info("Autorun is {0}.", result ? "enabled" : "disabled");
					return result;
				}
			}
			catch (Exception /*ex*/)
			{
				//Trace.ErrorException("Unable to read the autorun state.", ex);
				return false;
			}
		}

		/// <summary>
		/// Creates autorun record for the application in the registry.
		/// </summary>
		/// <param name="enable">Creates (true) or deletes (false) autorun record.</param>
		/// <param name="args">Additional startup args.</param>
		public static bool UpdateAutorunState(bool enable, string args = null)
		{
			try
			{
				var currentVersionRun = Registry.CurrentUser.OpenSubKey(AutorunRegistryPath, true);
				if (currentVersionRun == null)
				{
					//Trace.Error("Registry key '{0}' does not exists!", AutorunRegistryPath);
					return false;
				}
				using (currentVersionRun)
				{
					var desiredValue = GetAutorunValue(args);
					var existedRecord = currentVersionRun.GetValue(ApplicationName);
					var recordExist = existedRecord != null;

					if (enable && recordExist)
					{
						if (!existedRecord.ToString().Equals(desiredValue, StringComparison.OrdinalIgnoreCase))
						{
							//Trace.Info("Autorun record already exists. But seems is not correct. Updating.");
							currentVersionRun.SetValue(ApplicationName, desiredValue);
						}
						//Trace.Info("Autorun record already exists. Nothing to do.");
						return true;
					}
					if (!enable && !recordExist)
					{
						//Trace.Info("Autorun record is not exists. Nothing to do.");
						return true;
					}

					if (enable)
					{
						currentVersionRun.SetValue(ApplicationName, desiredValue);
						//Trace.Info("Autorun record successfully created.");
						return true;
					}
					else
					{
						currentVersionRun.DeleteValue(ApplicationName);
						//Trace.Info("Autorun record successfully deleted.");
						return true;
					}
				}
			}
			catch (Exception /*ex*/)
			{
				//Trace.ErrorException("Unable to {0} the autorun record.".FormatInvariant(enable ? "create" : "delete"), ex);
				return false;
			}
		}

		public static void SetProcessDPIAware()
		{
			if (Environment.OSVersion.Version.Major < 6) return;

			try
			{
				NativeMethods.SetProcessDPIAware();
			}
			catch (Exception ex)
			{
				Trace.Info(ex, "Unable to set DPI aware.");
			}
		}

		public static float GetDpiMultiplier()
		{
			try
			{
				using (var control = new Control())
				{
					using (var gfx = control.CreateGraphics())
					{
						return gfx.DpiX / 96f;
					}
				}
			}
			catch (Exception)
			{
				return 1;
			}
		}

		private static string GetAutorunValue(string args = null)
		{
			return ApplicationExecutableLocation + (string.IsNullOrEmpty(args) ? string.Empty : " " + args);
		}
	}
}
