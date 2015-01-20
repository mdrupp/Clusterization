using System.Collections.Generic;

namespace Clustering.Metrics
{
    public interface IMetrics<T>
    {
        double Distance(T v1, T v2);

        T GetCentroid(IList<T> data);
    }
}
