using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Common;
using static System.Net.Mime.MediaTypeNames;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Address")]
    public class ImportAddressDto
    {
        [XmlElement("StreetName")]
        [Required]
        [MinLength(ValidationConstants.AddressStreetNameMinLength)]
        [MaxLength(ValidationConstants.AddressStreetNameMaxLength)]
        public string StreetName { get; set; }
        [XmlElement("StreetNumber")]
        [Required]
        public int StreetNumber { get; set; }
        [XmlElement("PostCode")]
        [Required]
        public string PostCode { get; set; }
        [XmlElement("City")]
        [Required]
        [MinLength(ValidationConstants.AddressCityMinLength)]
        [MaxLength(ValidationConstants.AddressCityMaxLength)]
        public string City { get; set; }
        [XmlElement("Country")]
        [Required]
        [MinLength(ValidationConstants.AddressCountryMinLength)]
        [MaxLength(ValidationConstants.AddressCountryMaxLength)]
        public string Country { get; set; }
    }

}
