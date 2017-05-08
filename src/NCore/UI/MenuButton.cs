using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using JetBrains.Annotations;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace NCore.UI
{
	public sealed class MenuButton : Button
	{
		private ContextMenuStrip m_menu;
		private const int DropDownArrowRectangleWidth = 17;

		public MenuButton()
		{
			TextAlign = ContentAlignment.MiddleLeft;
			ImageAlign = ContentAlignment.MiddleLeft;
			TextImageRelation = TextImageRelation.ImageBeforeText;
			AutoEllipsis = true;
			CheckClickedMenuItem = true;
		}

		[DefaultValue(true)]
		public bool CheckClickedMenuItem { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CanBeNull]
		public ToolStripItem LastClickedMenuItem { get; private set; }

		public void AddItems(IEnumerable<ToolStripItem> items)
		{
			if (m_menu == null)
			{
				m_menu = new ContextMenuStrip();
				m_menu.ItemClicked += Menu_ItemClicked;
			}

			foreach (var item in items)
			{
				m_menu.Items.Add(item);
			}
		}

		public T GetSelectedItem<T>()
		{
			if (LastClickedMenuItem == null) throw new InvalidOperationException("LastClickedMenuItem == null");
			if (LastClickedMenuItem.Tag == null) throw new InvalidOperationException("LastClickedMenuItem.Tag == null");

			return (T)LastClickedMenuItem.Tag;
		}

		public void SelectItem<T>(T data, bool ignoreNotFound = false)
		{
			if (m_menu == null) throw new InvalidOperationException("m_menu == null");

			foreach (ToolStripItem menuItem in m_menu.Items)
			{
				if (Equals(menuItem.Tag, data))
				{
					menuItem.PerformClick();
					return;
				}
			}

			if (ignoreNotFound) return;
			throw new InvalidOperationException(string.Format("Item \"{0}\" in the \"{1}\" not found.", data, Name));
		}

		private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			LastClickedMenuItem = e.ClickedItem;

			if (LastClickedMenuItem == null) return;
			if (CheckClickedMenuItem)
			{
				var toolStripMenuItem = LastClickedMenuItem as ToolStripMenuItem;
				if (toolStripMenuItem != null)
				{
					foreach (var menuItem in m_menu.Items.OfType<ToolStripMenuItem>())
					{
						menuItem.Checked = false;
					}
					toolStripMenuItem.Checked = true;
				}
			}
			Image = LastClickedMenuItem.Image;
			Text = LastClickedMenuItem.Text;
		}

		#region Overrides of Button
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			if (m_menu == null) return;
			if (Parent != null) Parent.Focus();

			m_menu.Show(this, new Point(1, Height));
			m_menu.Focus();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (ComboBoxRenderer.IsSupported)
			{
				var actualDropDownArrowRectangleWidth = e.Graphics.ScaleToDpi(DropDownArrowRectangleWidth);
				var rect = new Rectangle
				(
					ClientRectangle.X + ClientRectangle.Width - actualDropDownArrowRectangleWidth - 1,
					1,
					actualDropDownArrowRectangleWidth,
					ClientRectangle.Y + ClientRectangle.Height - 2
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
