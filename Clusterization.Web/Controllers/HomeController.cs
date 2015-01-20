using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CatalogData;
using Clustering.Web.Models;

namespace Clustering.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            using (var dbo = new BiblioEntities())
            {
                var clusters = dbo.CentroidClusters.Where(x => !x.ParentId.HasValue).GroupBy(x => x.CentroidId);

                var list = new List<ClusterModel>();

                foreach (var cluster in clusters)
                {
                    var id = cluster.Select(x => x.Id).FirstOrDefault();
                    var title = cluster.Select(x => x.Document.Id + " - " + x.Document.Title).FirstOrDefault();

                    list.Add(new ClusterModel
                        {
                            Id = id, 
                            Title = title
                        });
                }
                
                return View("Index", list);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
