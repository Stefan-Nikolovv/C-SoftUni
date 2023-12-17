
using Footballers.Data.Models.Enums;
using Newtonsoft.Json;

namespace Footballers.DataProcessor.ExportDto
{
    public class ExportFootBallersDto
    {
        [JsonProperty("FootballerName")]
        public string FootballerName { get; set; }
        [JsonProperty("ContractStartDate")]
        public string  ContractStartDate { get; set; }
        [JsonProperty("ContractEndDate")]
        public string ContractEndDate { get; set; }
        [JsonProperty("BestSkillType")]
        public BestSkillType BestSkillType { get; set; }
        [JsonProperty("PositionType")]
        public PositionType PositionType { get; set; }
    }
}