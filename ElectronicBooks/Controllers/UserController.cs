using ElectronicBooks.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicBooks.Controllers
{
    public class UserController : Controller
    {
        List<User> ListOfRegisteredUsers = new List<User>();

        [HttpGet]
        public ActionResult LogInView()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogInView(User loggedUser)
        {
            if (Session["UserList"] == null)
            {
                Session["UserList"] = ListOfRegisteredUsers;
            }

            ListOfRegisteredUsers= (List<User>)Session["UserList"];
            loggedUser.UserId = Guid.NewGuid();
            ListOfRegisteredUsers.Add(loggedUser);
            Session["UserList"] = ListOfRegisteredUsers;
            return RedirectToAction("ListOfBooks", "Book");
        }

    }
}