using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
			var result = new List<FirmwareDefinition>();

			var definitionsDirEx = Safe.Execute(() => Paths.EnsureDirectoryExists(Paths.DefinitionsDirectory));
			if (definitionsDirEx != null)
			{
				s_logger.Warn(definitionsDirEx, "An error occured during creating definitions directory '{0}'.", Paths.DefinitionsDirectory);
				return result;
			}

			var files = Directory.GetFiles(Paths.DefinitionsDirectory, Consts.DefinitionFileExtension);
			foreach (var file in files)
			{
				var definitionFile = file;
				FirmwareDefinition definition = null;

				var definitionEx = Safe.Execute(() => definition = Serializer.Read<FirmwareDefinition>(File.OpenRead(definitionFile)));
				if (definitionEx != null)
				{
					s_logger.Warn(definitionEx, "An error occured during reading definition file '{0}'.", definitionFile);
					continue;
				}

				if (definition != null) result.Add(definition);
			}
			return result;
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
