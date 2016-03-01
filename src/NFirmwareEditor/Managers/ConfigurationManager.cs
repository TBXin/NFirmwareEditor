using System;
using System.IO;
using System.Xml.Serialization;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class ConfigurationManager
	{
		public Configuration Load()
		{
			Configuration result = null;
			try
			{
				var serializer = new XmlSerializer(typeof(Configuration));
				using (var fs = File.Open(Paths.SettingsFile, FileMode.Open))
				{
					result = serializer.Deserialize(fs) as Configuration;
				}
			}
			catch (Exception)
			{
				// Ignore
			}
			return result ?? new Configuration();
		}

		public void Save(Configuration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			try
			{
				var serializer = new XmlSerializer(typeof(Configuration));
				using (var fs = File.Open(Paths.SettingsFile, FileMode.Create))
				{
					serializer.Serialize(fs, configuration);
				}
			}
			catch (Exception)
			{
				// Ignore
			}
		}
	}
}
