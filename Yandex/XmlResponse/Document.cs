using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Document
    {
        [XmlElement("domain")]
        public string Domain { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlIgnore]
        public DateTime ModificationTime
        {
            get { return DateTime.ParseExact(ModificationTimeString, "yyyyMMddTHHmmss", null); }
        }

        [XmlElement("modtime")]
        public string ModificationTimeString { get; set; }

        [XmlArray("passages")]
        [XmlArrayItem("passage")]
        public List<Passage> Passages { get; set; }

        [XmlElement("size")]
        public int Size { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}
