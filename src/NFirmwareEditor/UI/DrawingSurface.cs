using System.Drawing;
using System.Windows.Forms;

namespace NFirmwareEditor.UI
{
	internal class DrawingSurface : ScrollableControl
	{
		public DrawingSurface()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, false);
			UpdateStyles();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
		}
	}
}
