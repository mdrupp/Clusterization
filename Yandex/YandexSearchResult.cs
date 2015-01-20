using System.Xml.Serialization;
using Yandex.XmlRequest;
using Yandex.XmlResponse;

namespace Yandex
{
    [XmlRoot("yandexsearch")]
    public class YandexSearchResult
    {
        [XmlElement("request")]
        public YandexRequest Request { get; set; }

        [XmlElement("response")]
        public YandexResponse Response { get; set; }

        public string Content { get; set; }
    }
}
