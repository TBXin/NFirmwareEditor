using System.Collections.Generic;
using System.Linq;

namespace NFirmwareEditor.Core
{
	internal class MruList<T>
	{
		private readonly int m_capacity;
		private readonly List<T> m_items;

		public MruList(IList<T> source, int capacity = 20)
		{
			m_capacity = capacity;
			m_items = new List<T>(m_capacity);

			if (source == null) return;

			source = source.Count > capacity ? source.Take(capacity).ToList() : source;
			foreach (var sourceItem in source)
			{
				m_items.Add(sourceItem);
			}
		}

		public void Add(T item)
		{
			Remove(item);
			if (m_items.Count == m_capacity)
			{
				m_items.RemoveAt(m_items.Count - 1);
			}
			m_items.Insert(0, item);
		}

		public bool Remove(T item)
		{
			return m_items.Remove(item);
		}

		public List<T> Items
		{
			get { return m_items; }
		}
	}
}
