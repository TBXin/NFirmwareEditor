using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using JetBrains.Annotations;
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

		public void SavePatch(string filePath, [NotNull] Patch patch)
		{
			if(string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");
			if (patch == null) throw new ArgumentNullException("patch");

			var serializer = new XmlSerializer(typeof(Patch));
			using (var fs = File.Open(filePath, FileMode.Create))
			{
				serializer.Serialize(fs, patch);
			}
		}

		public string CompareFiles([NotNull] byte[] file1, [NotNull] byte[] file2)
		{
			if (file1 == null) throw new ArgumentNullException("file1");
			if (file2 == null) throw new ArgumentNullException("file2");

			var result = new StringBuilder();
			for (var i = 0; i < file2.Length; i++)
			{
				var sourceByte = GetByte(file1, i);
				var patchedByte = file2[i];
				if (sourceByte == patchedByte) continue;

				result.AppendLine("{0:X8}: {1} -> {2:X2}", i, sourceByte.HasValue ? sourceByte.Value.ToString("X2") : "null", patchedByte);
			}
			if (file1.Length > file2.Length)
			{
				for (var i = file2.Length; i < file1.Length; i++)
				{
					result.AppendLine("{0:X8}: {1:X2} -> {2}", i, file1[i], "null");
				}
			}
			return result.ToString();
		}

		private static byte? GetByte(byte[] source, int offset)
		{
			if (source.Length <= offset) return null;
			return source[offset];
		}
	}
}
