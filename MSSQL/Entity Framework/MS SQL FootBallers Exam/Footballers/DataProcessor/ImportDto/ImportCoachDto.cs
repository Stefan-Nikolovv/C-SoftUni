

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Common;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Coach")]
    public class ImportCoachDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.FootballerCoachMinLength)]
        [MaxLength(ValidationConstants.FootballerCoachMaxLength)]
        public string Name { get; set; }
        [XmlElement("Nationality")]
        [Required]
        public string Nationality { get; set; }
        [XmlArray("Footballers")]
        public ImportFootBallerDto[] Footballers { get; set; }
    }
}
