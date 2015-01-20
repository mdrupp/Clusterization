using System;
using RestSharp;
using Yandex.Serialization;
using Yandex.XmlRequest;

namespace Yandex
{
    public class YandexSearchEngine
    {
        private readonly string _requestUrl = string.Empty;

        private readonly RestClient _restClient;

        public YandexSearchEngine(Uri baseUrl, string requestUrl)
        {
            _requestUrl = requestUrl;
            _restClient = new RestClient(baseUrl);
            _restClient.AddHandler(@"text/xml", new YandexDeserializer());
        }

        public YandexSearchResult Search(YandexRequest request, out string content)
        {
            var restRequest = new RestRequest(_requestUrl, Method.POST)
            {
                XmlSerializer = new YandexSerializer(),
            };
            restRequest.AddBody(request);

            var response = _restClient.Execute<YandexSearchResult>(restRequest);
            content = response.Content;
            return response.Data;
        }
    }
}
