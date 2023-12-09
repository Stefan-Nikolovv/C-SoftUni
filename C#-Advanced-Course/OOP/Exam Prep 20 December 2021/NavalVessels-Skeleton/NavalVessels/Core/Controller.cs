using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private IRepository<IVessel> vessels;
        private ICollection<ICaptain> captains;

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new HashSet<ICaptain>();
        }
        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captainToAssign = captains
                .FirstOrDefault(c => c.FullName == selectedCaptainName);
            if (captainToAssign == null)
            {
                return $"Captain {selectedCaptainName} could not be found.";
            }
            IVessel vessel = vessels
                .FindByName(selectedVesselName);

            if (vessel == null)
            {
                return $"Vessel {selectedVesselName} could not be found.";
            }
            if (vessel.Captain != null)
            {
                return $"Vessel {selectedVesselName} is already occupied.";
            }
            vessel.Captain = captainToAssign;
            captainToAssign.AddVessel(vessel);

            return $"Captain {selectedCaptainName} command vessel {selectedVesselName}.";
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var atackVessel = vessels.FindByName(attackingVesselName);
            var defenceVessel = vessels.FindByName(defendingVesselName);

            if (atackVessel == null)
            {
                return $"Vessel {atackVessel.Name} could not be found.";
            }
            if (defenceVessel == null)
            {
                return $"Vessel {defenceVessel.Name} could not be found.";
            }
            if (atackVessel.ArmorThickness == 0)
            {
                return $"Unarmored vessel {atackVessel.Name} cannot attack or be attacked.";
            }

            if (defenceVessel.ArmorThickness == 0)
            {
                return $"Unarmored vessel {defenceVessel.Name} cannot attack or be attacked.";
            }
            atackVessel.Attack(defenceVessel);
            return $"Vessel {defenceVessel.Name} was attacked by vessel {atackVessel.Name} - current armor thickness: {defenceVessel.ArmorThickness}.";

        }
        public string CaptainReport(string captainFullName)
        {
            ICaptain capitan = captains.First(x => x.FullName == captainFullName);

            return capitan.Report();
        }

        public string HireCaptain(string fullName)
        {
            var searchedCapitan = captains.Any(x => x.FullName == fullName);
            if (searchedCapitan)
            {
                return $"Captain {fullName} is already hired.";
            }
            ICaptain captain = new Captain(fullName);
            captains.Add(captain);
            return $"Captain {fullName} is hired.";
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            var serachedVessel = vessels.FindByName(name);
            if (serachedVessel != null)
            {
                return $"{vesselType} vessel {name} is already manufactured.";
            }
            IVessel vessel = null;

            if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            vessels.Add(vessel);
            return $"{vesselType} {name} is manufactured with the main weapon caliber of {mainWeaponCaliber} inches and a maximum speed of {speed} knots.";
        }

        public string ServiceVessel(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }
            vessel.RepairVessel();
            return $"Vessel {vesselName} was repaired.";
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }
            if (vessel.GetType() == typeof(Battleship))
            {
                Battleship battleshipVessel = (Battleship)vessel;
                battleshipVessel.ToggleSonarMode();
                return $"Battleship {vesselName} toggled sonar mode.";
            }
            else
            {
                Submarine submarineVessel = (Submarine)vessel;
                submarineVessel.ToggleSubmergeMode();
                return $"Submarine {vesselName} toggled submerge mode.";
            }
        }

        public string VesselReport(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            return vessel.ToString();
        }
    }

   
}
