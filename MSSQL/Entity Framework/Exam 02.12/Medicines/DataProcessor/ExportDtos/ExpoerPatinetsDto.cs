

using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExpoerPatinetsDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlAttribute("Gender")]
        public string Gender { get; set; }
        [XmlElement("AgeGroup")]
        public string AgeGroup { get; set; }
        [XmlElement("Medicines")]
        public ExportMedicineDto[] ExportMedicines { get; set; }

    }
}
