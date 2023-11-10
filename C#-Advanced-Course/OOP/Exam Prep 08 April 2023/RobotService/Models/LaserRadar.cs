using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class LaserRadar : Supplement
    {
        private const int LazarRadarInterfaceStandard = 20082;
        private const int LazarRadarArmBatteryUsage = 5000;

        public LaserRadar()
            : base(LazarRadarInterfaceStandard, LazarRadarArmBatteryUsage)
        {
        }
    }
}
