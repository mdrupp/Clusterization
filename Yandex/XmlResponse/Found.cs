using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Found
    {
        [XmlText]
        public long Count { get; set; }

        [XmlAttribute("priority")]
        public string Priority { get; set; }
    }
}
