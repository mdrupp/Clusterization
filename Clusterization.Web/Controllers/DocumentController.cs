using System.Linq;
using System.Web.Mvc;
using CatalogData;
using Clustering.Web.Models;

namespace Clustering.Web.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/

        public ActionResult Index(int id)
        {
            using (var dbo = new BiblioEntities())
            {
                var model = dbo.Documents.Where(x => x.Id == id).Select(book=>new DocumentModel()
                    {
                        Title = book.Title,
                        Id = id,
                        AdditionalTitle = book.AdditionalTitle,
                        Author = book.Author,
                        BBK = book.BBK,
                        City = book.City,
                        DocumentType = book.DocumentType.TypeName,
                        Edition = book.Edition,
                        Editor = book.Editor,
                        Pages = book.Pages,
                        Publish = book.Publish,
                        Serial = book.Serial,
                        Year = book.ImprintYear
                    }).First();
                model.ClusterId = dbo.ClusteredBooks.Where(x => x.Document.Id == id).Select(x => x.ClusterId).First();
                return View(model);
            }
        }
    }
}
