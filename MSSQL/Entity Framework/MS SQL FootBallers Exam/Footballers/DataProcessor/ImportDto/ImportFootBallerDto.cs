using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Common;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Footballer")]
    public class ImportFootBallerDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.FootballerNameMinLength)]
        [MaxLength(ValidationConstants.FootballerNameMaxLength)]
        public string Name { get; set; }
        [Required]
        [XmlElement("ContractStartDate")]
        public string ContractStartDate { get; set; }
        [Required]
        [XmlElement("ContractEndDate")]
        public string ContractEndDate { get; set; }
        [Required]
        [XmlElement("BestSkillType")]
        [Range(ValidationConstants.FootballerBestSkillMinLength, ValidationConstants.FootballerBestSkillTypeMaxLength)]
        public int BestSkillType { get; set; }
        [Required]
        [XmlElement("PositionType")]
        [Range(ValidationConstants.FootballerPositionTypeMinLength, ValidationConstants.FootballerPositionTypeMaxLength)]
        public int PositionType { get; set; }
    }
}