using System.Collections.Generic;
using System.Xml.Serialization;

namespace NCore.Serialization
{
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

			if (wasEmpty)
				return;

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
