
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.House;
namespace HouseRentingSystem.Data.Models
{
    public class House
    {
        public House()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(HouseTitleMax)]
        public string Title { get; set; }
        [Required]
        [MaxLength(HouseAddressMax)]
        public string Address { get; set; }
        [Required]
        [MaxLength(HouseDescriptionMax)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public decimal  PricePerMonth { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool isActive { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid AgentId { get; set; }
        public virtual Agent Agent { get; set; }
        public Guid? RenterId { get; set; }
        public User? Renter { get; set; }
    }
}
