using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
  
    public class EventParticipant
    {
        [Required]
        [ForeignKey(nameof(Helper))]
        public string HelperId { get; set; }
        public IdentityUser Helper { get; set; }
        [ForeignKey(nameof(Event))]
        [Required]
        public int EventId { get; set; }    
        public  Event Event { get; set; }
    }
}