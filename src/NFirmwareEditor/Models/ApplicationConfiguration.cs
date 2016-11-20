using System.Collections.Generic;
using System.Xml.Serialization;
using NCore.Serialization;

namespace NFirmwareEditor.Models
{
	[XmlType("Configuration")]
	public class ApplicationConfiguration : NamespacelessObject
	{
		public ApplicationConfiguration()
		{
			MainWindowWidth = 800;
			MainWindowHeight = 600;
			GridSize = 16;
			ShowGid = true;
			ImageEditorMouseMode = ImageEditorMouseMode.LeftSetRightUnset;
			BackupCreationMode = BackupCreationMode.Extended;
			MostRecentlyUsed = new List<string>();
			CheckForApplicationUpdates = true;
			CheckForPatchesUpdates = true;
			CheckForDefinitionsUpdates = true;
			DeviceMonitorSeries = new SerializableDictionary<string, bool>();
		}

		public int MainWindowTop { get; set; }

		public int MainWindowLeft { get; set; }

		public int MainWindowWidth { get; set; }

		public int MainWindowHeight { get; set; }

		public bool MainWindowMaximaged { get; set; }

		public bool ShowGid { get; set; }

		public int GridSize { get; set; }

		public ImageEditorMouseMode ImageEditorMouseMode { get; set; }

		public BackupCreationMode BackupCreationMode { get; set; }

		public bool CheckForApplicationUpdates { get; set; }

		public bool CheckForPatchesUpdates { get; set; }

		public bool CheckForDefinitionsUpdates { get; set; }

		[XmlArrayItem("Item")]
		public List<string> MostRecentlyUsed { get; set; }

		public SerializableDictionary<string, bool> DeviceMonitorSeries { get; set; }
	}

	public enum ImageEditorMouseMode
	{
		LeftSetRightUnset,
		LeftSetUnset
	}

	public enum BackupCreationMode
	{
		Disabled,
		Simple,
		Extended
	}
}
