using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class ScubaDiver : Diver
    {
        private const int freeDiverScubaDiver = 540;

        public ScubaDiver(string name) 
            : base(name, freeDiverScubaDiver)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            double decreaseAmount = TimeToCatch * 0.3;
            int roundedDecreaseAmount = (int)Math.Round(decreaseAmount);
            OxygenLevel -= roundedDecreaseAmount;
        }

        public override void RenewOxy()
        {
            OxygenLevel = freeDiverScubaDiver;
        }
    }
}
