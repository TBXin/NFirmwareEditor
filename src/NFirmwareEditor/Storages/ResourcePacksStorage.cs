using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NCore;
using NCore.Serialization;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Storages
{
	internal class ResourcePacksStorage : IFileStorage<ResourcePack>
	{
		private const char TrueChar = 'X';
		private const char FalseChar = '.';

		#region Implementation of IStorage
		public void Initialize()
		{
			var initEx = Safe.Execute(() => NFEPaths.EnsureDirectoryExists(NFEPaths.ResourcePackDirectory));
			if (initEx == null) return;

			Trace.Warn(initEx, "An error occured during creating resource packs directory '{0}'.", NFEPaths.ResourcePackDirectory);
		}
		#endregion

		#region Implementation of IFileStorage<out ResourcePack>
		public ResourcePack TryLoad(string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			try
			{
				ResourcePack result;
				using (var fs = File.OpenRead(filePath))
				{
					result = Serializer.Read<ResourcePack>(fs);
				}
				if (result == null) return null;

				result.Description = result.Description.SplitLines();
				result.SuitableDefinitions = result
					.Definition
					.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(x => x.Trim())
					.ToArray();

				result.FilePath = filePath;
				result.FileName = Path.GetFileName(filePath);
				if (result.Images == null)
				{
					Trace.Warn("Resource pack '{0}' does not contains any images.", filePath);
					return null;
				}

				result.Images.ForEach(image =>
				{
					image.Index = image.IndexString.HexStringToInt();
					image.Data = ReadImageFromAsciiString(image.Width, image.Height, image.DataString);
				});
				return result;
			}
			catch (Exception ex)
			{
				Trace.Warn(ex, "An error occured during reading resource pack file '{0}'.", filePath);
				return null;
			}
		}

		public IEnumerable<ResourcePack> LoadAll()
		{
			var result = new List<ResourcePack>();
			var files = Directory.GetFiles(NFEPaths.ResourcePackDirectory, Consts.ResourcePackFileExtension, SearchOption.AllDirectories);
			foreach (var filePath in files)
			{
				var resourcePack = TryLoad(filePath);
				if (resourcePack != null) result.Add(resourcePack);
			}
			return result;
		}
		#endregion

		public void Save([NotNull] string filePath, [NotNull] ResourcePack resourcePack)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");
			if (resourcePack == null) throw new ArgumentNullException("resourcePack");
			if (resourcePack.Images == null || resourcePack.Images.Count == 0) return;

			try
			{
				resourcePack.Images.ForEach(image =>
				{
					image.IndexString = image.Index.ToString("X2");
					image.DataString = WriteImageToAsciiString(image.Width, image.Height, image.Data);
				});

				using (var fs = File.Create(filePath))
				{
					Serializer.Write(resourcePack, fs);
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to export resource pack.\n" + ex.Message);
			}
		}

		internal static bool[,] ReadImageFromAsciiString(int width, int height, string text)
		{
			var result = new bool[width, height];
			if (string.IsNullOrEmpty(text)) return result;

			var rows = text.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			for (var row = 0; row < rows.Length; row++)
			{
				for (var col = 0; col < rows[row].Length; col++)
				{
					result[col, row] = IsTrue(rows[row][col]);
				}
			}
			return result;
		}

		internal static string WriteImageToAsciiString(int width, int height, bool[,] imageData)
		{
			var sb = new StringBuilder();
			sb.AppendLine();
			for (var row = 0; row < height; row++)
			{
				for (var col = 0; col < width; col++)
				{
					sb.Append(imageData[col, row] ? TrueChar : FalseChar);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		private static bool IsTrue(char c)
		{
			return c.Equals(TrueChar) || c.Equals('1');
		}
	}
}
