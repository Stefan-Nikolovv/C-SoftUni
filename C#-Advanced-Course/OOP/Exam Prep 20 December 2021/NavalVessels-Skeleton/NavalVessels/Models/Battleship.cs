

using System.Text;
using NavalVessels.Models.Contracts;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        public Battleship(string Name, double mainWeaponCaliber, double speed ) 
            : base(Name, mainWeaponCaliber, speed, 300)
        {
            this.SonarMode = false;
        }
        
        public bool SonarMode { get; private set; }

        public override void RepairVessel()
        {
            this.ArmorThickness = 300;
        }

        public void ToggleSonarMode()
        {
           if (this.SonarMode == false) 
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
                
            }
            this.MainWeaponCaliber -= 40;
            this.Speed += 5;

            this.SonarMode = !this.SonarMode;
        }
        public override string ToString()
        {
            string sonarMode = this.SonarMode ? "ON" : "OFF";

            var sb = new StringBuilder();

            sb.AppendLine($" *Sonar mode: {sonarMode}");

            return sb.ToString().TrimEnd();
        }
    }
}
