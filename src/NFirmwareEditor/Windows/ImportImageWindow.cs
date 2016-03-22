using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows
{
	public partial class ImportImageWindow : Form
	{
		private readonly IList<bool[,]> m_originalImages;
		private readonly IList<bool[,]> m_importedImages;
		private readonly IList<PixelGrid> m_rightPanelGrids = new List<PixelGrid>();

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
			if (originalImages.Count != importedImages.Count)
			{
				throw new InvalidOperationException("Source and imported images count does not match.");
			}

			m_originalImages = originalImages;
			m_importedImages = importedImages;

			for (var i = 0; i < originalImages.Count; i++)
			{
				var originalImage = originalImages[i];
				var importedImage = FirmwareImageProcessor.PasteImage(originalImage, importedImages[i]);

				LeftLayoutPanel.Controls.Add(CreateGrid(originalImage));
				var rightGrid = CreateGrid(importedImage);
				m_rightPanelGrids.Add(rightGrid);
				RightLayoutPanel.Controls.Add(rightGrid);
			}

			BeforeLabel.Text = string.Format("Before:\nUsing {0} of {1} selected images.", originalImages.Count, originalsImageCount);
			AfterLabel.Text = string.Format("After:\nUsing {0} of {1} importing images.", importedImages.Count, importedImageCount);
		}

		private void ResizingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			for (var i = 0; i < m_rightPanelGrids.Count; i++)
			{
				var grid = m_rightPanelGrids[i];
				var originalImage = m_originalImages[i];
				var importedImage = m_importedImages[i];

				grid.Data = ResizingCheckBox.Checked
					? importedImage
					: FirmwareImageProcessor.PasteImage(originalImage, importedImage);
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
