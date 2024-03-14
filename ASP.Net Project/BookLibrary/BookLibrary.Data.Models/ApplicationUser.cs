
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using static BookLibrary.Common.EntityValidationsConstants.User;

namespace BookLibrary.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            LikedBooks = new HashSet<Book>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public virtual ICollection<Book> LikedBooks { get; set; }
    }
}
