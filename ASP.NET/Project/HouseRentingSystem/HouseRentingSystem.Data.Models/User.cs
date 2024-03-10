using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HouseRentingSystem.Common.EntityValidationConstants.User;
namespace HouseRentingSystem.Data.Models
{
    public class User : IdentityUser<Guid>
    {

        public User() 
        { 
            this.Id = Guid.NewGuid();
            this.RentedHouses = new HashSet<House>();
        }
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;
        public virtual ICollection<House> RentedHouses { get; set; }
    }
}
