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
        public User CurrentUser { get; set; }

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
            ListOfRegisteredUsers = (List<User>)Session["UserList"];

            if (ListOfRegisteredUsers.Count >0)
            {
                User existingUser = ListOfRegisteredUsers.Where(w => w.Name == loggedUser.Name && w.LastName == loggedUser.LastName).FirstOrDefault();
                if (existingUser == null)
                {
                    //New User
                    loggedUser.UserId = Guid.NewGuid();
                    ListOfRegisteredUsers.Add(loggedUser);

                    Session["CurrentActiveUser"] = loggedUser;
                    Session["UserList"] = ListOfRegisteredUsers;
                    Session["UserStatus"] = "User: "+loggedUser.Name+" "+loggedUser.LastName+" Is created.";
                }
                else
                {
                    Session["CurrentActiveUser"] = existingUser;
                    Session["UserStatus"] = "User: " + loggedUser.Name + " " + loggedUser.LastName + " Is logged in.";
                }
                return RedirectToAction("ListOfBooks", "Book");
            }
            else
            {
                loggedUser.UserId = Guid.NewGuid();
                ListOfRegisteredUsers.Add(loggedUser);

                Session["CurrentActiveUser"] = loggedUser;
                Session["UserList"] = ListOfRegisteredUsers;
                Session["UserStatus"] = "User: " + loggedUser.Name + " " + loggedUser.LastName + " Is created.";
                return RedirectToAction("ListOfBooks", "Book");
            }



        }

    }
}