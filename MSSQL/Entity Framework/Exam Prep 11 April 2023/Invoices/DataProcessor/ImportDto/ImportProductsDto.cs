using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Common;
using Newtonsoft.Json;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportProductsDto
    {
        [JsonProperty("Name")]
        [Required]
        [MinLength(ValidationConstants.ProductNameMinLength)]
        [MaxLength(ValidationConstants.ProductNameMaxLength)]
        public  string Name { get; set; }
        [JsonProperty("Price")]
        [Required]
        [Range(5.00, 1000.00)]
        public decimal Price { get; set; }
        [JsonProperty("CategoryType")]
        [Required]
        [Range(0, 4)]
        public int CategoryType { get; set; }
        [JsonProperty("Clients")]
        public int[] Clients { get; set; }
    }
}
//{

//    "Name": "ADR plate",

//"Price": 14.97,

//"CategoryType": 1,

//"Clients": [

//]

//},
