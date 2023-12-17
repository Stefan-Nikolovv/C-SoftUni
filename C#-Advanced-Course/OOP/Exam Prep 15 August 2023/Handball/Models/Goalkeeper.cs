using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double goalKeeprInital = 2.5;
        public Goalkeeper(string name) 
            : base(name, goalKeeprInital)
        {
        }

        public override void DecreaseRating()
        {
            Rating -= 1.25;
        }

        public override void IncreaseRating()
        {
            Rating += 0.75;
        }
    }
}
