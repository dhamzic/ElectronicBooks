using ElectronicBooks.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicBooks.Views.Book
{
    public class ResponseMessageAfterBorrowingBook
    {
        public User User { get; set; }
        public String ResponseMessage { get; set; }
        public CatalogBook BookInformation { get; set; }
    }
}