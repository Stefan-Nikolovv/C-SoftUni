using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Handball.Models.Contracts;

namespace Handball.Models
{
    public  class Team : ITeam
    {
        private string name;
        private int pointsEarned = 0;
        public string Name 
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Team name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int PointsEarned 
        {  get 
            {
                return pointsEarned;
            }
            private set
            {
                pointsEarned = value;
            }
        }
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            players = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Players { get { return players.AsReadOnly(); } }

        public double OverallRating 
        {
            get
            {
                if(players.Count == 0)
                {
                    return 0;
                }
                return Math.Round(players.Average(p => p.Rating), 2);
            }
        }

        

        public void Draw()
        {
            PointsEarned += 1;
          IPlayer player = players.FirstOrDefault(p => p is Goalkeeper);
            if(player != null)
            {
                player.IncreaseRating();
            }
        }

        public void Lose()
        {
            foreach (IPlayer player in players)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            players.Add(player);
        }

        public void Win()
        {
            PointsEarned += 3;
            foreach(IPlayer player in players)
            {
                player.IncreaseRating();
            }
        }
        public override string ToString()
        {
            var sb =  new StringBuilder();

            string playerString = "none";
            if(players.Count > 0 )
            {
                playerString = String.Join(", ", players.Select(p => p.Name));
            }
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            sb.AppendLine($"--Players: {playerString}");

            return base.ToString();
        }
    }
}
