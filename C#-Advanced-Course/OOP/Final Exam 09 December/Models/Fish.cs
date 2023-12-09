using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NauticalCatchChallenge.Models.Contracts;

namespace NauticalCatchChallenge.Models
{
    public abstract class Fish : IFish
    {
        private string _name;
        private double _points;

        protected Fish(string _name, double _points, int timeToCatch)
        {
            Name = _name;
            Points = _points;
            TimeToCatch = timeToCatch;
        }

        public string Name 
        { 
            get { return _name; } 
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Fish name should be determined.");
                }
                _name = value;
            }

        }

        public double Points 
        {
            get { return _points; }
            private set
            {
                if(value < 1.0 && value > 10.0)
                {
                    throw new ArgumentException("Points must be a numeric value, between 1 and 10.");
                }
                _points = value;
            }
        }

        public int TimeToCatch { get; private set; }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {Name} [ Points: {Points}, Time to Catch: {TimeToCatch} seconds ]";
        }
    //    if (value< 1 || value> 10 || Math.Round(value, 1) != value)
    //        {
    //            throw new ArgumentException("Points must be a numeric value, between 1 and 10 with at most one decimal place.");
    //}
    //points = value;
    }
}
