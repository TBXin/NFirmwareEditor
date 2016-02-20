using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NFirmwareEditor.UI
{
	sealed class BorderedPanel : ScrollableControl
	{
		private RectangleF m_clientRect;

		private bool m_borderLeft;
		private bool m_borderTop;
		private bool m_borderRight;
		private bool m_borderBottom;

		private Color m_borderColor = Color.FromArgb(185, 185, 185);
		private float m_borderWidth = 1.0f;

		[Category("Borders")]
		public Color BorderColor
		{
			get { return m_borderColor; }
			set
			{
				m_borderColor = value;
				Invalidate();
			}
		}

		[Category("Borders")]
		public float BorderWidth
		{
			get { return m_borderWidth; }
			set
			{
				m_borderWidth = value;
				Invalidate();
			}
		}

		[Category("Borders")]
		public bool BorderLeft
		{
			get { return m_borderLeft; }
			set
			{
				m_borderLeft = value;
				Invalidate();
			}
		}

		[Category("Borders")]
		public bool BorderRight
		{
			get { return m_borderRight; }
			set
			{
				m_borderRight = value;
				Invalidate();
			}
		}

		[Category("Borders")]
		public bool BorderTop
		{
			get { return m_borderTop; }
			set
			{
				m_borderTop = value;
				Invalidate();
			}
		}

		[Category("Borders")]
		public bool BorderBottom
		{
			get { return m_borderBottom; }
			set
			{
				m_borderBottom = value;
				Invalidate();
			}
		}

		public BorderedPanel()
		{
			Size = new Size(150, 150);

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.UserPaint, true);
			BackColor = Color.Transparent;
		}

		private void GetClientRect()
		{
			m_clientRect = new RectangleF(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		}

		private void CalculatePaddings()
		{
			Padding = m_borderLeft
				? new Padding((int)m_borderWidth, Padding.Top, Padding.Right, Padding.Bottom)
				: new Padding(0, Padding.Top, Padding.Right, Padding.Bottom);
			Padding = m_borderTop
				? new Padding(Padding.Left, (int)m_borderWidth, Padding.Right, Padding.Bottom)
				: new Padding(Padding.Left, 0, Padding.Right, Padding.Bottom);
			Padding = m_borderRight
				? new Padding(Padding.Left, Padding.Top, (int)m_borderWidth, Padding.Bottom)
				: new Padding(Padding.Left, Padding.Top, 0, Padding.Bottom);
			Padding = m_borderBottom
				? new Padding(Padding.Left, Padding.Top, Padding.Right, (int)m_borderWidth)
				: new Padding(Padding.Left, Padding.Top, Padding.Right, 0);
		}

		private void DrawBorders(Graphics g)
		{
			CalculatePaddings();
			using (var bPen = new Pen(m_borderColor, m_borderWidth))
			{
				if (m_borderLeft)
				{
					g.DrawLine(bPen, m_clientRect.X, m_clientRect.Y, m_clientRect.X, m_clientRect.Height);
				}
				if (m_borderTop)
				{
					g.DrawLine(bPen, m_clientRect.X, m_clientRect.Y, m_clientRect.Width, m_clientRect.Y);
				}
				if (m_borderRight)
				{
					g.DrawLine(bPen, m_clientRect.Width, m_clientRect.Y, m_clientRect.Width, m_clientRect.Height);
				}
				if (m_borderBottom)
				{
					g.DrawLine(bPen, m_clientRect.Width, m_clientRect.Height, m_clientRect.X, m_clientRect.Height);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			GetClientRect();
			DrawBorders(e.Graphics);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			Invalidate();
		}
	}
}
