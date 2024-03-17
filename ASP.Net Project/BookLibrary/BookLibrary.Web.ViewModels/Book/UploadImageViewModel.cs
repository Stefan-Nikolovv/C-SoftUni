using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Web.ViewModels.Book
{
    public class UploadImageViewModel
    {
        
        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }
    }
}
