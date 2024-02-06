using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class HouseDeleteDetailsViewModel
    {
        public string Title  { get; set; }
        public string Address { get; set; }
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; }
    }
}
