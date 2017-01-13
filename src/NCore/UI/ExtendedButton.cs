using System;
using System.Drawing;
using System.Windows.Forms;

namespace NCore.UI
{
	public sealed class ExtendedButton : Control
	{
		private const int ImageOffset = 5;

		private static readonly TextFormatFlags s_headerFormatFlags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
		private static readonly StringFormat s_headerStringFormat = new StringFormat
		{
			Alignment = StringAlignment.Near,
			LineAlignment = StringAlignment.Center
		};

		private bool m_isMouseOver;
		private bool m_isMouseDown;
		private string m_additionalText;
		private Image m_image;

		public ExtendedButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, false);
			UpdateStyles();

			TextChanged += (s, e) => Invalidate();
		}

		public string AdditionalText
		{
			get { return m_additionalText; }
			set
			{
				m_additionalText = value;
				Invalidate();
			}
		}

		public Image Image
		{
			get { return m_image; }
			set
			{
				m_image = value;
				Invalidate();
			}
		}

		public void PerformClick()
		{
			OnClick(EventArgs.Empty);
		}

		#region Overrides of Control
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			m_isMouseOver = true;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			m_isMouseOver = false;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			m_isMouseDown = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			m_isMouseDown = false;	
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var clientRect = GetClientArea();
			var imageRect = GetImageRect(clientRect);
			var textRect = GetTextRect(clientRect, imageRect);

			DrawImage(e.Graphics, imageRect);
			DrawText(e.Graphics, textRect);
			DrawBorder(e.Graphics, clientRect);
		}
		#endregion

		private void DrawImage(Graphics gfx, Rectangle rect)
		{
			if (Image == null) return;
			gfx.DrawImage(Image, rect);
		}

		private void DrawBorder(Graphics gfx, Rectangle rect)
		{
			if (m_isMouseDown)
			{
				gfx.FillRectangle(new SolidBrush(Color.FromArgb(25, SystemColors.Highlight)), rect);
			}
			gfx.DrawRectangle(m_isMouseOver ? SystemPens.Highlight : SystemPens.ActiveBorder, rect);
		}

		private void DrawText(Graphics gfx, Rectangle rect)
		{
			if (string.IsNullOrEmpty(AdditionalText))
			{
				DrawString(gfx, rect, Text, true);
			}
			else
			{
				var textRect = new Rectangle
				{
					X = rect.X,
					Y = rect.Y,
					Width = rect.Width,
					Height = rect.Height / 2
				};

				var additionalTextRect = new Rectangle
				{
					X = rect.X,
					Y = rect.Y + rect.Height / 2,
					Width = rect.Width,
					Height = rect.Height / 2
				};

				DrawString(gfx, textRect, Text, true);
				DrawString(gfx, additionalTextRect, AdditionalText);
			}
		}

		private void DrawString(Graphics gfx, Rectangle rect, string text, bool mainBold = false)
		{
			var font = mainBold ? new Font(Font, FontStyle.Bold) : Font;

			try
			{
				TextRenderer.DrawText(gfx, text, font, rect, ForeColor, s_headerFormatFlags);
			}
			catch
			{
				gfx.DrawString(text, font, new SolidBrush(ForeColor), rect, s_headerStringFormat);
			}
		}

		private Rectangle GetClientArea()
		{
			return new Rectangle
			{
				X = ClientRectangle.X,
				Y = ClientRectangle.Y,
				Width = ClientRectangle.Width - 1,
				Height = ClientRectangle.Height - 1
			};
		}

		private Rectangle GetImageRect(Rectangle clientRect)
		{
			return Image == null
				? Rectangle.Empty
				: new Rectangle(ImageOffset, clientRect.Height / 2 - Image.Height / 2 + 1, Image.Width, Image.Height);
		}

		private Rectangle GetTextRect(Rectangle clientRect, Rectangle imageRect)
		{
			var left = imageRect.Right + ImageOffset;
			return new Rectangle
			{
				X = imageRect.Right + ImageOffset,
				Y = 0,
				Width = clientRect.Width - left,
				Height = clientRect.Height
			};
		}
	}
}
