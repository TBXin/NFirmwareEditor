using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NCore.UI
{
	public class CardPanel : Panel
	{
		private Color m_borderColor = Color.FromArgb(0xF5, 0xF5, 0xF5);
		private Color m_shadowColor = Color.FromArgb(0xA9, 0xA9, 0xA9);
		private int m_cornerRadius = 3;
		private int m_shadowLength = 5;
		private bool m_showBorder = true;

		public CardPanel()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.Selectable, false);
			UpdateStyles();
		}

		[DefaultValue(3)]
		public int CornerRadius
		{
			get { return m_cornerRadius; }
			set
			{
				m_cornerRadius = value;
				Invalidate();
			}
		}

		[DefaultValue(true)]
		public bool ShowBorder
		{
			get { return m_showBorder; }
			set
			{
				m_showBorder = value;
				Invalidate();
			}
		}

		[DefaultValue(typeof(Color), "0xF5F5F5")]
		public Color BorderColor
		{
			get { return m_borderColor; }
			set
			{
				m_borderColor = value;
				Invalidate();
			}
		}

		[DefaultValue(5)]
		public int ShadowLength
		{
			get { return m_shadowLength; }
			set
			{
				m_shadowLength = value;
				Invalidate();
			}
		}

		[DefaultValue(typeof(Color), "0xA9A9A9")]
		public Color ShadowColor
		{
			get { return m_shadowColor; }
			set
			{
				m_shadowColor = value;
				Invalidate();
			}
		}

		public override Rectangle DisplayRectangle
		{
			get
			{
				using (var gfx = Graphics.FromHwnd(IntPtr.Zero))
				{
					return new Rectangle
					(
						Padding.Left + m_shadowLength,
						Padding.Top + 0,
						Math.Max(Width - Padding.Horizontal - m_shadowLength * 2, 0),
						Math.Max(Height - m_shadowLength - Padding.Vertical, 0)
					);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var gfx = e.Graphics;

			var clientRect = GetClientRectangle();
			var shadowRect = GetShadowRectangle(clientRect);

			var shadowPath = GetShadowPath(shadowRect);
			DrawShadow(gfx, shadowPath);

			var borderPath = GenerateRoundedRectangle(clientRect, m_cornerRadius, RectangleEdgeFilter.All);
			FillPath(gfx, new SolidBrush(BackColor), borderPath);

			if (ShowBorder) DrawPath(gfx, new Pen(m_borderColor, 1), borderPath);
		}

		private void DrawShadow(Graphics gfx, GraphicsPath shadowPath)
		{
			using (var shadowBrush = new PathGradientBrush(shadowPath))
			{
				shadowBrush.WrapMode = WrapMode.Clamp;
				var colorBlend = new ColorBlend(3)
				{
					Colors = new[]
					{
						Color.Transparent,
						Color.FromArgb(180, m_shadowColor),
						Color.FromArgb(180, m_shadowColor)
					},
					Positions = new[] { 0f, .4f, 1f }
				};

				shadowBrush.InterpolationColors = colorBlend;
				gfx.FillPath(shadowBrush, shadowPath);
			}
		}

		private RectangleF GetClientRectangle()
		{
			return new RectangleF
			(
				ClientRectangle.X + m_shadowLength,
				ClientRectangle.Y,
				ClientRectangle.Width - m_shadowLength * 2 - 1,
				ClientRectangle.Height - m_shadowLength - 1
			);
		}

		private RectangleF GetShadowRectangle(RectangleF clientRect)
		{
			return new RectangleF
			(
				clientRect.X - m_shadowLength,
				clientRect.Y,
				clientRect.Width + m_shadowLength * 2,
				clientRect.Height + m_shadowLength - 2
			);
		}

		private GraphicsPath GetShadowPath(RectangleF rect)
		{
			return GenerateRoundedRectangle(rect, m_cornerRadius * 2, RectangleEdgeFilter.All);
		}

		private static void DrawPath(Graphics gfx, Pen pen, GraphicsPath path)
		{
			var old = gfx.SmoothingMode;
			gfx.SmoothingMode = SmoothingMode.AntiAlias;
			gfx.DrawPath(pen, path);
			gfx.SmoothingMode = old;
		}

		private static void FillPath(Graphics gfx, Brush brush, GraphicsPath path)
		{
			var old = gfx.SmoothingMode;
			gfx.SmoothingMode = SmoothingMode.AntiAlias;
			gfx.FillPath(brush, path);
			gfx.SmoothingMode = old;
		}

		private static void DrawRoundedRectangle(Graphics gfx, Pen pen, RectangleF rectangle, int radius, RectangleEdgeFilter filter)
		{
			var path = GenerateRoundedRectangle(rectangle, radius, filter);
			DrawPath(gfx, pen, path);
		}

		private static void FillRoundedRectangle(Graphics gfx, Brush brush, RectangleF rectangle, int radius, RectangleEdgeFilter filter)
		{
			var path = GenerateRoundedRectangle(rectangle, radius, filter);
			FillPath(gfx, brush, path);
		}

		private static GraphicsPath GenerateRoundedRectangle(RectangleF rectangle, float radius, RectangleEdgeFilter filter)
		{
			var path = new GraphicsPath();
			if (radius <= 0.0F || filter == RectangleEdgeFilter.None)
			{
				path.AddRectangle(rectangle);
				path.CloseFigure();
				return path;
			}

			if (radius >= Math.Min(rectangle.Width, rectangle.Height) / 2.0)
			{
				return GenerateCapsule(rectangle);
			}

			var diameter = radius * 2.0F;
			var sizeF = new SizeF(diameter, diameter);
			var arc = new RectangleF(rectangle.Location, sizeF);
			if (filter.HasFlag(RectangleEdgeFilter.TopLeft))
			{
				path.AddArc(arc, 180, 90);
			}
			else
			{
				path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
				path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
			}
			arc.X = rectangle.Right - diameter;
			if (filter.HasFlag(RectangleEdgeFilter.TopRight))
			{
				path.AddArc(arc, 270, 90);
			}
			else
			{
				path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
				path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X + arc.Width, arc.Y);
			}
			arc.Y = rectangle.Bottom - diameter;
			if (filter.HasFlag(RectangleEdgeFilter.BottomRight))
			{
				path.AddArc(arc, 0, 90);
			}
			else
			{
				path.AddLine(arc.X + arc.Width, arc.Y, arc.X + arc.Width, arc.Y + arc.Height);
				path.AddLine(arc.X, arc.Y + arc.Height, arc.X + arc.Width, arc.Y + arc.Height);
			}
			arc.X = rectangle.Left;
			if (filter.HasFlag(RectangleEdgeFilter.BottomLeft))
			{
				path.AddArc(arc, 90, 90);
			}
			else
			{
				path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X, arc.Y + arc.Height);
				path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
			}
			path.CloseFigure();
			return path;
		}

		private static GraphicsPath GenerateCapsule(RectangleF rectangle)
		{
			var path = new GraphicsPath();
			try
			{
				float diameter;
				RectangleF arc;
				if (rectangle.Width > rectangle.Height)
				{
					diameter = rectangle.Height;
					var sizeF = new SizeF(diameter, diameter);
					arc = new RectangleF(rectangle.Location, sizeF);
					path.AddArc(arc, 90, 180);
					arc.X = rectangle.Right - diameter;
					path.AddArc(arc, 270, 180);
				}
				else if (rectangle.Width < rectangle.Height)
				{
					diameter = rectangle.Width;
					var sizeF = new SizeF(diameter, diameter);
					arc = new RectangleF(rectangle.Location, sizeF);
					path.AddArc(arc, 180, 180);
					arc.Y = rectangle.Bottom - diameter;
					path.AddArc(arc, 0, 180);
				}
				else path.AddEllipse(rectangle);
			}
			catch
			{
				path.AddEllipse(rectangle);
			}
			finally
			{
				path.CloseFigure();
			}
			return path;
		}

		[Flags]
		public enum RectangleEdgeFilter
		{
			None = 0,
			TopLeft = 1,
			TopRight = 2,
			BottomLeft = 4,
			BottomRight = 8,
			All = TopLeft | TopRight | BottomLeft | BottomRight
		}
	}
}
