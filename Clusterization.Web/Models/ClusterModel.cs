using System.Collections.Generic;

namespace Clustering.Web.Models
{
    public class ClusterModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<DocumentModel> Books { get; set; }
    }
}
