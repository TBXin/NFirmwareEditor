using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NFirmwareEditor.UI
{
	/// <summary>
	/// This is a special ComboBox that each item may conatins an image.
	/// </summary>
	public class ImagedComboBox : ComboBox
	{
		private ImagedComboBoxItemsCollection m_items;

		/// <summary>
		/// The imaged ComboBox items.
		/// this property is invisibile for design serializer.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImagedComboBoxItemsCollection Items
		{
			get { return m_items; }
			set { m_items = value; }
		}

		public ImagedComboBox()
		{
			m_items = new ImagedComboBoxItemsCollection { ItemsBase = base.Items };

			DropDownStyle = ComboBoxStyle.DropDownList;
			DrawMode = DrawMode.OwnerDrawVariable;
		}

		protected override ControlCollection CreateControlsInstance()
		{
			m_items = new ImagedComboBoxItemsCollection { ItemsBase = base.Items };
			return base.CreateControlsInstance();
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			base.OnMeasureItem(e);
			if (e.Index < 0) return;

			var item = Items[e.Index];
			try
			{
				if (item.Image != null)
				{
					e.ItemHeight = item.Image.Height + 4;
				}
			}
			catch
			{
				// Ignore
			}
		}

		#region Overrides of ComboBox
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs"/> that contains the event data. </param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.DrawBackground();
			if (e.Index >= 0)
			{
				var item = Items[e.Index];
				var itemText = item.ToString();
				var textSize = e.Graphics.MeasureString(itemText, Font);

				e.Graphics.DrawString(itemText, Font, new SolidBrush(ForeColor), e.Bounds.X, e.Bounds.Y + (int)(e.Bounds.Height / 2f - textSize.Height / 2f));
				
				try
				{
					e.Graphics.DrawImage(item.Image, e.Bounds.X + 40, e.Bounds.Y + 2, item.Image.Width, item.Image.Height);
				}
				catch
				{
					// Ignore
				}
			}
			e.DrawFocusRectangle();
		}
		#endregion

		/// <summary>
		/// This class represents an ComboBox item of the ImagedComboBox which may contains an image and value.
		/// </summary>
		[Serializable]
		public class ImagedComboBoxItem
		{
			private object m_value;
			private Image m_image;
			private readonly string m_dispayName;

			/// <summary>
			/// ComobBox Item.
			/// </summary>
			public object Value
			{
				get { return m_value; }
				set { m_value = value; }
			}

			/// <summary>
			/// Item image.
			/// </summary>
			public Image Image
			{
				get { return m_image; }
				set { m_image = value; }
			}

			public ImagedComboBoxItem()
			{
				m_value = string.Empty;
				m_image = new Bitmap(1, 1);
			}

			/// <summary>
			/// Constructor item without image.
			/// </summary>
			/// <param name="value">Item value.</param>
			public ImagedComboBoxItem(object value)
			{
				m_value = value;
				m_image = new Bitmap(1, 1);
			}

			/// <summary>
			///  Constructor item with image.
			/// </summary>
			/// <param name="value">Item value.</param>
			/// <param name="image">Item image.</param>
			/// <param name="dispayName">Display name.</param>
			public ImagedComboBoxItem(object value, Image image, string dispayName = null)
			{
				m_value = value;
				m_image = image;
				m_dispayName = dispayName;
			}

			public override string ToString()
			{
				return m_dispayName ?? (m_value != null ? m_value.ToString() : string.Empty);
			}
		}

		/// <summary>
		/// Collections of ImagedComboBoxItem.
		/// </summary>
		public class ImagedComboBoxItemsCollection : CollectionBase
		{
			public ObjectCollection ItemsBase { get; set; }

			public ImagedComboBoxItem this[int index]
			{
				get { return (ImagedComboBoxItem)ItemsBase[index]; }
				set { ItemsBase[index] = value; }
			}

			public int Add(ImagedComboBoxItem value)
			{
				var result = ItemsBase.Add(value);
				return result;
			}

			public int IndexOf(ImagedComboBoxItem value)
			{
				return ItemsBase.IndexOf(value);
			}

			public void Insert(int index, ImagedComboBoxItem value)
			{
				ItemsBase.Insert(index, value);
			}

			public void Remove(ImagedComboBoxItem value)
			{
				ItemsBase.Remove(value);
			}

			public bool Contains(ImagedComboBoxItem value)
			{
				return ItemsBase.Contains(value);
			}
		}
	}
}
