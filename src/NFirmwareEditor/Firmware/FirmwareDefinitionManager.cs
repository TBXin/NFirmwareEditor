using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Firmware
{
	internal static class FirmwareDefinitionManager
	{
		public static List<FirmwareDefinition> Load()
		{
			try
			{
				var serializer = new XmlSerializer(typeof(List<FirmwareDefinition>));
				using (var fs = File.Open(Paths.DefinitionsFile, FileMode.Open))
				{
					var result = serializer.Deserialize(fs) as List<FirmwareDefinition> ?? new List<FirmwareDefinition>();
					if (result.Any(definition => string.IsNullOrEmpty(definition.Name)
					                             || definition.ImageTable1 == null
					                             || definition.ImageTable1.OffsetFrom == 0
					                             || definition.ImageTable1.OffsetTo == 0))
					{
						throw new InvalidDataException("Definition file is malformed.");
					}
					return result;
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show(ex.Message);
			}
			return new List<FirmwareDefinition>();
		}
	}
}
