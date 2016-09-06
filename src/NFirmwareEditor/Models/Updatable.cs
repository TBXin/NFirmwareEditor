namespace NFirmwareEditor.Models
{
	internal interface IUpdatable
	{
		string FileName { get; set; }

		string Sha { get; set; }
	}

	internal class Updatable<T> : IUpdatable
	{
		public T Entity { get; set; }

		#region Implementation of IUpdatable
		public string FileName { get; set; }

		public string Sha { get; set; }
		#endregion
	}
}
