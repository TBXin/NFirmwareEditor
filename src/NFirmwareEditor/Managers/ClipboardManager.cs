using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;
using NFirmwareEditor.Core;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Managers
{
	internal class ClipboardManager
	{
		public void SetData([NotNull] List<bool[,]> images)
		{
			if (images == null) throw new ArgumentNullException("images");

			var data = new List<ExportedImage>();
			foreach (var image in images)
			{
				var size = FirmwareImageProcessor.GetImageSize(image);
				data.Add(new ExportedImage(0, size, image)
				{
					DataString = ResourcePackManager.WriteImageToAsciiString(size.Width, size.Height, image)
				});
			}
			var buffer = Serializer.Write(data);
			Clipboard.SetText(buffer);
		}

		[NotNull]
		public List<bool[,]> GetData()
		{
			var images = new List<bool[,]>();
			try
			{
				var buffer = Clipboard.GetText();
				var data = Serializer.Read<List<ExportedImage>>(buffer);
				if (data == null) return images;

				foreach (var exportedImage in data)
				{
					images.Add(ResourcePackManager.ReadImageFromAsciiString(exportedImage.Width, exportedImage.Height, exportedImage.DataString));
				}
				return images;
			}
			catch
			{
				return images;
			}
		}
	}
}
