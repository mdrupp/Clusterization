using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clustering.Web.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string AdditionalTitle { get; set; }

        public string Author { get; set; }

        public string Editor { get; set; }

        public string Serial { get; set; }
        public string Edition { get; set; }

        public string BBK { get; set; }

        public string DocumentType { get; set; }

        public string Publish { get; set; }

        public string City { get; set; }
        public string Year { get; set; }

        public string Pages { get; set; }

        public int ClusterId { get; set; }

    }
}