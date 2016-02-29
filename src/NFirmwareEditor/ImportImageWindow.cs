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

		public ImportImageWindow(IEnumerable<bool[,]> originalImages, IEnumerable<bool[,]> importedImages) : this()
		{
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
