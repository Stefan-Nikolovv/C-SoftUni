using System.ComponentModel.DataAnnotations;
using static BookLibrary.Common.EntityValidationsConstants.Category;
namespace BookLibrary.Data.Models
{
    public class Category
    {
        public Category()
        {
            //this.Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
        //public virtual ICollection<Book> Books { get; set; } = null!;
    }
}
