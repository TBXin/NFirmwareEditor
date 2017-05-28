using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
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
		private Size? m_imageSize;
		private Font m_additionalTextFont;
		private Color m_additionalTextColor;
		private bool m_useCompatibleTextRendering;
		private bool m_drawBorders;
		private Color m_primaryTextDisabledColor;

		public ExtendedButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, false);
			UpdateStyles();

			m_drawBorders = true;
			m_useCompatibleTextRendering = false;
			m_image = null;
			m_imageSize = null;
			m_additionalText = null;
			m_additionalTextFont = null;
			m_additionalTextColor = SystemColors.ControlText;

			TextChanged += (s, e) => Invalidate();
		}

		[DefaultValue(true)]
		public bool DrawBorders
		{
			get { return m_drawBorders; }
			set
			{
				m_drawBorders = value;
				Cursor = m_drawBorders ? Cursors.Default : Cursors.Hand;
				Invalidate();
			}
		}

		[DefaultValue(false)]
		public bool UseCompatibleTextRendering
		{
			get { return m_useCompatibleTextRendering; }
			set
			{
				m_useCompatibleTextRendering = value;
				Invalidate();
			}
		}

		[DefaultValue(null)]
		public string AdditionalText
		{
			get { return m_additionalText; }
			set
			{
				m_additionalText = value;
				Invalidate();
			}
		}

		[DefaultValue(null)]
		public Font AdditionalTextFont
		{
			get { return m_additionalTextFont; }
			set
			{
				m_additionalTextFont = value;
				Invalidate();
			}
		}

		[DefaultValue(typeof(Color), "ControlText")]
		public Color AdditionalTextColor
		{
			get { return m_additionalTextColor; }
			set
			{
				m_additionalTextColor = value;
				Invalidate();
			}
		}

		[DefaultValue(typeof(Color), "85; 85; 85")]
		public Color PrimaryTextDisabledColor
		{
			get { return m_primaryTextDisabledColor; }
			set
			{
				m_primaryTextDisabledColor = value;
				Invalidate();
			}
		}

		[DefaultValue(null)]
		public Image Image
		{
			get { return m_image; }
			set
			{
				m_image = value;
				Invalidate();
			}
		}

		[DefaultValue(null)]
		public Size? ImageSize
		{
			get { return m_imageSize; }
			set
			{
				m_imageSize = value;
				Invalidate();
			}
		}

		[DefaultValue(null)]
		public Color? MouserOverPrimaryTextColor { get; set; }

		[DefaultValue(null)]
		public Color? MouserDownPrimaryTextColor { get; set; }

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
			if (m_image == null) return;
			gfx.DrawImage(m_image, rect);
		}

		private void DrawBorder(Graphics gfx, Rectangle rect)
		{
			if (!m_drawBorders) return;
			if (m_isMouseDown)
			{
				gfx.FillRectangle(new SolidBrush(Color.FromArgb(25, SystemColors.Highlight)), rect);
			}
			gfx.DrawRectangle(m_isMouseOver ? SystemPens.Highlight : SystemPens.ActiveBorder, rect);
		}

		private void DrawText(Graphics gfx, Rectangle rect)
		{
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			var primaryTextColor = ForeColor;
			if (m_isMouseOver && MouserOverPrimaryTextColor != null) primaryTextColor = MouserOverPrimaryTextColor.Value;
			if (m_isMouseDown && MouserDownPrimaryTextColor != null) primaryTextColor = MouserDownPrimaryTextColor.Value;
			if (!Enabled) primaryTextColor = PrimaryTextDisabledColor;

			if (string.IsNullOrEmpty(m_additionalText))
			{
				DrawString(gfx, rect, Text, Font, primaryTextColor);
			}
			else
			{
				var layoutArea = new SizeF(rect.Width, 0);
				var additionalTextFont = m_additionalTextFont ?? Font;

				var primaryTextSize = gfx.MeasureString(Text, Font, layoutArea, s_headerStringFormat);
				var additionalTextSize = gfx.MeasureString(m_additionalText, additionalTextFont, layoutArea, s_headerStringFormat);
				var contentHeight = (int)(primaryTextSize.Height + additionalTextSize.Height);

				var primaryTextRect = new Rectangle
				{
					X = rect.X,
					Y = rect.Y + (rect.Height - contentHeight) / 2,
					Width = rect.Width,
					Height = (int)primaryTextSize.Height
				};

				var additionalTextRect = new Rectangle
				{
					X = rect.X,
					Y = rect.Y + primaryTextRect.Y + primaryTextRect.Height,
					Width = rect.Width,
					Height = (int)additionalTextSize.Height
				};

				DrawString(gfx, primaryTextRect, Text, Font, primaryTextColor);
				DrawString(gfx, additionalTextRect, m_additionalText, additionalTextFont, m_additionalTextColor);
			}
		}

		private void DrawString(Graphics gfx, Rectangle rect, string text, Font font, Color color)
		{
			if (UseCompatibleTextRendering)
			{
				gfx.DrawString(text, font, new SolidBrush(color), rect, s_headerStringFormat);
			}
			else
			{
				try
				{
					TextRenderer.DrawText(gfx, text, font, rect, color, s_headerFormatFlags);
				}
				catch
				{
					gfx.DrawString(text, font, new SolidBrush(color), rect, s_headerStringFormat);
				}
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
			if (m_image == null) return Rectangle.Empty;

			var imageSize = m_imageSize ?? m_image.Size;
			return new Rectangle(ImageOffset, clientRect.Height / 2 - imageSize.Height / 2 + 1, imageSize.Width, imageSize.Height);
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
