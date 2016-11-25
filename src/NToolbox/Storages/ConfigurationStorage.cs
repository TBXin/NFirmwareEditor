using System;
using System.IO;
using JetBrains.Annotations;
using NCore.Serialization;
using NToolbox.Models;

namespace NToolbox.Storages
{
	internal class ConfigurationStorage
	{
		#region Implementation of IStorage
		public void Initialize()
		{
		}
		#endregion

		[NotNull]
		public ToolboxConfiguration Load([NotNull] string filePath)
		{
			ToolboxConfiguration result = null;
			if (File.Exists(filePath))
			{
				try
				{
					using (var fs = File.OpenRead(filePath))
					{
						result = Serializer.Read<ToolboxConfiguration>(fs);
					}
				}
				catch (Exception /*ex*/)
				{
					//s_logger.Warn(ex, "An error occurred during loading application configuration.");
				}
			}
			return result ?? new ToolboxConfiguration();
		}

		public void Save([NotNull] string filePath, [NotNull] ToolboxConfiguration configuration)
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
			catch (Exception /*ex*/)
			{
				//s_logger.Warn(ex, "An error occurred during saving application configuration.");
			}
		}
	}
}
