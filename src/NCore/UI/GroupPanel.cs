using System;
using System.Drawing;
using System.Windows.Forms;

namespace NCore.UI
{
	public sealed class GroupPanel : GroupBox
	{
		private static readonly TextFormatFlags s_headerFormatFlags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
		private static readonly StringFormat s_headerStringFormat = new StringFormat
		{
			Alignment = StringAlignment.Near,
			LineAlignment = StringAlignment.Center
		};

		private int m_headerHeight = 30;
		private int m_actualHeaderHeight;
		private Color m_borderColor = Color.FromArgb(185, 185, 185);
		private Color m_headerBackgroundColor = Color.White;

		public int HeaderHeight
		{
			get { return m_headerHeight; }
			set { m_headerHeight = value; }
		}

		public Color BorderColor
		{
			get { return m_borderColor; }
			set { m_borderColor = value; }
		}

		public Color HeaderBackColor
		{
			get { return m_headerBackgroundColor; }
			set { m_headerBackgroundColor = value; }
		}

		/*public override Rectangle DisplayRectangle
		{
			get
			{
				return new Rectangle
				(
					Padding.Left,
					Padding.Top + m_actualHeaderHeight,
					Math.Max(Width - Padding.Horizontal, 0),
					Math.Max(Height - m_actualHeaderHeight - Padding.Vertical, 0)
				);
			}
		}*/

		protected override void OnPaint(PaintEventArgs e)
		{
			m_actualHeaderHeight = (int)(m_headerHeight * (e.Graphics.DpiX / 96f));

			var clientRect = GetClientRect();
			var headerRect = GetHedearRect(clientRect);
			var containerRect = GetContainerRect(clientRect);
			var borderPen = new Pen(BorderColor, 1);

			// Draw header
			e.Graphics.FillRectangle(new SolidBrush(HeaderBackColor), headerRect);
			e.Graphics.DrawLine(borderPen, clientRect.X, clientRect.Y + m_actualHeaderHeight - 1, clientRect.Width, clientRect.Y + m_actualHeaderHeight - 1);
			try
			{
				TextRenderer.DrawText(e.Graphics, Text, Font, GetHeaderTextRect(headerRect), ForeColor, s_headerFormatFlags);
			}
			catch
			{
				e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), GetHeaderTextRect(headerRect), s_headerStringFormat);
			}
			// Draw container
			e.Graphics.FillRectangle(new SolidBrush(BackColor), containerRect);

			// Draw borders
			e.Graphics.DrawRectangle(borderPen, clientRect);
		}

		private Rectangle GetClientRect()
		{
			return new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
		}

		private RectangleF GetHedearRect(Rectangle clientRect)
		{
			return new RectangleF(clientRect.X, clientRect.Y, clientRect.Width, m_actualHeaderHeight - 1);
		}

		private Rectangle GetContainerRect(Rectangle clientRect)
		{
			return new Rectangle(clientRect.X, clientRect.Y + m_actualHeaderHeight + 1, clientRect.Width, clientRect.Height - m_actualHeaderHeight - 1);
		}

		private Rectangle GetHeaderTextRect(RectangleF headerRect)
		{
			return new Rectangle((int)(headerRect.X + 3), (int)headerRect.Y, (int)(headerRect.Width - 3), (int)headerRect.Height);
		}
	}
}
