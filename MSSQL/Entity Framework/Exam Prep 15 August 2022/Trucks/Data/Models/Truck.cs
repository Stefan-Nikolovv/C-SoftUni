

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Common;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models
{
    public class Truck
    {
        public Truck()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(ValidationConstants.TruckRegistrationNumberLength)]
        public  string? RegistrationNumber { get; set; }
        [MaxLength(ValidationConstants.TruckVinNumberLength)]
        [Required]
        public string VinNumber { get; set; } = null!;
        [MaxLength()]
        public int TankCapacity { get; set; }
        [MaxLength()]
        public int CargoCapacity { get; set; }
       
        public CategoryType CategoryType { get; set; }
        
        public MakeType MakeType { get; set; }
        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId { get; set; }
        
        public Despatcher Despatcher { get; set; } = null!;
        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
    }
}
