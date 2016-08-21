using System.Collections.Generic;
using JetBrains.Annotations;

namespace NFirmwareEditor.Storages
{
	internal interface IStorage
	{
		void Initialize();
	}

	internal interface IFileStorage<out T> : IStorage
	{
		[CanBeNull]
		T TryLoad([NotNull] string filePath);

		[NotNull, ItemNotNull]
		IEnumerable<T> LoadAll();
	}
}
