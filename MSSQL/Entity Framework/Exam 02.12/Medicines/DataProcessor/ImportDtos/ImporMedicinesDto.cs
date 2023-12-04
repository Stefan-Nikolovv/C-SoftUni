

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Medicines.Common;
using Medicines.Data.Models.Enums;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImporMedicinesDto
    {
        [XmlAttribute("category")]
        [Required]
        [Range(0, 4)]
        public int Category { get; set; }
        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.MedicineNameMinLength)]
        [MaxLength(ValidationConstants.MedicineNameMaxLength)]
        
        public string Name { get; set; }
        [XmlElement("Price")]
        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }
        [Required]
        public string ProductionDate { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        [MinLength(ValidationConstants.MedicineProducerMinLength)]
        [MaxLength(ValidationConstants.MedicineProducerMaxLength)]
        public string Producer { get; set;}
    }

    

}
