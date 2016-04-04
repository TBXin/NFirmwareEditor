using System.Xml;
using System.Xml.Serialization;
using NFirmwareEditor.Core;

namespace NFirmwareEditor.Models
{
	public class NamespacelessObject
	{
		private readonly XmlSerializerNamespaces m_namespaces;

		public NamespacelessObject()
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
	}
}
