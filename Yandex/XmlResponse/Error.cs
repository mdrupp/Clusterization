using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Error
    {
        [XmlAttribute("code")]
        public int Code { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
