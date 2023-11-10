using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;

namespace RobotService.Models
{
    public class SpecializedArm : Supplement
    {
        private const int SpecializedArmInterfaceStandard = 10045;
        private const int SpecializedArmBatteryUsage = 10000;

        public SpecializedArm() 
            : base(SpecializedArmInterfaceStandard, SpecializedArmBatteryUsage)
        {
        }
    }
}
