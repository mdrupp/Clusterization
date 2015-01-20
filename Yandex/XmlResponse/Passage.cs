using System.Xml.Serialization;

namespace Yandex.XmlResponse
{
    public class Passage
    {
        [XmlText(DataType = "string", Type = typeof(string))]
        public string Text { get; set; }
    }
}
