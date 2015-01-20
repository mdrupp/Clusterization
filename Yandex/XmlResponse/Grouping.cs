using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Grouping
    {
        [XmlAttribute("attr")]
        public string Attribute { get; set; }

        [XmlAttribute("docs-in-group")]
        public int DocsInGroup { get; set; }

        [XmlElement("found-docs")]
        public Found[] FoundDocuments { get; set; }

        [XmlElement("group")]
        public Group[] Groups { get; set; }

        [XmlAttribute("groups-on-page")]
        public int GroupsOnPage { get; set; }

        [XmlAttribute("mode")]
        public string Mode { get; set; }
    }
}
