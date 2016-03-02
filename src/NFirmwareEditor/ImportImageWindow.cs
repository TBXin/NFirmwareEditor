using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.UI;

namespace NFirmwareEditor
{
	public partial class ImportImageWindow : Form
	{
		public ImportImageWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;
		}

		public ImportImageWindow
		(
			IList<bool[,]> originalImages,
			IList<bool[,]> importedImages,
			int originalsImageCount,
			int importedImageCount
		) : this()
		{
			if (originalImages.Count != importedImages.Count) throw new InvalidOperationException("Source and imported images count does not match.");

			BeforeLabel.Text = string.Format("Before:\nUsing {0} of the {1} selected images.", originalImages.Count, originalsImageCount);
			AfterLabel.Text = string.Format("After:\nUsing {0} of the {1} importing images.", importedImages.Count, importedImageCount);

			for (var i = 0; i < originalImages.Count; i++)
			{
				var originalImage = originalImages[i];
				var importedImage = FirmwareImageProcessor.PasteImage(originalImage, importedImages[i]);

				LeftLayoutPanel.Controls.Add(CreateGrid(originalImage));
				RightLayoutPanel.Controls.Add(CreateGrid(importedImage));
			}
		}

		private PixelGrid CreateGrid(bool[,] imageData)
		{
			return new PixelGrid
			{
				Width = 205,
				Height = 80,
				BlockSize = 2,
				ShowGrid = false,
				Data = imageData,
				BackColor = Color.Black,
				BlockInnerBorderPen = Pens.Transparent,
				BlockOuterBorderPen = Pens.Transparent,
				ActiveBlockBrush = Brushes.White,
				InactiveBlockBrush = Brushes.Black
			};
		}
	}
}
