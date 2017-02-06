using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace NCore
{
	public static class Extensions
	{
		public static int FindByteArray(this byte[] source, byte[] searchedBytes)
		{
			if (searchedBytes.Length > source.Length) return -1;

			for (var i = 0; i < source.Length - searchedBytes.Length; i++)
			{
				var match = true;
				for (var j = 0; j < searchedBytes.Length; j++)
				{
					if (source[i + j] != searchedBytes[j])
					{
						match = false;
						break;
					}
				}
				if (match) return i;
			}
			return -1;
		}

		public static List<int> ToList([NotNull] this ListBox.SelectedIndexCollection indexCollection)
		{
			if (indexCollection == null) throw new ArgumentNullException("indexCollection");
			return new List<int>(indexCollection.Cast<int>());
		}

		public static void AddRange([NotNull] this ListBox.SelectedIndexCollection indexCollection, [NotNull] IEnumerable<int> indices)
		{
			if (indexCollection == null) throw new ArgumentNullException("indexCollection");
			if (indices == null) throw new ArgumentNullException("indices");

			foreach (var index in indices)
			{
				indexCollection.Add(index);
			}
		}

		public static void ForEach<T>([NotNull] this IEnumerable<T> collection, [NotNull] Action<T> action)
		{
			if (collection == null) throw new ArgumentNullException("collection");
			if (action == null) throw new ArgumentNullException("action");

			foreach (var item in collection)
			{
				action(item);
			}
		}

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			var seenKeys = new HashSet<TKey>();
			foreach (var element in source)
			{
				if (seenKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}

		public static void ForEach([NotNull] this ListView.CheckedListViewItemCollection collection, [NotNull] Action<ListViewItem> action)
		{
			if (collection == null) throw new ArgumentNullException("collection");
			if (action == null) throw new ArgumentNullException("action");

			collection.OfType<ListViewItem>().ForEach(action);
		}

		public static void Fill([NotNull] this ListBox listBox, [NotNull] IEnumerable<object> items, bool selectFirstItem)
		{
			if (listBox == null) throw new ArgumentNullException("listBox");
			if (items == null) throw new ArgumentNullException("items");

			listBox.Items.Clear();
			listBox.BeginUpdate();
			items.ForEach(item => listBox.Items.Add(item));
			listBox.EndUpdate();

			if (selectFirstItem && listBox.Items.Count > 0)
			{
				listBox.SelectedIndex = 0;
			}
		}

		public static void Fill([NotNull] this ListView listView, [NotNull] IEnumerable<ListViewItem> items)
		{
			if (listView == null) throw new ArgumentNullException("listView");
			if (items == null) throw new ArgumentNullException("items");

			listView.Items.Clear();
			listView.BeginUpdate();
			items.ForEach(item => listView.Items.Add(item));
			listView.EndUpdate();
		}

		public static void Fill(this ComboBox comboBox, object[] items)
		{
			comboBox.BeginUpdate();
			comboBox.Items.Clear();
			comboBox.Items.AddRange(items);
			comboBox.EndUpdate();
		}

		public static void ScrollToEnd([NotNull] this TextBox textBox)
		{
			if (textBox == null) throw new ArgumentNullException("textBox");

			textBox.SelectionStart = textBox.TextLength;
			textBox.ScrollToCaret();
		}

		public static void SetValue([NotNull] this NumericUpDown upDown, decimal value)
		{
			if (upDown == null) throw new ArgumentNullException("upDown");

			upDown.Value = Math.Max(upDown.Minimum, Math.Min(upDown.Maximum, value));
		}

		public static StringBuilder AppendLine([NotNull] this StringBuilder sb, [NotNull] string format, params object[] args)
		{
			if (sb == null) throw new ArgumentNullException("sb");
			if (string.IsNullOrEmpty(format)) throw new ArgumentNullException("format");

			sb.AppendLine(string.Format(format, args));
			return sb;
		}

		public static string Nvl(this string source, string value)
		{
			return string.IsNullOrEmpty(source) ? value : source;
		}

		public static string SplitLines([CanBeNull] this string source)
		{
			return (source ?? string.Empty).Trim().Replace("\n", Environment.NewLine);
		}
	}
}
