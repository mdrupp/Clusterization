using System;
using System.Collections.Generic;
using System.Linq;
using CatalogData;
using Clustering.Clusterization;
using Clustering.Metrics;

namespace Clusterizator
{
    public class KMeansAlgorithm : IClusterization<Document>
    {
        public KMeansAlgorithm(int clustersCount, IMetrics<Document> metrics, double error = 0)
        {
            _clustersCount = clustersCount;
            Deviation = error;
            _metrics = metrics;
        }

        private static readonly Random Random = new Random();

        private readonly IMetrics<Document> _metrics;
        private readonly int _clustersCount;

        private ClusterizationResult<Document> _clusterizationResult;

        //TODO: is this necessary?
        public int MaxItemsPerCluster { get; set; }

        public double Deviation { get; set; }

        public ClusterizationResult<Document> Clusterize(IList<Document> data)
        {
            _clusterizationResult = new ClusterizationResult<Document>();

            double error, currError;
            var flag = false;
            int iter = 0;

            Console.WriteLine("We are started!\n");

            Console.WriteLine("Initializing clusters, time = {0}", DateTime.Now);
            //initialize centres of clusters
            InitializeClusters(data);

            Console.WriteLine("Starting iterations, time = {0}", DateTime.Now);
            do
            {
                error = currError = 0d;
                flag = false;

                //refer documents to the relevant clusters
                BuildClusters(data);

                var oldCentroids = _clusterizationResult.Clusters.Select(x => x.Centroid).ToList();
                var newCentroids = _clusterizationResult.Clusters.Select(cluster => _metrics.GetCentroid(cluster.Items)).ToList();

                var emptyCentroids = newCentroids.Count(x => x == null);
                //find new centroids in clusters
                for (int i = 0; i < _clustersCount; i++)
                {
                    if (newCentroids[i] == null)
                    {
                        flag = true;

                        int index;
                        do
                        {
                            index = Random.Next(0, data.Count);
                        } while (newCentroids.Any(x => data.IndexOf(x) == index));

                        newCentroids[i] = data[index];
                    }

                    if (newCentroids[i].Id != oldCentroids[i].Id) flag = true;

                    _clusterizationResult.Clusters[i].Centroid = newCentroids[i];
                    _clusterizationResult.Clusters[i].Items.Clear();
                    _clusterizationResult.Clusters[i].Items = new List<Document> { newCentroids[i] };
                    currError = _metrics.Distance(newCentroids[i], oldCentroids[i]);
                    error += currError;

                    using (var dbo = new BiblioEntities())
                    {
                        dbo.ClusterErrors.Add(new ClusterError()
                            {
                                ClusterNumber = i,
                                Iteration = iter + 1,
                                NewCentroidId = newCentroids[i].Id,
                                OldCentriodId = oldCentroids[i].Id,
                                Error = currError,
                                Date = DateTime.Now,
                            });
                        dbo.SaveChanges();
                    }

                }

                if (!flag)
                {
                    break;
                }
                iter++;
                Console.WriteLine("Cluster #{0}! Error = {1}, empty = {2} Time = {3}", iter, error, emptyCentroids, DateTime.Now.ToLongTimeString());

            } while (error >= Deviation);

            BuildClusters(data);

            return _clusterizationResult;
        }

        private void BuildClusters(IList<Document> data)
        {
            foreach (var document in data)
            {
                if (_clusterizationResult.Clusters.All(x => x.Centroid.Id != document.Id))
                {
                    int temp = 0;

                    var minDistance = double.PositiveInfinity;
                    foreach (var cluster in _clusterizationResult.Clusters)
                    {
                        var dist = _metrics.Distance(cluster.Centroid, document);
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            temp = cluster.Centroid.Id;
                        }
                    }

                    _clusterizationResult.Clusters.First(x => x.Centroid.Id == temp).Items.Add(document);
                }
            }
        }

        private void InitializeClusters(IList<Document> data)
        {
            var indexes = new List<int>() { Random.Next(0, data.Count) };
            for (int i = 1; i < _clustersCount; i++)
            {
                int index;
                do
                {
                    index = Random.Next(0, data.Count);
                } while (indexes.Any(x => x == index));

                indexes.Add(index);
            }

            _clusterizationResult.Clusters = new List<CentroidCluster<Document>>();
            foreach (var ind in indexes)
            {
                _clusterizationResult.Clusters.Add(new CentroidCluster<Document>()
                {
                    Centroid = data[ind],
                    Items = new List<Document> { data[ind] }
                });
            }
        }

        public string AlgorithmDescription { get; set; }
    }
}
