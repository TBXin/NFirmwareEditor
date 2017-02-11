using System;
using System.Drawing;
using System.Windows.Forms;
using NCore.UI;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Windows
{
	internal partial class ResizeImageWindow : EditorDialogWindow
	{
		public ResizeImageWindow()
		{
			InitializeComponent();
			NewWidthUpDown.Maximum = NewHeightUpDown.Maximum = Consts.MaximumImageWidthAndHeight;

			Load += (s, e) => { NewWidthUpDown.Select(); };
			NewWidthUpDown.Enter += UpDownEnter;
			NewHeightUpDown.Enter += UpDownEnter;
		}

		private void UpDownEnter(object sender, EventArgs eventArgs)
		{
			var upDown = sender as NumericUpDown;
			if (upDown == null) return;

			upDown.Select(0, upDown.Text.Length);
		}

		public ResizeImageWindow(int currentWidth, int currentHeight) : this()
		{
			CurrentWidthUpDown.Value = NewWidthUpDown.Value = Math.Max(currentWidth, 1);
			CurrentHeightUpDown.Value = NewHeightUpDown.Value = Math.Max(currentHeight, 1);
		}

		public Size NewSize
		{
			get { return new Size((int)NewWidthUpDown.Value, (int)NewHeightUpDown.Value); }
		}
	}
}
