using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CatalogData;
using Clusterization.Common;
using Yandex;
using Yandex.XmlRequest;

namespace SearchRequster
{
    public class YandexWebSearchManager
    {
        private readonly YandexSearchEngine _webSearch;
        //private int _availableRequests;

        public YandexWebSearchManager(YandexSearchEngine webSearchEngine)
        {
            _webSearch = webSearchEngine;
        }

        public void Process()
        {
            //TODO: replace this hardcode with the database calling (TitlesFrequency table in future)
            var list = DeserializeFromXml(@"d:\Titlefrequency.xml", typeof(List<Title>)) as List<Title>;

            if (list == null)
            {
                return;
            }
            var lastBookId = GetLastBook();

            int fromIndex1, fromIndex2;

            if (lastBookId == null)
            {
                fromIndex1 = fromIndex2 = 0;
            }
            else
            {
                fromIndex1 = list.FindIndex(x => x.Id == lastBookId.BookId);
                fromIndex2 = list.FindIndex(x => x.Id == lastBookId.ComparedWithBookId);

                if (fromIndex2 > 5000)
                {
                    fromIndex1 ++;
                    fromIndex2 = 0;
                }
            }

            for (int i = fromIndex1; i < list.Count; i++)
            {
                using (var dbo = new BiblioEntities())
                {
                    var book1Id = list[i].Id;
                    var book1 = dbo.Documents.First(x => x.Id == book1Id);
                    var title1 = book1.Title + " " + book1.AdditionalTitle;
                    var words1 = Parser.ParseWords(title1, StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine();
                    Console.WriteLine("==================================");
                    Console.WriteLine("{2} - We started a new book:\n {0} '{1}'", book1Id, title1, DateTime.Now);

                    for (int j = fromIndex2; j < list.Count; j++)
                    {
                        var book2Id = list[j].Id;
                        var book2 = dbo.Documents.First(x => x.Id == book2Id);
                        var title2 = book2.Title + " " + book2.AdditionalTitle;
                        var words2 = Parser.ParseWords(title2, StringSplitOptions.RemoveEmptyEntries);

                        Console.WriteLine();
                        Console.WriteLine("{2} - Working with the second book:\n {0} '{1}'", book2Id, title2, DateTime.Now);

                        if (words1.SequenceEqual(words2))
                        {
                            Console.WriteLine();
                            Console.WriteLine("{2} - The sequences of words are similar:\n '{0}' '{1}'", title1, title2, DateTime.Now);
                            continue;
                        }

                        foreach (var word1 in words1)
                        {
                            foreach (var word2 in words2)
                            {
                                var id1 = dbo.Words.First(x => x.Word1.Equals(word1)).Id;
                                var id2 = dbo.Words.First(x => x.Word1.Equals(word2)).Id;

                                if (dbo.SearchResults.Any(x => x.WordId1 == id1 && x.WordId2 == id2))
                                {
                                    continue;
                                }

                                var request = new YandexRequest
                                    {
                                        SortBy = new SortBy
                                            {
                                                Order = "descending",
                                                Value = "rlv"
                                            },
                                        Query = string.Format("\"{0}\" \"{1}\"", word1, word2)
                                    };

                                string content;
                                var result = _webSearch.Search(request, out content);

                                if (result.Response.Error == null)
                                {
                                    dbo.SearchResults.Add(new SearchResult
                                        {
                                            WordId1 = id1,
                                            WordId2 = id2,
                                            Count = result.Response.Found.First(x => x.Priority == "all").Count
                                        });

                                    dbo.Snippets.Add(new Snippet()
                                        {
                                            WordId1 = id1,
                                            WordId2 = id2,
                                            Snippet1 = content
                                        });

                                    dbo.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine(result.Response.Error.Text);

                                    if (result.Response.Error.Code == 15)
                                    {
                                        dbo.SearchResults.Add(new SearchResult
                                            {
                                                WordId1 = id1,
                                                WordId2 = id2,
                                                Count = 0
                                            });

                                        dbo.SaveChanges();
                                        continue;
                                    }

                                    var upd = new LastBookUpdate
                                        {
                                            BookId = list[i].Id,
                                            Updated = DateTime.Now,
                                            ComparedWithBookId = list[j].Id,
                                        };

                                    if (result.Response.Error.Code != 32)
                                    {
                                        upd.LastSnippet = string.Format("Error code {0}: {1}", result.Response.Error.Code, result.Response.Error.Text);
                                    }

                                    dbo.LastBookUpdates.Add(upd);
                                    dbo.SaveChanges();
                                    return;

                                }
                            }
                        }
                    }
                }
            }
        }

        private LastBookUpdate GetLastBook()
        {
            using (var dbo = new BiblioEntities())
            {
                var lastbook = dbo.LastBookUpdates.OrderByDescending(x => x.Updated);
                if (lastbook.Any())
                {
                    return lastbook.First();
                }

                //TODO: it's a terrible solution. Make a database call to TitlesFrequency table in future!
                return null;
            }
        }


        private static IEnumerable<Title> ComputeTitleFrequencies()
        {
            using (var dbo = new BiblioEntities())
            {
                //14 - it means that books will be selected only
                var books = dbo.Documents.Where(x => x.DocumentTypeId == 14).OrderBy(x => x.Id);
                var words = dbo.Words;
                var frequencies = dbo.WordsFrequencies;
                foreach (var book in books)
                {
                    var ws = Parser.ParseWords(book.Title, StringSplitOptions.RemoveEmptyEntries);
                    var sum = 0;
                    foreach (var w in ws)
                    {
                        var wordId = words.First(x => x.Word1.Equals(w)).Id;
                        sum += frequencies.First(y => y.WordId == wordId).Frequency;
                    }
                    yield return new Title
                    {
                        Id = book.Id,
                        Freq = sum
                    };
                }
            }
        }

        private static void SerializeToXml(string path, bool append, Encoding encoding, object obj, int bufferSize = 2*1024*1024)
        {
            var xml = new XmlSerializer(obj.GetType());
            using (var file = new StreamWriter(path, append, encoding ?? Encoding.UTF8, bufferSize))
            {
                xml.Serialize(file, obj);
            }
        }

        private static IEnumerable<Title> DeserializeFromXml(string path, Type objType)
        {
            IEnumerable<Title> res;

            var xml = new XmlSerializer(objType);
            using (var file = new StreamReader(path))
            {
                res = xml.Deserialize(file) as List<Title>;
            }

            return res;
        }
    }
}
