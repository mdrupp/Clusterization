using System.Collections.Generic;
using Clustering.Clusterization;

namespace Clustering
{
    public class ClusterizationManager<T>
    {
        public readonly IClusterization<T> Algorithm;

        public ClusterizationManager(IClusterization<T> algorithm)
        {
            Algorithm = algorithm;
        }

        public ClusterizationResult<T> DoClustering(int clustersCount, IList<T> documents)
        {
            return Algorithm.Clusterize(documents);
        }
    }
}
