using System.ComponentModel.DataAnnotations;
using Boardgames.Commons;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellerDto
    {
        [JsonProperty("Name")]
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }
        [JsonProperty("Address")]
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Address { get; set; }
        [JsonProperty("Country")]
        [Required]
        public string Country { get; set; }
        [JsonProperty("Website")]
        [Required]
        [RegularExpression(ValidationConstants.SelerWebSiteRegex)]
        public string Website { get; set; }
        [JsonProperty("Boardgames")]
        public int[] Boardgames { get; set; }

    }
}
