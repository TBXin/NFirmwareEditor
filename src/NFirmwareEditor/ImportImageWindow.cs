using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NFirmwareEditor.Core;
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
			ICollection<bool[,]> originalImages, 
			ICollection<bool[,]> importedImages,
			int originalsImageCount,
			int importedImageCount
		) : this()
		{
			BeforeLabel.Text = string.Format("Before:\nUsing {0} of the {1} selected images.", originalImages.Count, originalsImageCount);
			AfterLabel.Text = string.Format("After:\nUsing {0} of the {1} importing images.", importedImages.Count, importedImageCount);

			foreach (var originalImage in originalImages)
			{
				LeftLayoutPanel.Controls.Add(CreateGrid(originalImage));
			}

			foreach (var importedImage in importedImages)
			{
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
