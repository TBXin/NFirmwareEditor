using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NFirmware;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class ResourcePackManager
	{
		private const char TrueChar = 'X';
		private const char FalseChar = '.';

		[NotNull, ItemNotNull]
		public IEnumerable<ResourcePackFile> LoadAll()
		{
			if (!Directory.Exists(Paths.ResourcePackDirectory)) Directory.CreateDirectory(Paths.ResourcePackDirectory);

			var result = new List<ResourcePackFile>();
			var files = Directory.GetFiles(Paths.ResourcePackDirectory, Consts.ResourcePackFileExtension, SearchOption.AllDirectories);
			foreach (var file in files)
			{
				try
				{
					using (var fs = File.Open(file, FileMode.Open))
					{
						var resourcePack = Serializer.Read<ResourcePackFile>(fs);
						if (resourcePack == null) continue;

						resourcePack.SuitableDefinitions = resourcePack
							.Definition
							.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
							.Select(x => x.Trim())
							.ToArray();
						resourcePack.FilePath = file;
						resourcePack.FileName = Path.GetFileName(file);
						resourcePack.Description = resourcePack.Description.SplitLines();
						result.Add(resourcePack);
					}
				}
				catch
				{
					// Ignore
				}
			}
			return result;
		}

		[CanBeNull]
		public ResourcePack LoadFromFile([NotNull] string path)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");

			ResourcePack result = null;
			try
			{
				using (var fs = File.Open(path, FileMode.Open))
				{
					result = Serializer.Read<ResourcePack>(fs);
					if (result != null)
					{
						result.Description = result.Description.SplitLines();
						if (result.Images != null)
						{
							result.Images.ForEach(image =>
							{
								image.Index = image.IndexString.HexStringToInt();
								image.Data = ReadImageFromAsciiString(image.Width, image.Height, image.DataString);
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to import images.\n" + ex.Message);
			}
			return result;
		}

		public void SaveToFile([NotNull] string path, [NotNull] ResourcePack pack)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
			if (pack == null) throw new ArgumentNullException("pack");
			if (pack.Images == null || pack.Images.Count == 0) return;

			try
			{
				using (var fs = File.Open(path, FileMode.Create))
				{
					pack.Images.ForEach(image =>
					{
						image.IndexString = image.Index.ToString("X2");
						image.DataString = WriteImageToAsciiString(image.Width, image.Height, image.Data);
					});
					Serializer.Write(pack, fs);
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
