
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicBooks.Users
{
    public class User
    {
        public Guid UserId { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public List<CatalogBook> BorrowedBooks = new List<CatalogBook>();
    }
}