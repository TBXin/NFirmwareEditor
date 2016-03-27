using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class PatchManager
	{
		public IEnumerable<Patch> LoadPatches()
		{
			if (!Directory.Exists(Paths.PatchDirectory)) Directory.CreateDirectory(Paths.PatchDirectory);

			var result = new List<Patch>();
			var files = Directory.GetFiles(Paths.PatchDirectory, "*.patch", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				var serializer = new XmlSerializer(typeof(Patch));
				try
				{
					using (var fs = File.Open(file, FileMode.Open))
					{
						var patch = serializer.Deserialize(fs) as Patch;
						if (patch != null) result.Add(patch);
					}
				}
				catch
				{
					// Ignore
				}
			}
			return result;
		}
	}
}
