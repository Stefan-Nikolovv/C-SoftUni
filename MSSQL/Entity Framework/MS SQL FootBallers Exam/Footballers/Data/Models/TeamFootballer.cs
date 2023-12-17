using System.ComponentModel.DataAnnotations.Schema;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {
        [ForeignKey(nameof(TeamId))]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [ForeignKey(nameof(FootballerId))]
        public int FootballerId { get; set; }
        public Footballer Footballer { get; set; }
    }
}