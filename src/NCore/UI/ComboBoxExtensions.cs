using System;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace NCore.UI
{
	public static class ComboBoxExtensions
	{
		public static void SelectItem<T>([NotNull] this ComboBox comboBox, T item)
		{
			if (comboBox == null) throw new ArgumentNullException("comboBox");

			foreach (var comboBoxItem in comboBox.Items)
			{
				var namedItem = comboBoxItem as NamedItemContainer<T>;
				if (namedItem == null) continue;

				if (Equals(namedItem.Data, item))
				{
					comboBox.SelectedItem = namedItem;
					return;
				}
			}
		}

		public static T GetSelectedItem<T>(this ComboBox comboBox)
		{
			var item = comboBox.SelectedItem as NamedItemContainer<T>;
			if (item == null) throw new InvalidOperationException("Invalid comboBox content.");

			return item.Data;
		}
	}
}
