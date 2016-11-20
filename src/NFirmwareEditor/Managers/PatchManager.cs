using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JetBrains.Annotations;
using NCore;
using NCore.Serialization;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;
using NLog;

namespace NFirmwareEditor.Managers
{
	internal class PatchManager
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		public static string GetPatchDirectoryPath(FirmwareDefinition definition)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			return Path.Combine(NFEPaths.PatchDirectory, definition.Name);
		}

		public static string GetPatchFilePath([NotNull] FirmwareDefinition definition, [NotNull] string fileName)
		{
			if (definition == null) throw new ArgumentNullException("definition");
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

			return Path.Combine(NFEPaths.PatchDirectory, definition.Name, fileName);
		}

		public void InitializeStorage([NotNull] IEnumerable<FirmwareDefinition> definitions)
		{
			if (definitions == null) throw new ArgumentNullException("definitions");

			var patchDirEx = Safe.Execute(() => NFEPaths.EnsureDirectoryExists(NFEPaths.PatchDirectory));
			if (patchDirEx != null)
			{
				s_logger.Warn(patchDirEx, "An error occured during creating primary pathes directory '{0}'.", NFEPaths.PatchDirectory);
				return;
			}

			foreach (var definition in definitions)
			{
				var definitionPatchDir = Path.Combine(NFEPaths.PatchDirectory, definition.Name);
				var ex = Safe.Execute(() => NFEPaths.EnsureDirectoryExists(definitionPatchDir));
				if (ex != null)
				{
					s_logger.Warn(ex, "An error occured during creating patches directory '{0}'.", definitionPatchDir);
				}
			}
		}

		public IEnumerable<Patch> LoadPatchesForFirmware([NotNull] FirmwareDefinition definition)
		{
			if (definition == null) throw new ArgumentNullException("definition");

			var result = new List<Patch>();
			var pathesLocation = Path.Combine(NFEPaths.PatchDirectory, definition.Name);
			if (!Directory.Exists(pathesLocation)) return result;

			var files = Directory.GetFiles(pathesLocation, Consts.PatchFileExtension, SearchOption.AllDirectories);
			foreach (var file in files)
			{
				var serializer = new XmlSerializer(typeof(Patch));
				try
				{
					Patch patch;
					using (var fs = File.Open(file, FileMode.Open))
					{
						patch = serializer.Deserialize(fs) as Patch;
					}

					if (patch == null) continue;
					if (!string.Equals(patch.Definition, definition.Name)) continue;

					patch.Data = ParseDiff(patch.DataString);
					patch.FilePath = file;
					patch.FileName = Path.GetFileName(file);
					patch.Description = patch.Description.SplitLines();
					patch.Sha = GitHubApi.GetGitSha(file);
					result.Add(patch);
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex, "An error occurred during loading patch: " + file);
				}
			}
			return result;
		}

		public void SavePatch(string filePath, [NotNull] Patch patch)
		{
			if(string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");
			if (patch == null) throw new ArgumentNullException("patch");

			using (var fs = File.Open(filePath, FileMode.Create))
			{
				Serializer.Write(patch, fs);
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

			patch.Data.Where(data => !data.IgnoreOriginalValue).ForEach(data => firmware.BodyStream.WriteByte(data.Offset, data.OriginalValue));
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
			foreach (var line in lines)
			{
				// Remove all spaces and tabs.
				var patchLine = line.Trim().Replace("\t", string.Empty);
				if (string.IsNullOrEmpty(patchLine)) continue;
				if (patchLine.StartsWith("#")) continue;

				// Look for inline comment.
				var lineCommentStartIndex = patchLine.IndexOf(';');
				if (lineCommentStartIndex != -1) patchLine = patchLine.Substring(0, lineCommentStartIndex);

				// Split offset and data.
				var offsetAndData = patchLine.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
				if (offsetAndData.Length != 2)
				{
					throw new InvalidDataException(string.Format("Invalid patch data '{0}'. Data should have format 'offset: oldByte - newByte'.", patchLine));
				}

				var offset = long.Parse(offsetAndData[0], NumberStyles.AllowHexSpecifier);
				var data = offsetAndData[1];

				// Split data to the old / new values.
				var originalAndPatchedBytes = data.Trim().Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (originalAndPatchedBytes.Length != 2)
				{
					throw new InvalidDataException(string.Format("Invalid patch data '{0}'. Data should have format 'oldByte - newByte'.", data));
				}

				PatchModificationData patchModificationData;
				var patchedByte = ParseByte(originalAndPatchedBytes[1]);
				if (string.Equals("*", originalAndPatchedBytes[0], StringComparison.OrdinalIgnoreCase))
				{
					patchModificationData = new PatchModificationData(offset, patchedByte);
				}
				else
				{
					var originalByte = ParseByte(originalAndPatchedBytes[0]);
					patchModificationData = new PatchModificationData(offset, originalByte, patchedByte);
				}
				result.Add(patchModificationData);
			}
			return result;
		}

		private static bool ValidatePatchApplyingCompatibility([NotNull] Patch patch, [NotNull] Firmware firmware)
		{
			return patch.Data.Where(data => !data.IgnoreOriginalValue).All(data => firmware.BodyStream.ReadByte(data.Offset) == data.OriginalValue);
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
