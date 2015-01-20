using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Category
    {
        [XmlAttribute("attr")]
        public string Attribute { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
