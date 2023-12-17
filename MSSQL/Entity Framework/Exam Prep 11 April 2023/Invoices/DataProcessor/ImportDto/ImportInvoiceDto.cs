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
    public class ImportInvoiceDto
    {
        [JsonProperty("Number")]
        [Required]
        [Range(ValidationConstants.InvoiceNumberMinLength, ValidationConstants.InvoiceNumberMaxLength)]
        public  int Number { get; set; }
        [JsonProperty("IssueDate")]
        [Required]
        public DateTime IssueDate { get; set; }
        [JsonProperty("DueDate")]
        [Required]
        public DateTime DueDate { get; set; }
        [JsonProperty("Amount")]
        [Required]
        public decimal Amount { get; set; }
        [JsonProperty("CurrencyType")]
        [Required]
        [Range(0, 2)]
        public int CurrencyType { get; set; }
        [JsonProperty("ClientId")]
        [Required]
        public int ClientId { get; set; }
    }
}





