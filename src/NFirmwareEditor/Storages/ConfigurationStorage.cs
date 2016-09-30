using System;
using System.Collections.Generic;
using System.IO;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;
using NLog;

namespace NFirmwareEditor.Storages
{
	internal class ConfigurationStorage : IFileStorage<ApplicationConfiguration>
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		#region Implementation of IStorage
		public void Initialize()
		{
		}
		#endregion

		#region Implementation of IFileStorage<out ApplicationConfiguration>
		public ApplicationConfiguration TryLoad(string filePath)
		{
			ApplicationConfiguration result = null;
			if (File.Exists(filePath))
			{
				try
				{
					using (var fs = File.OpenRead(filePath))
					{
						result = Serializer.Read<ApplicationConfiguration>(fs);
					}
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex, "An error occurred during loading application configuration.");
				}
			}
			return result ?? new ApplicationConfiguration();
		}

		public IEnumerable<ApplicationConfiguration> LoadAll()
		{
			throw new NotSupportedException();
		}
		#endregion

		public void Save(string filePath, ApplicationConfiguration configuration)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");
			if (configuration == null) throw new ArgumentNullException("configuration");

			try
			{
				using (var fs = File.Create(filePath))
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
