using System.Drawing;
using JetBrains.Annotations;

namespace NFirmwareEditor.UI
{
	internal class ImagedListBoxItem<T>
	{
		[CanBeNull]
		private readonly string m_dispayName;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public ImagedListBoxItem([CanBeNull] T value, [CanBeNull] Image image, [CanBeNull] string dispayName = null)
		{
			m_dispayName = dispayName;
			Image = image;
			Value = value;
		}

		[CanBeNull]
		public T Value { get; set; }

		[CanBeNull]
		public Image Image { get; set; }

		public override string ToString()
		{
			return m_dispayName ?? (Value != null ? Value.ToString() : string.Empty);
		}
	}
}
