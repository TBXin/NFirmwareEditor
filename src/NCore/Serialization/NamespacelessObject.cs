using System.Xml;
using System.Xml.Serialization;

namespace NCore.Serialization
{
	public class NamespacelessObject
	{
		private const string PatchXmlNamespace = "urn:NFirmware";
		private readonly XmlSerializerNamespaces m_namespaces;

		public NamespacelessObject()
		{
			m_namespaces = new XmlSerializerNamespaces(new[]
			{
				new XmlQualifiedName(string.Empty, PatchXmlNamespace)
			});
		}

		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get { return m_namespaces; }
		}
	}
}
