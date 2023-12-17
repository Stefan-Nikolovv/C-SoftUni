using Invoices.Common;
using Invoices.Data.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    public class ExportProductDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [JsonProperty("Category")]
        public CategoryType Category { get; set; } 
        [JsonProperty("Clients")]
        public ExportClientDto[] Clients { get; set; }
    }
}
