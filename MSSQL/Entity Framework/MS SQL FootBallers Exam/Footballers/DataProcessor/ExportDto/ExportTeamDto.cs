

using Newtonsoft.Json;

namespace Footballers.DataProcessor.ExportDto
{
    public class ExportTeamDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        public ExportFootBallersDto[] Footballers { get; set; }
    }
}
