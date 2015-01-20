using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using RestSharp;
using RestSharp.Deserializers;

namespace Yandex.Serialization
{
    public class YandexDeserializer : IDeserializer
    {
        public YandexDeserializer()
        {
            DateFormat = @"yyyymmDDTHHmmss";
            Namespace = string.Empty;
            RootElement = string.Empty;
        }

        public T Deserialize<T>(IRestResponse response)
        {
            var content = Regex.Replace(response.Content, @"\<hlword\>(.*?)\</hlword\>", "$1");

            var serializer = new XmlSerializer(typeof(T), "");
            var stringReader = new StringReader(content);

            var result = (T)serializer.Deserialize(stringReader);

            return result;
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }
}
