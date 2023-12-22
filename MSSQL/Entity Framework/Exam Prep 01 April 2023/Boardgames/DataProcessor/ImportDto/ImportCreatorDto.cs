using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Creator")]
    public class ImportCreatorDto
    {
        [XmlElement("FirstName")]
        [MinLength(2)]
        [MaxLength(7)]
        [Required]
        public string FirstName { get; set; }
        [XmlElement("LastName")]
        [MinLength(2)]
        [MaxLength(7)]
        [Required]
        public string LastName { get; set; }
        [XmlArray("Boardgames")]
        public ImportBoardGameDto[] importBoardGames { get; set; } 
    }
}
