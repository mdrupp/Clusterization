using System.Collections.Generic;

namespace Clustering.Clusterization
{
    public interface ICluster<T>
    {
        IList<T> Items { get; }
    }
}
