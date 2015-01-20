using System.Xml.Serialization;

namespace Yandex.XmlRequest
{
    public class SortBy
    {
        [XmlAttribute("order")]
        public string Order { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
