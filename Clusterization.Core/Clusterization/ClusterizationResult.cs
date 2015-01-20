using System.Collections.Generic;

namespace Clustering.Clusterization
{
    public class ClusterizationResult<T>
    {
        public IList<CentroidCluster<T>> Clusters { get; set; }

        public double Cost { get; set; }
    }
}
