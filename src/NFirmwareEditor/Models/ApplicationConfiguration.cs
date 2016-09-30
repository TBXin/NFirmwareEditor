using System.Collections.Generic;
using System.Xml.Serialization;
using NFirmware;

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

	[XmlRoot("dictionary")]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
	{
		private const string ItemElement = "Item";
		private const string KeyElement = "Key";
		private const string ValueElement = "Value";

		#region IXmlSerializable Members
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			var keySerializer = new XmlSerializer(typeof(TKey));
			var valueSerializer = new XmlSerializer(typeof(TValue));

			var wasEmpty = reader.IsEmptyElement;
			reader.Read();

			if (wasEmpty) return;

			while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
			{
				reader.ReadStartElement(ItemElement);

				reader.ReadStartElement(KeyElement);
				var key = (TKey)keySerializer.Deserialize(reader);
				reader.ReadEndElement();

				reader.ReadStartElement(ValueElement);
				var value = (TValue)valueSerializer.Deserialize(reader);
				reader.ReadEndElement();

				Add(key, value);

				reader.ReadEndElement();
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			var keySerializer = new XmlSerializer(typeof(TKey));
			var valueSerializer = new XmlSerializer(typeof(TValue));

			foreach (var key in Keys)
			{
				writer.WriteStartElement(ItemElement);

				writer.WriteStartElement(KeyElement);
				keySerializer.Serialize(writer, key);
				writer.WriteEndElement();

				writer.WriteStartElement(ValueElement);
				var value = this[key];
				valueSerializer.Serialize(writer, value);
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
		}
		#endregion
	}
}
