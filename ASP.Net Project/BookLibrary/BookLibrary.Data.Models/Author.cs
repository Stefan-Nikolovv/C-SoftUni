
using System.ComponentModel.DataAnnotations;
using static BookLibrary.Common.EntityValidationsConstants.Author;


namespace BookLibrary.Data.Models
{
    public class Author
    {
        public Author()
        {

            Id = Guid.NewGuid();
            this.ManagedBooks = new HashSet<Book>();
        }
        public Guid Id { get; set; }
        [Required]
        [MaxLength(AuthorFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(AuthorLastNameMaxLength)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(AuthorPhoneNumberMax)]
        public string PhoneNumber { get; set; }

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Book> ManagedBooks { get; set; }
    }
}
