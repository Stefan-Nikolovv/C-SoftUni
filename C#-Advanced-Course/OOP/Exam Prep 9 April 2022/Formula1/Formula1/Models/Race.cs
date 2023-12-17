using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models.Contracts;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string racename;
        private int numberOfLaps;
        private ICollection<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get { return racename; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Invalid race name: {racename}.");
                }
                racename = value;
            }
        }

        public int NumberOfLaps 
        {
            get {return numberOfLaps;}
            private set
            {
                if(value < 1)
                {
                    throw new ArgumentException($"Invalid lap numbers: { numberOfLaps }.");
                }
                numberOfLaps = value;
            }
        }

        public bool TookPlace { get; set; }

        public ICollection<IPilot> Pilots 
        {  get { return pilots; }
            private set
            {
                pilots = value;
            }
        }

        public void AddPilot(IPilot pilot)
        {
            Pilots.Add(pilot);
        }

        public string RaceInfo()
        {
           var sb = new StringBuilder();
            sb.AppendLine($"The { racename } race has:");
            sb.AppendLine($"Participants: { pilots.Count }");
            sb.AppendLine($"Number of laps: {numberOfLaps}");
            sb.AppendLine($"Took place: {(TookPlace ? "Yes" : "No")}");

            return sb.ToString().TrimEnd();
        }
    }
}
