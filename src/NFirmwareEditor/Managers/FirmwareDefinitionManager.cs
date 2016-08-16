using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NFirmware;
using NFirmwareEditor.Core;
using NLog;

namespace NFirmwareEditor.Managers
{
	internal class FirmwareDefinitionManager
	{
		private const string RegexPattern = @"(?<device>.+)\s(?<version>[0-9]{1,3}\.[0-9]{1,3}.+)";
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();
		private static readonly Regex s_regex = new Regex(RegexPattern, RegexOptions.Compiled);

		public List<FirmwareDefinition> Load()
		{
			try
			{
				var serializer = new XmlSerializer(typeof(List<FirmwareDefinition>));
				using (var fs = File.OpenRead(Paths.DefinitionsFile))
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
				s_logger.Fatal(ex, "An error occurred during loading firmware definitions.");
				InfoBox.Show(ex.Message);
			}
			return new List<FirmwareDefinition>();
		}

		public IDictionary<string, SortedList<string, FirmwareDefinition>> CreateHierarchy(IEnumerable<FirmwareDefinition> definitions)
		{
			var result = new SortedDictionary<string, SortedList<string, FirmwareDefinition>>(StringComparer.OrdinalIgnoreCase);

			foreach (var definition in definitions)
			{
				var match = s_regex.Match(definition.Name);
				if (match.Success)
				{
					var device = match.Groups["device"].Value;
					var version = match.Groups["version"].Value;

					if (result.ContainsKey(device))
					{
						result[device].Add(version, definition);
					}
					else
					{
						result[device] = new SortedList<string, FirmwareDefinition> { { version, definition } };
					}
				}
				else
				{
					result[definition.Name] = new SortedList<string, FirmwareDefinition>
					{
						{ definition.Name, definition }
					};
				}
			}
			return result;
		}
	}
}
