
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicBooks.Users
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        public String Country { get; set; }

        public List<CatalogBook> BorrowedBooks = new List<CatalogBook>();
    }
}