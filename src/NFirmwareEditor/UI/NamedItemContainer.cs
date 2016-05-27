using System;

namespace NFirmwareEditor.UI
{
	internal class NamedItemContainer<T>
	{
		public NamedItemContainer(string name, T data)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

			Name = name;
			Data = data;
		}

		private string Name { get; set; }

		public T Data { get; private set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
