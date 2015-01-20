using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogData;
using Clustering.Web.Models;

namespace Clustering.Web.Controllers
{
    public class ClusterController : Controller
    {
        //
        // GET: /Cluster/5

        public ActionResult Index(int id)
        {
            var model = new ClusterModel() {Id = id};

            using (var dbo = new BiblioEntities())
            {
                model.Title = dbo.CentroidClusters.Where(x => x.Id == id).Select(x => x.Document.Title).First();

                var books = dbo.ClusteredBooks.Where(x => x.ClusterId == id).Select(x => x.Document);
                model.Books = books.Select(book => new DocumentModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        BBK = book.BBK,
                    }).ToList();
            }

            return View(model);
        }
    }
}
