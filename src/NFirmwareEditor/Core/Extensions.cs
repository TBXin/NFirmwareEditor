using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace NFirmwareEditor.Core
{
	internal static class Extensions
	{
		internal static List<int> ToList([NotNull] this ListBox.SelectedIndexCollection indexCollection)
		{
			if (indexCollection == null) throw new ArgumentNullException("indexCollection");
			return new List<int>(indexCollection.Cast<int>());
		}

		internal static void AddRange([NotNull] this ListBox.SelectedIndexCollection indexCollection, [NotNull] IEnumerable<int> indices)
		{
			if (indexCollection == null) throw new ArgumentNullException("indexCollection");
			if (indices == null) throw new ArgumentNullException("indices");

			foreach (var index in indices)
			{
				indexCollection.Add(index);
			}
		}
	}
}
