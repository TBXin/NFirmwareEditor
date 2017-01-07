using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;
using NCore.Serialization;

namespace NToolbox.Models
{
	public class SerializableConfiguration : NamespacelessObject
	{
		public SerializableConfiguration()
		{
			Data = new List<StringKeyValuePair>();
		}

		public SerializableConfiguration([NotNull] IDictionary<string, string> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			Data = new List<StringKeyValuePair>(source.Count);
			foreach (var kvp in source)
			{
				Data.Add(new StringKeyValuePair { Key = kvp.Key, Value = kvp.Value });
			}
		}

		[XmlArray("Data"), XmlArrayItem("Item")]
		public List<StringKeyValuePair> Data { get; set; }

		public IDictionary<string, string> GetDictionary()
		{
			var result = new Dictionary<string, string>(Data != null ? Data.Count : 0);
			if (Data == null) return result;
			foreach (var kvp in Data)
			{
				result[kvp.Key] = kvp.Value;
			}
			return result;
		}
	}

	public struct StringKeyValuePair
	{
		[XmlAttribute("Key")]
		public string Key { get; set; }

		[XmlAttribute("Value")]
		public string Value { get; set; }
	}
}
