using System;
using System.IO;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;
using NLog;

namespace NFirmwareEditor.Managers
{
	internal class ConfigurationManager
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		public ApplicationConfiguration Load()
		{
			ApplicationConfiguration result = null;
			try
			{
				using (var fs = File.Open(Paths.SettingsFile, FileMode.Open))
				{
					result = Serializer.Read<ApplicationConfiguration>(fs);
				}
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex, "An error occurred during loading application configuration.");
			}
			return result ?? new ApplicationConfiguration();
		}

		public void Save(ApplicationConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			try
			{
				using (var fs = File.Open(Paths.SettingsFile, FileMode.Create))
				{
					Serializer.Write(configuration, fs);
				}
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex, "An error occurred during saving application configuration.");
			}
		}
	}
}
