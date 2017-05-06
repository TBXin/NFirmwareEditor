using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace NCore.UI
{
	public sealed class MenuButton : Button
	{
		private ContextMenuStrip m_menu;
		private const int DropDownArrowRectangleWidth = 17;

		[DefaultValue(null)]
		public ContextMenuStrip Menu
		{
			get { return m_menu; }
			set
			{
				if (m_menu != null) m_menu.ItemClicked -= Menu_ItemClicked;
				m_menu = value;
				if (m_menu != null) m_menu.ItemClicked += Menu_ItemClicked;
			}
		}

		public MenuButton()
		{
			TextAlign = ContentAlignment.MiddleLeft;
			ImageAlign = ContentAlignment.MiddleLeft;
			TextImageRelation = TextImageRelation.ImageBeforeText;
		}

		private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			Image = e.ClickedItem.Image;
			Text = e.ClickedItem.Text;
		}

		#region Overrides of Button
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if (Parent != null) Parent.Focus();

			Menu.Show(this, new Point(0, Height));
			Menu.Focus();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (ComboBoxRenderer.IsSupported)
			{
				var actualDropDownArrowRectangleWidth = e.Graphics.ScaleToDpi(DropDownArrowRectangleWidth);
				var rect = new Rectangle
				(
					e.ClipRectangle.X + e.ClipRectangle.Width - actualDropDownArrowRectangleWidth - 1,
					1,
					actualDropDownArrowRectangleWidth,
					e.ClipRectangle.Y + e.ClipRectangle.Height - 2
				);
				DrawDropDownButton(e.Graphics, rect, ComboBoxState.Normal, ComboBoxElementParts.DropDownButtonRight);
			}
			else
			{
				var arrowX = ClientRectangle.Width - 14;
				var arrowY = ClientRectangle.Height / 2 - 1;

				var brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
				var arrows = new[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
				e.Graphics.FillPolygon(brush, arrows);
			}
		}
		#endregion

		private static void DrawDropDownButton(IDeviceContext g, Rectangle bounds, ComboBoxState state, ComboBoxElementParts part = ComboBoxElementParts.DropDownButton)
		{
			var visualStyleRenderer = new VisualStyleRenderer("COMBOBOX", (int)part, (int)state);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		private enum ComboBoxElementParts
		{
			DropDownButton = 1,
			Border = 4,
			ReadOnlyButton = 5,
			DropDownButtonRight = 6,
			DropDownButtonLeft = 7
		}
	}
}
