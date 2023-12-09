using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavalVessels.Models.Contracts;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {


        private string _name;

        
        public string Name 
        {
            get { return _name; } 
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Vessel name cannot be null or empty.");
                }
                _name = value;
            }
        }
        private ICaptain captain; 
        public ICaptain Captain 
        { get { return captain; }
            set
            {
                if(value == null)
                {
                    throw new NullReferenceException("Captain cannot be null.");
                }
                captain = value;
            }
        }

        private double armorThickness;
        public double ArmorThickness { get;  set; }
        

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

       

        protected Vessel(string Name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            _name = Name;
            ArmorThickness = armorThickness;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            Targets =  new List<string>();
        }

        public ICollection<string> Targets
        {
            get;
            private set; 
            
        }

        public void Attack(IVessel target)
        {
           if(target == null)
            {
                throw new NullReferenceException("Target cannot be null.");
            }
            target.ArmorThickness -= this.armorThickness;
            if(target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }
            Targets.Add(target.Name);
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            string targetsOutput = Targets.Any() ? String.Join(", ", Targets) : "None";
            var sb = new StringBuilder();
            sb.AppendLine($"- {this.Name}")
            .AppendLine($" *Type: {this.GetType().Name}")
            .AppendLine($" *Armor thickness: {this.ArmorThickness}")
            .AppendLine($" *Main weapon caliber: {this.MainWeaponCaliber}")
            .AppendLine($" *Speed: {this.Speed} knots")
            .AppendLine($" *Targets: {targetsOutput}");

            return sb.ToString().TrimEnd();
        }

    }
}
