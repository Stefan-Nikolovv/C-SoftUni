using System.Xml.Serialization;
using Footballers.Data.Models.Enums;

namespace Footballers.DataProcessor.ExportDto
{
    [XmlType("Footballer")]
    public class ExportFootBallerDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Position")]
        public PositionType Position { get; set; } 
    }
}