
using ElectronicBooks.Operations;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace ElectronicBooks.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public ActionResult ListOfBooks()
        {
            string xmlInputData = string.Empty;

            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            path = path + @"\books.xml";

            xmlInputData = System.IO.File.ReadAllText(path);

            Catalog ct = Serialization.Deserialize<Catalog>(xmlInputData);
            List<CatalogBook> listOfBooks = new List<CatalogBook>((CatalogBook[])ct.ArrayOfBooks);
            return View(listOfBooks);
        }

    }
}