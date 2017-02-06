using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NCore;
using NFirmware;

namespace NFirmwareEditor.Managers
{
	internal class FirmwareDefinitionManager
	{
		private const string RegexPattern = @"(?<device>.+)\s(?<version>[0-9]{1,3}\.[0-9]{1,3}.+)";
		private static readonly Regex s_regex = new Regex(RegexPattern, RegexOptions.Compiled);

		internal static IDictionary<string, SortedList<string, FirmwareDefinition>> CreateHierarchy(IEnumerable<FirmwareDefinition> definitions)
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
						if (result[device].ContainsKey(version))
						{
							var message = "Detected multiple definitions with the same code: " + definition.Name;
							{
								InfoBox.Global.Show(message);
								Trace.Warn(message);
							}
							continue;
						}
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
