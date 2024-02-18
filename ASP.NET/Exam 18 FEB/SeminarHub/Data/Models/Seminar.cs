using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {

        public Seminar()
        {
            this.SeminarsParticipants = new List<SeminarParticipant>(); 
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Topic { get; set; } = null!;
        [Required]
        [StringLength(60)]
        public string Lecturer { get; set; } = null!;
        [Required]
        [StringLength(500)]
        public string Details { get; set; } = null!;
        [Required]
      
        public string OrganizerId { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        public DateTime DateAndTime { get; set; }
      
     
        [Range(30, 180)]
        public int? Duration { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public List<SeminarParticipant> SeminarsParticipants { get; set; } 
    }
}
