using System;
using System.IO;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class ConfigurationManager
	{
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
			catch (Exception)
			{
				// Ignore
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
			catch (Exception)
			{
				// Ignore
			}
		}
	}
}
