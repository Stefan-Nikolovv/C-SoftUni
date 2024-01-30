using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.Agent;
namespace HouseRentingSystem.Data.Models
{
    public class Agent
    {
        public Agent()
        {
            this.ManagedHouses = new HashSet<House>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(AgentPhoneNumberMax)]
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }  
        public ICollection<House> ManagedHouses { get; set; }
    }
}