using System.Drawing;
using System.Windows.Forms;

namespace NFirmwareEditor.Windows
{
	public partial class ResizeImageWindow : Form
	{
		public ResizeImageWindow()
		{
			InitializeComponent();
		}

		public ResizeImageWindow(int currentWidth, int currentHeight) : this()
		{
			CurrentWidthUpDown.Value = NewWidthUpDown.Value = currentWidth;
			CurrentHeightUpDown.Value = NewHeightUpDown.Value = currentHeight;
		}

		public Size NewSize
		{
			get { return new Size((int)NewWidthUpDown.Value, (int)NewHeightUpDown.Value); }
		}
	}
}
