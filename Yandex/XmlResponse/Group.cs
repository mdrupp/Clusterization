using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Group
    {
        [XmlElement("categ")]
        public Category Category { get; set; }

        [XmlElement("doc")]
        public Document[] Documents { get; set; }

        [XmlElement("doccount")]
        public int DocumentsCount { get; set; }
    }
}
