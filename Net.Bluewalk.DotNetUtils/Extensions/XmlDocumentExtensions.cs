using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class XmlDocumentExtensions
    {
        /// <summary>
        /// Tries to load XML.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="xml">The XML.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool TryLoadXml(this XmlDocument doc, string xml)
        {
            try
            {
                doc.LoadXml(xml);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Removes all namespaces.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns>System.String.</returns>
        public static string RemoveAllNamespaces(this string xmlDocument)
        {
            var xmlDocumentWithoutNs = XElement.Parse(xmlDocument).RemoveAllNamespaces();

            return xmlDocumentWithoutNs.ToString();
        }

        /// <summary>
        /// Removes all namespaces.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns>XElement.</returns>
        public static XElement RemoveAllNamespaces(this XElement xmlDocument)
        {
            if (xmlDocument.HasElements)
                return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(RemoveAllNamespaces));

            var xElement = new XElement(xmlDocument.Name.LocalName)
            {
                Value = xmlDocument.Value
            };

            foreach (var attribute in xmlDocument.Attributes())
                xElement.Add(attribute);

            return xElement;
        }
    }
}
