using System.Collections.Generic;

namespace Clustering.Clusterization
{
    public class CentroidCluster<T> : ICluster<T>
    {
        //public CentroidCluster(T centroid, IEnumerable<T> items)
        //{
        //    Centroid = centroid;
        //    Items = items;
        //}

        public T Centroid { get; set; }

        public IList<T> Items { get; set; }
    }
}
