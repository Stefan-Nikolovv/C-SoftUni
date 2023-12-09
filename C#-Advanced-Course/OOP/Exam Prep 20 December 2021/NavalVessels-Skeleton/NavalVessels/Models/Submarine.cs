
using System.Text;
using NavalVessels.Models.Contracts;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        public Submarine(string Name, double mainWeaponCaliber, double speed) 
            : base(Name, mainWeaponCaliber, speed, 200)
        {
            this.SubmergeMode = false;
        }
       
        public bool SubmergeMode 
        {
            get; private set;
 
        }

        public override void RepairVessel()
        {
           this.ArmorThickness = 200;
        }

        public void ToggleSubmergeMode()
        {
           if(this.SubmergeMode == false)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
            else
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
           this.SubmergeMode = !this.SubmergeMode;
        }
        public override string ToString()
        {
            string submergeMode = this.SubmergeMode ? "ON" : "OFF";

            var sb = new StringBuilder();

            sb.AppendLine($" *Sonar mode: {submergeMode}");

            return sb.ToString().TrimEnd();
        }
    }
}
