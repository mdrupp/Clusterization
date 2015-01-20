using System;
using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    [XmlRoot("response")]
    public class YandexResponse
    {
        [XmlAttribute("date")]
        public string DateString { get; set; }

        [XmlIgnore]
        public DateTime DateTime
        {
            get { return DateTime.ParseExact(DateString, "yyyyMMddTHHmmss", null); }
        }

        [XmlElement("error")]
        public Error Error { get; set; }

        [XmlElement("results")]
        public Results Results { get; set; }

        [XmlElement("found")]
        public Found[] Found { get; set; }
    }
}
