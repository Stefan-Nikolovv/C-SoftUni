

using System.ComponentModel.DataAnnotations;
using Medicines.Common;

namespace Medicines.Data.Models
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            this.Medicines = new HashSet<Medicine>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.PharmacyNameMaxLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(ValidationConstants.PharmacyPhoneNumberLength)]
        public string PhoneNumber  { get; set; }
        [Required]
        public bool IsNonStop  { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
