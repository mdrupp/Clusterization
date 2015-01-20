using System.Collections.Generic;
using System.Xml.Serialization;

namespace Yandex.XmlRequest
{
    [XmlRoot("request")]
    public class YandexRequest
    {
        [XmlArray("groupings")]
        [XmlArrayItem("groupby")]
        public List<GroupBy> Groupings { get; set; }

        [XmlElement("maxpassages")]
        public int MaxPassages { get; set; }

        [XmlElement("page")]
        public int Page { get; set; }

        [XmlElement("query")]
        public string Query { get; set; }

        [XmlElement("sortby")]
        public SortBy SortBy { get; set; }
    }
}
