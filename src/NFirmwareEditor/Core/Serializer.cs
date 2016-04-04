using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;
using NFirmwareEditor.Models;

namespace NFirmwareEditor.Core
{
	internal static class Serializer
	{
		private static readonly XmlWriterSettings s_xmlWriterSettings = new XmlWriterSettings
		{
			OmitXmlDeclaration = true,
			Indent = true
		};

		[CanBeNull]
		public static T Read<T>([NotNull] Stream stream) where T : NamespacelessObject
		{
			if (stream == null) throw new ArgumentNullException("stream");

			var serializer = new XmlSerializer(typeof(T));
			return serializer.Deserialize(stream) as T;
		}

		public static void Write<T>([NotNull] T data, [NotNull] Stream stream) where T : NamespacelessObject
		{
			if (data == null) throw new ArgumentNullException("data");
			if (stream == null) throw new ArgumentNullException("stream");

			var serializer = new XmlSerializer(typeof(T));
			using (var writer = XmlWriter.Create(stream, s_xmlWriterSettings))
			{
				serializer.Serialize(writer, data, data.Namespaces);
			}
		}
	}
}
