using System;
using System.Collections.Generic;

namespace Clusterization.Common
{
    public static class Parser
    {
        public static IList<string> ParseWords(List<string> strings, StringSplitOptions options)
        {
            var dictionary = new List<string>();
            foreach (var str in strings)
            {
                dictionary.AddRange(str.Split(new[] { ' ', ',', ':', ';', '!', '?', '(', ')', '.', '[', ']' }, options));
            }

            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary[i] = dictionary[i].Trim(new[] { '\'', '-', '\\', '/', '<', '>', '\r', '\n', '&', '_', '=', '"' }).ToLowerInvariant();
                if (string.IsNullOrEmpty(dictionary[i]))
                {
                    dictionary.RemoveAt(i);
                    i--;
                }
            }
            return dictionary;
        }

        public static IList<string> ParseWords(string str, StringSplitOptions options)
        {
            var dictionary = new List<string>();

            dictionary.AddRange(str.Split(new[] { ' ', ',', ':', ';', '!', '?', '(', ')', '.', '[', ']' }, options));

            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary[i] = dictionary[i].Trim(new[] { '\'', '-', '\\', '/', '<', '>', '\r', '\n', '&', '_', '=', '"' }).ToLowerInvariant();
                if (string.IsNullOrEmpty(dictionary[i]))
                {
                    dictionary.RemoveAt(i);
                    i--;
                }
            }
            return dictionary;
        }
    }
}
