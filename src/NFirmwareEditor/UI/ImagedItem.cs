using System;
using JetBrains.Annotations;

namespace NFirmwareEditor.UI
{
	internal class ImagedItem<T>
	{
		[CanBeNull]
		private readonly string m_dispayName;

		/// <summary>
		/// Initializes a new instance of the <see cref="ImagedItem{T}"/> class.
		/// </summary>
		public ImagedItem([NotNull] T value, int imageCacheIndex, [CanBeNull] string dispayName = null)
		{
			if (value == null) throw new ArgumentNullException("value");

			m_dispayName = dispayName;
			ImageCacheIndex = imageCacheIndex;
			Value = value;
		}

		[NotNull]
		public T Value { get; set; }

		public int ImageCacheIndex { get; set; }

		public override string ToString()
		{
			return m_dispayName ?? Value.ToString();
		}
	}
}
