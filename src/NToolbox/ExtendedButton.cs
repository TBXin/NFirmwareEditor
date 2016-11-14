using System;
using System.Drawing;
using System.Windows.Forms;

namespace NToolbox
{
	internal class ExtendedButton : Control
	{
		private static readonly TextFormatFlags s_headerFormatFlags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
		private static readonly StringFormat s_headerStringFormat = new StringFormat
		{
			Alignment = StringAlignment.Near,
			LineAlignment = StringAlignment.Center
		};

		private const int ImageOffset = 5;
		private bool m_isMouseOver;

		public string AdditionalText { get; set; }

		public Image Image { get; set; }

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
			if (m_isMouseOver)
			{
				gfx.DrawRectangle(SystemPens.Highlight, rect);
			}
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
				gfx.DrawString(text, Font, new SolidBrush(ForeColor), rect, s_headerStringFormat);
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
