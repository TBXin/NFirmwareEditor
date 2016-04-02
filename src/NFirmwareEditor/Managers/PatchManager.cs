using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;
using NFirmware;
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
						if (patch != null)
						{
							patch.Data = ParseDiff(patch.DataString);
							result.Add(patch);
						}
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

		public bool IsPatchApplied([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			if (patch == null) throw new ArgumentNullException("patch");
			if (firmware == null) throw new ArgumentNullException("firmware");

			return patch.Data.All(data => firmware.BodyStream.ReadByte(data.Offset) == data.PatchedValue);
		}

		public bool IsPatchCompatible([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			if (patch == null) throw new ArgumentNullException("patch");
			if (firmware == null) throw new ArgumentNullException("firmware");

			return ValidatePatchApplyingCompatibility(patch, firmware);
		}

		public List<Patch> CheckConflicts(Patch patch, IEnumerable<Patch> otherPathes)
		{
			var result = new List<Patch>();
			foreach (var otherPatch in otherPathes.Where(x=> x != patch))
			{
				foreach (var otherPatchData in otherPatch.Data)
				{
					if (patch.Data.Any(x => x.Offset == otherPatchData.Offset && x.PatchedValue != otherPatchData.PatchedValue))
					{
						result.Add(otherPatch);
						break;
					}
				}
			}
			return result;
		}

		public bool ApplyPatch([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			if (patch == null) throw new ArgumentNullException("patch");
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (!ValidatePatchApplyingCompatibility(patch, firmware)) return false;

			patch.Data.ForEach(data => firmware.BodyStream.WriteByte(data.Offset, data.PatchedValue));
			return true;
		}

		public bool RollbackPatch([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			if (patch == null) throw new ArgumentNullException("patch");
			if (firmware == null) throw new ArgumentNullException("firmware");
			if (!ValidatePatchRollbackCompatibility(patch, firmware)) return false;

			patch.Data.ForEach(data => firmware.BodyStream.WriteByte(data.Offset, data.OriginalValue));
			return true;
		}

		public BulkPatchResult BulkOperation(IEnumerable<Patch> patchList, Func<Patch, bool> operation)
		{
			var proceededPatches = new List<Patch>();
			var conflictedPatches = new List<Patch>();
			foreach (var patch in patchList)
			{
				if (operation(patch))
				{
					proceededPatches.Add(patch);
				}
				else
				{
					conflictedPatches.Add(patch);
				}
			}
			return new BulkPatchResult(proceededPatches, conflictedPatches);
		}

		private static IEnumerable<PatchModificationData> ParseDiff(string dataString)
		{
			if (string.IsNullOrEmpty(dataString)) return new List<PatchModificationData>();

			var lines = dataString.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var result = new List<PatchModificationData>();
			foreach (var line in lines.Where(x => string.IsNullOrEmpty(x) || !x.StartsWith("#")).Select(x => x.Replace(" ", string.Empty)))
			{
				var offsetAndData = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
				if (offsetAndData.Length != 2) throw new InvalidDataException();

				var offset = long.Parse(offsetAndData[0], NumberStyles.AllowHexSpecifier);
				var data = offsetAndData[1];
				if (data.IndexOf(';') != -1) data = data.Substring(0, data.IndexOf(';'));

				var originalAndPatchedBytes = data.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
				if (originalAndPatchedBytes.Length != 2) throw new InvalidDataException();

				var originalByte = ParseByte(originalAndPatchedBytes[0]);
				var patchedByte = ParseByte(originalAndPatchedBytes[1]);

				result.Add(new PatchModificationData(offset, originalByte, patchedByte));
			}
			return result;
		}

		private static bool ValidatePatchApplyingCompatibility([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			return patch.Data.All(data => firmware.BodyStream.ReadByte(data.Offset) == data.OriginalValue);
		}

		private static bool ValidatePatchRollbackCompatibility([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			return patch.Data.All(data => firmware.BodyStream.ReadByte(data.Offset) == data.PatchedValue);
		}

		private static byte? GetByte(byte[] source, int offset)
		{
			if (source.Length <= offset) return null;
			return source[offset];
		}

		private static byte? ParseByte([NotNull] string byteString)
		{
			if (string.IsNullOrEmpty(byteString)) throw new ArgumentNullException("byteString");

			return byteString.Equals("null", StringComparison.OrdinalIgnoreCase)
				? (byte?)null
				: byte.Parse(byteString, NumberStyles.AllowHexSpecifier);
		}
	}
}
