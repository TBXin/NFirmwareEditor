namespace NFirmwareEditor.Core
{
	public class Configuration
	{
		public Configuration()
		{
			MainWindowWidth = 800;
			MainWindowHeight = 600;
		}

		public int MainWindowWidth { get; set; }

		public int MainWindowHeight { get; set; }

		public bool MainWindowMaximaged { get; set; }

		public string LastUsedDefinition { get; set; }
	}
}
