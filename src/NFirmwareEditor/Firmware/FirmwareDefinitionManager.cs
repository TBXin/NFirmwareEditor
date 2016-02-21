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
#if DEBUG
			var data = new List<FirmwareDefinition>
			{
				new FirmwareDefinition
				{
					Name = "Wismec RX200 3.00",
					ImageTable = new ImageTable { OffsetFromString = "0x8808", OffsetToString = "0x8A84" }
				}
			};
#endif
			var serizlier = new XmlSerializer(typeof(List<FirmwareDefinition>));
#if DEBUG
			/*using (var fs = File.Open(Paths.DefinitionsFile, FileMode.Create))
			{
				serizlier.Serialize(fs, data);
			}*/
#endif

			try
			{
				serizlier = new XmlSerializer(typeof(List<FirmwareDefinition>));
				using (var fs = File.Open(Paths.DefinitionsFile, FileMode.Open))
				{
					var result = serizlier.Deserialize(fs) as List<FirmwareDefinition> ?? new List<FirmwareDefinition>();
					if (result.Any(definition => string.IsNullOrEmpty(definition.Name)
					                             || definition.ImageTable == null
					                             || definition.ImageTable.OffsetFrom == 0
					                             || definition.ImageTable.OffsetTo == 0))
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
