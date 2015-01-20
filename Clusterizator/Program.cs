using System.Collections.Generic;
using System.Linq;
using CatalogData;
using Clustering;

namespace Clusterizator
{
    class Program
    {
        private static void Main(string[] args)
        {
            var clustersCount = 2000;
            var error = 0.01;


            var clusterManager = new ClusterizationManager<Document>(new KMeansAlgorithm(clustersCount, new SimpleContainsMetrics(), error));
            List<Document> documents;
            using (var dbo = new BiblioEntities())
            {
                documents = dbo.Documents.Where(x => x.Id % 7 == 0 && x.DocumentTypeId == 14).ToList();
            }
            var clusters = clusterManager.DoClustering(10, documents).Clusters;

            using (var dbo = new BiblioEntities())
            {
                var centroids = clusters;
                foreach (var item in centroids.Select(x => new CentroidCluster() { CentroidId = x.Centroid.Id }))
                {
                    dbo.CentroidClusters.Add(item);
                }
                dbo.SaveChanges();

                foreach (var cluster in centroids)
                {
                    foreach (var book in cluster.Items)
                    {
                        dbo.ClusteredBooks.Add(new ClusteredBook
                            {
                                ClusterId = dbo.CentroidClusters.First(x => x.CentroidId == cluster.Centroid.Id).Id,
                                DocumentId = book.Id
                            });
                    }

                    dbo.SaveChanges();
                }
            }
        }
    }
}
