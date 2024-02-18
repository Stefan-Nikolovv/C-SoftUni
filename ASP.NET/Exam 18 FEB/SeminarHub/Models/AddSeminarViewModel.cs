using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace SeminarHub.Models
{
    public class AddSeminarViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Topic { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Lecturer { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Details { get; set; }
        [Required(ErrorMessage = "Date and Time are required")]
        public string DateAndTime { get; set; } = string.Empty;
        [Range(30, 180)]
        public int? Duration { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public List<AllViewCategories> Categories { get; set; } = new List<AllViewCategories>();
    }
}



//· Has Id – a unique integer, Primary Key

//· Has Topic – string with min length 3 and max length 100 (required)

//· Has Lecturer – string with min length 5 and max length 60 (required)

//· Has Details – string with min length 10 and max length 500 (required)

//· Has OrganizerId – string (required)

//· Has Organizer – IdentityUser (required)

//· Has DateAndTime – DateTime with format "dd/MM/yyyy HH:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)

//· Has Duration – integer value between 30 and 180

//· Has CategoryId – integer, foreign key (required)

//· Has Category – Category (required)

//· Has SeminarsParticipants – a collection of type SeminarParticipant