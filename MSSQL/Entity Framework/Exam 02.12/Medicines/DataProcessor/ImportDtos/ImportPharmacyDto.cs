using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Medicines.Common;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [XmlAttribute("non-stop")]
        public string IsNonStop { get; set; }
        [XmlElement("Name")]
        [MinLength(ValidationConstants.PharmacyNameMinLength)]
        [MaxLength(ValidationConstants.PharmacyNameMaxLength)]
        [Required]
        public string Name { get; set; }
        [XmlElement("PhoneNumber")]
        [Required]
        [RegularExpression(ValidationConstants.PharmacyPhoneNumberRegex)]
        public string PhoneNumber { get; set; }
        [XmlArray("Medicines")]
        public ImporMedicinesDto[] ImportMedicines { get; set; }
    }
}
