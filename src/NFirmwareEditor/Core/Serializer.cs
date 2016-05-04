using System;
using System.IO;
using System.Text;
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
		public static T Read<T>([NotNull] Stream stream) where T : class
		{
			if (stream == null) throw new ArgumentNullException("stream");

			var serializer = new XmlSerializer(typeof(T));
			return serializer.Deserialize(stream) as T;
		}

		[CanBeNull]
		public static T Read<T>(string xml) where T : class
		{
			if (string.IsNullOrEmpty(xml)) throw new ArgumentNullException("stream");

			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
			{
				return Read<T>(ms);
			}
		}

		public static void Write<T>([NotNull] T data, [NotNull] Stream stream) where T : class
		{
			if (data == null) throw new ArgumentNullException("data");
			if (stream == null) throw new ArgumentNullException("stream");

			var serializer = new XmlSerializer(typeof(T));
			using (var writer = XmlWriter.Create(stream, s_xmlWriterSettings))
			{
				var namespacelessObject = data as NamespacelessObject;
				if (namespacelessObject != null)
				{
					serializer.Serialize(writer, data, namespacelessObject.Namespaces);
				}
				else
				{
					serializer.Serialize(writer, data);
				}
			}
		}

		public static string Write<T>([NotNull] T data) where T : class
		{
			if (data == null) throw new ArgumentNullException("data");

			using (var ms = new MemoryStream())
			{
				Write(data, ms);
				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}
	}
}
