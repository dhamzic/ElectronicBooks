
using ElectronicBooks.Operations;
using ElectronicBooks.Users;
using ElectronicBooks.Views.Book;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            ///Check if User exists, if a user doesn't exist, redirect page to log in page.
            if (Session["UserList"] == null)
            {
                return RedirectToAction("LogInView", "User");
            }

            ///Check if Session exists, if the session does not exist, create it and fill it with data. If Exist, do not fill it, just return its object.
            if (Session["listOfBooksObject"] == null)
            {
                Catalog ct = Serialization.Deserialize<Catalog>(FetchBookXmlStringFromFile());
                listOfBooks = new List<CatalogBook>((CatalogBook[])ct.ArrayOfBooks);
                Session["listOfBooksObject"] = listOfBooks;
            }
            return View((List<CatalogBook>)Session["listOfBooksObject"]);
        }

        [HttpGet]
        public ActionResult Return(string bookId)
        {
            ///Check if User exists, if a user doesn't exist, redirect page to log in page.
            if (Session["UserList"] == null)
            {
                return RedirectToAction("LogInView", "User");
            }

            User currentUser = (User)Session["CurrentActiveUser"];
            listOfBooks = (List<CatalogBook>)Session["listOfBooksObject"];
            CatalogBook selectedBook = listOfBooks.Where(w => w.Id == bookId).FirstOrDefault();

            ///Response message
            ResponseMessageAfterBookOp rmabo = new ResponseMessageAfterBookOp()
            {
                User = currentUser,
                BookInformation = selectedBook
            };


            if (selectedBook.UserWhoBorrowedBook != null)
            {
                if (selectedBook.UserWhoBorrowedBook.UserId == currentUser.UserId)
                {

                    listOfBooks.Where(w => w.Id == bookId).ToList().ForEach(s => s.UserWhoBorrowedBook = null);
                    Session["listOfBooksObject"] = listOfBooks;
                    rmabo.ResponseMessage = "You have successfully returned the book.";
                }
                else
                {
                    rmabo.ResponseMessage = "You can not return a book which you did not borrow.";
                }
            }
            else
            {
                rmabo.ResponseMessage = "You can not return a book which is not borrowed.";
            }
            return View(rmabo);
        }






        [HttpGet]
        public ActionResult Borrow(string bookId)
        {
            ///Check if User exists, if a user doesn't exist, redirect page to log in page.
            if (Session["UserList"] == null)
            {
                return RedirectToAction("LogInView", "User");
            }

            User currentUser = (User)Session["CurrentActiveUser"];

            listOfBooks = (List<CatalogBook>)Session["listOfBooksObject"];
            User userThatBorrowedBook = listOfBooks.Where(w => w.Id == bookId).Select(u => u.UserWhoBorrowedBook).FirstOrDefault();
            CatalogBook selectedBook = listOfBooks.Where(w => w.Id == bookId).FirstOrDefault();

            if (userThatBorrowedBook == null)
            {
                listOfBooks.Where(w => w.Id == bookId).ToList().ForEach(s => s.UserWhoBorrowedBook = currentUser);
                Session["listOfBooksObject"] = listOfBooks;


                ResponseMessageAfterBookOp rmabo = new ResponseMessageAfterBookOp()
                {
                    User = currentUser,
                    ResponseMessage = "You have successfully borrowed the book.",
                    BookInformation = selectedBook
                };
                return View(rmabo);
            }
            else if (currentUser.UserId == userThatBorrowedBook.UserId)
            {
                ResponseMessageAfterBookOp rmabb = new ResponseMessageAfterBookOp()
                {
                    User = currentUser,
                    ResponseMessage = "You have already borrowed this book.",
                    BookInformation = selectedBook
                };
                return View(rmabb);
            }
            else
            {
                ResponseMessageAfterBookOp rmabb = new ResponseMessageAfterBookOp()
                {
                    User = userThatBorrowedBook,
                    ResponseMessage = "Currently, you can not borrow this book because the selected book is already borrowed.",
                    BookInformation = selectedBook
                };
                return View(rmabb);
            }
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