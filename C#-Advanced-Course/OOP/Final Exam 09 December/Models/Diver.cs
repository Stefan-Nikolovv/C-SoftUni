using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NauticalCatchChallenge.Models.Contracts;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private List<string> catchList;
        private double competitionPoints;
        private int oxygenLevel;

        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            CompetitionPoints = 0.0;
            catchList = new List<string>();
            HasHealthIssues = false;
        }

        public string Name
        {
            get { return name; }
           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Diver's name cannot be null or empty.");
                }
                name = value;
            }
        }
        //To check for private
        public int OxygenLevel 
        {  
            get { return oxygenLevel; }
           protected  set
            {
                if(value < 0)
                {
                    oxygenLevel = 0;
                }
                oxygenLevel = value;

            }
        }

        public IReadOnlyCollection<string> Catch
        {
            get { return catchList.AsReadOnly(); }
        }

        public double CompetitionPoints
        {
            get { return Math.Round(competitionPoints, 1); }
            private set { competitionPoints = value; }
        }

        public bool HasHealthIssues { get; private set; }

        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            catchList.Add(fish.Name);
            CompetitionPoints += fish.Points;
            if(OxygenLevel <= 0)
            {
                OxygenLevel = 0;
            }
        }

        public abstract void Miss(int TimeToCatch);


        public abstract void RenewOxy();
       

        public void UpdateHealthStatus()
        {
             HasHealthIssues = !HasHealthIssues;
        }
        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {catchList.Count}, Points earned: {CompetitionPoints} ]";
        }
    }
}
