
using System.ComponentModel.DataAnnotations;
using static BookLibrary.Common.EntityValidationsConstants.Author;

namespace BookLibrary.Web.ViewModels.Author
{
    public class BecomeAuthorFormModel
    {
        [Required]
        [StringLength(AuthorFirstNameMaxLength, MinimumLength = AuthorFirstNameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(AuthorLastNameMaxLength, MinimumLength = AuthorLastNameMinLength)]
        public string LastName { get; set; }
        [Required]
        [StringLength(AuthorPhoneNumberMax, MinimumLength = AuthorPhoneNumberMin)]
        public string PhoneNumber { get; set; }
    }
}
