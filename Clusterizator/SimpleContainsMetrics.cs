using System;
using System.Collections.Generic;
using System.Linq;
using CatalogData;
using Clustering.Metrics;
using Clusterization.Common;

namespace Clusterizator
{
    public class SimpleContainsMetrics : IMetrics<Document>
    {
        public double Distance(Document v1, Document v2)
        {
            if (v1 == null || v2 == null)
            {
                throw new ArgumentException("Vectors can not be null");
            }

            var words1 = Parser.ParseWords(v1.Title, StringSplitOptions.RemoveEmptyEntries);
            var words2 = Parser.ParseWords(v2.Title, StringSplitOptions.RemoveEmptyEntries);

            if (words1.SequenceEqual(words2))
            {
                return v1.Id == v2.Id ? 0 : 1;
            }

            var sum = 0d;
            foreach (var s in words1)
            {
                sum += words2.Count(x => x.Equals(s));
            }

            if (sum.Equals(0d))
            {
                return Math.Pow(words1.Count, 2) + Math.Pow(words2.Count, 2);
            }

            var n = words1.Count + words2.Count;
            var near = sum * 2d / n;

            return 1 / near;
        }

        public Document GetCentroid(IList<Document> clusterItems)
        {
            if (clusterItems.Count == 1)
            {
                return null;
            }

            var bestCentroid = clusterItems[0];
            var minSum = double.PositiveInfinity;
            for (int i = 0; i < clusterItems.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < clusterItems.Count; j++)
                {
                    if (i != j)
                    {
                        sum += Math.Pow(Distance(clusterItems[i], clusterItems[j]), 2);
                    }
                }

                if (sum < minSum)
                {
                    minSum = sum;
                    bestCentroid = clusterItems[i];
                }
            }

            return bestCentroid;
        }
    }
}
