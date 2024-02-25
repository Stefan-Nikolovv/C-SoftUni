using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Data.Models
{
    public class User : IdentityUser<Guid>
    {

        public User() 
        { 
            this.Id = Guid.NewGuid();
            this.RentedHouses = new HashSet<House>();
        }
        public virtual ICollection<House> RentedHouses { get; set; }
    }
}
