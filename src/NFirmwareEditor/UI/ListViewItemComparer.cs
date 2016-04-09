using System.Collections;
using System.Windows.Forms;

namespace NFirmwareEditor.UI
{
	internal class ListViewItemComparer : IComparer
	{
		public ListViewItemComparer(int column, bool sortAscending)
		{
			ColumnIndex = column;
			SortAscending = sortAscending;
		}

		public int ColumnIndex
		{
			get; private set;
		}

		public bool SortAscending
		{
			get; private set;
		}

		public int Compare(object x, object y)
		{
			var item1 = (ListViewItem)x;
			var item2 = (ListViewItem)y;

			var item1Value = item1.SubItems[ColumnIndex].Text;
			var item2Value = item2.SubItems[ColumnIndex].Text;

			return SortAscending
				? string.CompareOrdinal(item1Value, item2Value)
				: string.CompareOrdinal(item2Value, item1Value);
		}

		internal static void ListViewColumnClick(object sender, ColumnClickEventArgs e)
		{
			var listView = sender as ListView;
			if (listView == null) return;

			var currentSorter = listView.ListViewItemSorter as ListViewItemComparer;
			if (currentSorter != null && currentSorter.ColumnIndex == e.Column)
			{
				listView.ListViewItemSorter = new ListViewItemComparer(currentSorter.ColumnIndex, !currentSorter.SortAscending);
			}
			else
			{
				listView.ListViewItemSorter = new ListViewItemComparer(e.Column, true);
			}
		}
	}
}
