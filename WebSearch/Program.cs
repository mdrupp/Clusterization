using System;
using Yandex;

namespace SearchRequster
{
    public class Program
    {
        static void Main(string[] args)
        {

            string baseUrl = @"http://xmlsearch.yandex.com";
            string requestUrl = @"xmlsearch?user=m-drupp&key=03.133254026:798c6a88fcfd81f0a408cc3f63e39b3b&sortby=rlv&filter=none";

            Console.WriteLine("We are starting! Now is " + DateTime.Now);
            var web = new YandexWebSearchManager(new YandexSearchEngine(new Uri(baseUrl), requestUrl));
            web.Process();

            Console.WriteLine("10 000 requests done!");
            Console.ReadKey();
        }
    }
}
