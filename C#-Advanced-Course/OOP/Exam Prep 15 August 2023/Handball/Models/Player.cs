

using System;
using System.Text;
using Handball.Models.Contracts;

namespace Handball.Models
{
    public abstract class Player : IPlayer
    {
        private string name;
        private string team;
        private double rating;

        protected Player(string name, double rating)
        {
            Name = name;
            Rating = rating;
        }

        public string Name
        { 
            get { return name; } 
            private set 
            {

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Player name cannot be null or empty.");
                }
                    name = value; 
            }
        }

        public double Rating
        {
            get { return rating; }
            protected set
            {
                if(value > 10)
                {
                    rating = 10;
                    return;
                }else if(value < 1)
                {
                    rating = 1;
                    return;
                }
                rating = value;
            }
        }

        public string Team
        {
            get;
            private set;
        }

        public abstract void DecreaseRating();


        public abstract void IncreaseRating();
        

        public void JoinTeam(string name)
        {  
            team = name;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {Name}");
           sb.AppendLine($"--Rating: {rating}");

            
            return sb.ToString().TrimEnd();
        }
    }
}
