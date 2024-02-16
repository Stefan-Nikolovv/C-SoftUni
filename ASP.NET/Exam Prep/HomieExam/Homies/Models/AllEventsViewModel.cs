using Microsoft.AspNetCore.Identity;

namespace Homies.Models
{
    public class AllEventsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start {  get; set; }
        public string Type { get; set; }

        public string Organiser { get; set; } 
    }
}
