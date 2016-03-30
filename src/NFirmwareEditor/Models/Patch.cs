using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using NFirmwareEditor.Core;
using NFirmwareEditor.Managers;

namespace NFirmwareEditor.Models
{
	public class Patch
	{
		private readonly XmlSerializerNamespaces m_namespaces;
		private IEnumerable<PatchModificationData> m_data;

		public Patch()
		{
			m_namespaces = new XmlSerializerNamespaces(new[]
			{
				new XmlQualifiedName(string.Empty, Consts.PatchXmlNamespace)
			});
		}

		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get { return m_namespaces; }
		}

		[XmlAttribute]
		public string Definition { get; set; }

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public string Version { get; set; }

		[XmlAttribute]
		public string Author { get; set; }

		public string Description { get; set; }

		[XmlElement("Data")]
		public string DataString { get; set; }

		[XmlIgnore]
		internal IEnumerable<PatchModificationData> Data
		{
			get { return m_data ?? (m_data = PatchManager.ParseDiff(DataString)); }
		}

		[XmlIgnore]
		internal bool IsApplied { get; set; }

		[XmlIgnore]
		internal bool IsCompatible { get; set; }
	}
}
