
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
        private List<CatalogBook> listOfBooks = new List<CatalogBook>();

        [HttpGet]
        public ActionResult ListOfBooks()
        {
            //Check if Session exists, if the session does not exists, create it and fill it with data. If Exists, do not fill it, just return its object.
   
            if (Session["listOfBooksObject"] == null)
            {
                Catalog ct = Serialization.Deserialize<Catalog>(FetchBookXmlStringFromFile());
                listOfBooks = new List<CatalogBook>((CatalogBook[])ct.ArrayOfBooks);
                Session["listOfBooksObject"] = listOfBooks;
            }
            return View((List<CatalogBook>)Session["listOfBooksObject"]);



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