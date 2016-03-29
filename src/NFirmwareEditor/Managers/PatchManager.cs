using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class PatchManager
	{
		private readonly XmlWriterSettings m_xmlWriterSettings = new XmlWriterSettings
		{
			OmitXmlDeclaration = true,
			Indent = true
		};

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
				using (var writer = XmlWriter.Create(fs, m_xmlWriterSettings))
				{
					serializer.Serialize(writer, patch, patch.Namespaces);
				}
			}
		}

		public string CreateDiff([NotNull] byte[] file1, [NotNull] byte[] file2)
		{
			if (file1 == null) throw new ArgumentNullException("file1");
			if (file2 == null) throw new ArgumentNullException("file2");

			var result = new StringBuilder();
			for (var i = 0; i < file2.Length; i++)
			{
				var sourceByte = GetByte(file1, i);
				var patchedByte = file2[i];
				if (sourceByte == patchedByte) continue;

				result.AppendLine("{0:X8}: {1} - {2:X2}", i, sourceByte.HasValue ? sourceByte.Value.ToString("X2") : "null", patchedByte);
			}
			if (file1.Length > file2.Length)
			{
				for (var i = file2.Length; i < file1.Length; i++)
				{
					result.AppendLine("{0:X8}: {1:X2} - {2}", i, file1[i], "null");
				}
			}
			return result.ToString();
		}

		public static IEnumerable<PatchModificationData> ParseDiff(string dataString)
		{
			if (string.IsNullOrEmpty(dataString)) return new List<PatchModificationData>();

			var lines = dataString.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var result = new List<PatchModificationData>();
			foreach (var line in lines.Where(x => string.IsNullOrEmpty(x) || !x.StartsWith("#")).Select(x => x.Replace(" ", string.Empty)))
			{
				var offsetAndData = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
				if (offsetAndData.Length != 2) continue;

				var offset = long.Parse(offsetAndData[0], NumberStyles.AllowHexSpecifier);
				var data = offsetAndData[1];
				if (data.IndexOf(';') != -1) data = data.Substring(0, data.IndexOf(';'));

				var originalPatchedData = data.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
				if (originalPatchedData.Length != 2) continue;

				var originalByte = originalPatchedData[0].Equals("null", StringComparison.OrdinalIgnoreCase)
					? (byte?)null
					: byte.Parse(originalPatchedData[0], NumberStyles.AllowHexSpecifier);
				var patchedByte = byte.Parse(originalPatchedData[1], NumberStyles.AllowHexSpecifier);

				result.Add(new PatchModificationData(offset, originalByte, patchedByte));
			}
			return result;
		}

		private static byte? GetByte(byte[] source, int offset)
		{
			if (source.Length <= offset) return null;
			return source[offset];
		}
	}
}
