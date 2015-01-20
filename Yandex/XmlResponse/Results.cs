using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Results
    {
        [XmlElement("grouping")]
        public Grouping Grouping { get; set; }
    }
}
