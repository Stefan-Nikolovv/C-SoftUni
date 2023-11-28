

using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientXmlDto
    {
        [XmlElement("ClientName")]
        public string ClientName { get; set; }
       
        [XmlElement("VatNumber")]
        public string VatNumber { get; set; }
        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }
        [XmlArray("Invoices")]
        public ExportInvoiceDto[] Invoices { get; set; }

    }
}
