using System.Collections.Generic;

namespace Clustering.Clusterization
{
    public interface IClusterization<T>
    {
        ClusterizationResult<T> Clusterize(IList<T> data);

        string AlgorithmDescription { get; set; }
    }
}
