using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace NFirmwareEditor.Managers
{
	internal class ClipboardManager
	{
		public void SetData([NotNull] List<bool[,]> images)
		{
			if (images == null) throw new ArgumentNullException("images");
			Clipboard.SetDataObject(images);
		}

		[NotNull]
		public List<bool[,]> GetData()
		{
			var dataObject = Clipboard.GetDataObject();
			var bufferedImages = dataObject != null
				? dataObject.GetData(typeof(List<bool[,]>)) as List<bool[,]>
				: null;
			return bufferedImages ?? new List<bool[,]>();
		}
	}
}
