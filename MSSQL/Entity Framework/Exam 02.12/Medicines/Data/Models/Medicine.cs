using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.DynamicProxy.Generators.Emitters;
using Medicines.Common;
using Medicines.Data.Models.Enums;

namespace Medicines.Data.Models
{
    public class Medicine
    {
        public Medicine()
        {
            this.PatientsMedicines = new HashSet<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.MedicineNameMaxLength)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        [Required]
        public DateTime ExpiryDate  { get; set; }
        [Required]
        [MaxLength(ValidationConstants.MedicineProducerMaxLength)]
        public string Producer { get; set; }
        public int PharmacyId { get; set; }
        [ForeignKey(nameof(PharmacyId))]
        public Pharmacy Pharmacy { get; set; }
        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }

    }
}