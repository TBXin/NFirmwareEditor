using System;
using System.Drawing;
using System.Windows.Forms;

namespace NCore.UI
{
	public class DrawingSurface : ScrollableControl
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

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}
	}
}
