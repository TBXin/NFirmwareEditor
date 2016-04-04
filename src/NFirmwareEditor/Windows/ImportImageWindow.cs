using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;
using NFirmwareEditor.UI;

namespace NFirmwareEditor.Windows
{
	internal partial class ImportImageWindow : EditorDialogWindow
	{
		private readonly IDictionary<int, bool[,]> m_originalImportedImages = new Dictionary<int, bool[,]>();
		private readonly IDictionary<int, bool[,]> m_croppedImportedImages = new Dictionary<int, bool[,]>();

		public ImportImageWindow()
		{
			InitializeComponent();
			Icon = Paths.ApplicationIcon;

			ResizeCheckBox.CheckedChanged += ResizeCheckBox_CheckedChanged;
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

			for (var i = 0; i < originalImages.Count; i++)
			{
				var originalImage = originalImages[i];
				var importedImage = importedImages[i];
				var croppedImportedImage = FirmwareImageProcessor.PasteImage(originalImage, importedImage);

				m_originalImportedImages[i] = importedImage;
				m_croppedImportedImages[i] = croppedImportedImage;

				LeftLayoutPanel.Controls.Add(CreateGrid(originalImage));
				RightLayoutPanel.Controls.Add(CreateGrid(croppedImportedImage));
			}

			BeforeLabel.Text = string.Format("Before:\nUsing {0} of {1} selected images.", originalImages.Count, originalsImageCount);
			AfterLabel.Text = string.Format("After:\nUsing {0} of {1} importing images.", importedImages.Count, importedImageCount);
		}

		public IEnumerable<bool[,]> GetImportedImages()
		{
			switch (ResizeCheckBox.Checked)
			{
				case true: return m_originalImportedImages.Values;
				case false: return m_croppedImportedImages.Values;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		private void ResizeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			var resizeOriginalImages = ResizeCheckBox.Checked;
			var pixelGrids = RightLayoutPanel.Controls.OfType<PixelGrid>().ToList();

			for (var i = 0; i < pixelGrids.Count; i++)
			{
				pixelGrids[i].Data = resizeOriginalImages ? m_originalImportedImages[i] : m_croppedImportedImages[i];
			}
		}

		private PixelGrid CreateGrid(bool[,] imageData)
		{
			var imageSize = FirmwareImageProcessor.GetImageSize(imageData);
			var blockSize = imageSize.Height > 64 ? 1 : 2;
			return new PixelGrid
			{
				Width = 132,
				Height = 132,
				Margin = new Padding(2),
				BlockSize = blockSize,
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
