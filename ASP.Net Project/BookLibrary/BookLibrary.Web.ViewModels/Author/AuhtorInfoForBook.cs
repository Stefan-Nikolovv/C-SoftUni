using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Web.ViewModels.Author
{
    public class AuhtorInfoForBook
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone")]

        public string PhoneNumber { get; set; }
    }
}
