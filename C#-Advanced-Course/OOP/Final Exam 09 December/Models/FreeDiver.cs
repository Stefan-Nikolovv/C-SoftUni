using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class FreeDiver : Diver
    {
        private const int freeDiverOxygenLevel = 120;

        public FreeDiver(string name) 
            : base(name, freeDiverOxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            double decreaseAmount = TimeToCatch * 0.6;
            int roundedDecreaseAmount = (int)Math.Round(decreaseAmount);
            OxygenLevel -= roundedDecreaseAmount;
        }

        public override void RenewOxy()
        {
            OxygenLevel = freeDiverOxygenLevel;
        }
    }
}
