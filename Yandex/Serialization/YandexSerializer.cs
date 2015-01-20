using System.Xml.Serialization;
using RestSharp.Serializers;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace Yandex.Serialization
{
    public class YandexSerializer : ISerializer
    {
        public YandexSerializer()
        {
            ContentType = @"text/xml";
            DateFormat = @"yyyymmDDTHHmmss";
            Namespace = string.Empty;
            RootElement = string.Empty;
        }

        public string Serialize(object obj)
        {
            var serializer = new XmlSerializer(obj.GetType(), "");
            var stringWriter = new Utf8StringWriter();

            var xnameSpace = new XmlSerializerNamespaces();
            xnameSpace.Add("", "");

            serializer.Serialize(stringWriter, obj, xnameSpace);

            return stringWriter.ToString();
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}
