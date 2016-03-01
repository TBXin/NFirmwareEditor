namespace NFirmwareEditor.Models
{
	public class Configuration
	{
		public Configuration()
		{
			MainWindowWidth = 800;
			MainWindowHeight = 600;
			GridSize = 16;
			ShowGid = true;
		}

		public int MainWindowWidth { get; set; }

		public int MainWindowHeight { get; set; }

		public bool MainWindowMaximaged { get; set; }

		public string LastUsedDefinition { get; set; }

		public bool ShowGid { get; set; }

		public int GridSize { get; set; }
	}
}
