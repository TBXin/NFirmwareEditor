using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal static class ImageExportManager
	{
		public static List<ExportedImage> Import(string path)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");

			List<ExportedImage> result = null;
			try
			{
				var serializer = new XmlSerializer(typeof(List<ExportedImage>));
				using (var fs = File.Open(path, FileMode.Open))
				{
					result = serializer.Deserialize(fs) as List<ExportedImage>;
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to import images.\n" + ex.Message);
			}
			return result ?? new List<ExportedImage>();
		}

		public static void Export(string path, List<ExportedImage> images)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
			if (images == null) throw new ArgumentNullException("images");

			try
			{
				var serializer = new XmlSerializer(typeof(List<ExportedImage>));
				using (var fs = File.Open(path, FileMode.Create))
				{
					serializer.Serialize(fs, images);
				}
			}
			catch (Exception ex)
			{
				InfoBox.Show("Unable to export images.\n" + ex.Message);
			}
		}
	}
}
