using System;
using System.Collections.Generic;
using System.IO;
using NCore;
using NCore.Serialization;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Storages
{
	internal class FirmwareDefinitionsStorage : IFileStorage<FirmwareDefinition>
	{
		#region Implementation of IStorage
		public void Initialize()
		{
			var initEx = Safe.Execute(() => NFEPaths.EnsureDirectoryExists(NFEPaths.DefinitionsDirectory));
			if (initEx == null) return;

			Trace.Warn(initEx, "An error occured during creating definitions directory '{0}'.", NFEPaths.DefinitionsDirectory);
		}
		#endregion

		#region Implementation of IFileStorage<out FirmwareDefinition>
		public FirmwareDefinition TryLoad(string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			try
			{
				using (var fs = File.OpenRead(filePath))
				{
					return Serializer.Read<FirmwareDefinition>(fs);
				}
			}
			catch (Exception ex)
			{
				Trace.Warn(ex, "An error occured during reading definition file '{0}'.", filePath);
				return null;
			}
		}

		public IEnumerable<FirmwareDefinition> LoadAll()
		{
			var result = new List<FirmwareDefinition>();
			var files = Directory.GetFiles(NFEPaths.DefinitionsDirectory, Consts.DefinitionFileExtension, SearchOption.AllDirectories);
			foreach (var filePath in files)
			{
				var definition = TryLoad(filePath);
				if (definition == null) continue;

				definition.FileName = Path.GetFileName(filePath);
				definition.Sha = GitHubApi.GetGitSha(filePath);
				result.Add(definition);
			}
			return result;
		}
		#endregion

		public void Save(FirmwareDefinition definition, string filePath)
		{
			using (var fs = File.Open(filePath, FileMode.Create))
			{
				Serializer.Write(definition, fs);
			}
		}
	}
}
