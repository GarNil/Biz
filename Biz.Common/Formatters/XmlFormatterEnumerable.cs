using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Biz.Common
{
    public abstract class XmlFormatterEnumerable<T> : IFormatterEnumerable<T>
        where T : class
    {
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(T), "");
        private readonly XmlSerializerNamespaces namespaces; 
        private readonly XmlWriterSettings settings;
        public XmlFormatterEnumerable()
        {
            namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
        }

        public IEnumerable<string> Serialize(IEnumerable<T> rows)
        {
            yield return "<List>";
            foreach (var row in rows.Where(j => j != null))
            {
                string result = null; 
                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, row, namespaces);
                    result = stream.ToString();
                }
                yield return result;
            }
            yield return "</List>";
        }
    }
}
