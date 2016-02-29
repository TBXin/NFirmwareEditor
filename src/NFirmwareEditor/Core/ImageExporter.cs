using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NFirmwareEditor.Firmware;

namespace NFirmwareEditor.Core
{
	internal static class ImageExporter
	{
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
