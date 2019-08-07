
using ElectronicBooks.Operations;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ElectronicBooks.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public ActionResult ListOfBooks()
        {
            Catalog ct = Serialization.Deserialize<Catalog>(FetchBookXmlStringFromFile());
            List<CatalogBook> listOfBooks = new List<CatalogBook>((CatalogBook[])ct.ArrayOfBooks);
            return View(listOfBooks);
        }
        public string FetchBookXmlStringFromFile()
        {
            string xmlString = string.Empty;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("~/XML/books.xml"));
            return xmlString = xmldoc.InnerXml;
        }

    }
}