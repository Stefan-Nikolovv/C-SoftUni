
using System.ComponentModel.DataAnnotations;
using Footballers.Common;
using Newtonsoft.Json;

namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamDto
    {
        [JsonProperty("Name")]
        [Required]
        [MinLength(ValidationConstants.TeamNameMinLength)]
        [MaxLength(ValidationConstants.TeamNameMaxLength)]
        public string Name { get; set; }
        [JsonProperty("Nationality")]
        [MinLength(ValidationConstants.TeamNationalityMinLength)]
        [MaxLength(ValidationConstants.TeamNationalityMaxLength)]
        [Required]
        public string Nationality { get; set; }
        [JsonProperty("Trophies")]
        [Required]
        public int Trophies { get; set; }
        [JsonProperty("Footballers")]

        public int[] Footballers { get; set; }
    }
}
