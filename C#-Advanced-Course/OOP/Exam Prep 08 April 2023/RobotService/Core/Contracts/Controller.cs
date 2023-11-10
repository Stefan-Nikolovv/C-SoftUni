using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;

namespace RobotService.Core.Contracts
{
    public class Controller : IController
    {
        IRepository<IRobot> robots;
        IRepository<ISupplement> supplements;

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            if(typeName != "DomesticAssistant" && typeName != "IndustrialAssistant")
            {
                return $"Robot type {typeName} cannot be created.";
            }
            if(typeName == "DomesticAssistant")
            {
                DomesticAssistant domesticRobot = new DomesticAssistant(model);
                robots.AddNew(domesticRobot);
            }
            if(typeName == "IndustrialAssistant")
            {
                IndustrialAssistant industrialAssistantRobo = new IndustrialAssistant(model);
                robots.AddNew(industrialAssistantRobo);
            }
            return $"{typeName} {model} is created and added to the RobotRepository.";
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != "SpecializedArm" && typeName != "LaserRadar")
            {
                return $"{typeName} is not compatible with our robots.";
            }
            if (typeName == "SpecializedArm")
            {
                Supplement SpecializedArmSupply = new SpecializedArm();
                supplements.AddNew(SpecializedArmSupply);
            }
            if (typeName == "LaserRadar")
            {
                Supplement LaserRadarSupply = new LaserRadar();
                supplements.AddNew(LaserRadarSupply);
            }
            return $"{typeName} is created and added to the SupplementRepository.";
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            IEnumerable<IRobot> robot = robots.Models().Where(x => x.InterfaceStandards.Contains(intefaceStandard))
                .OrderByDescending(x => x.BatteryLevel);
            if(!robot.Any())
            {
                return $"Unable to perform service, {intefaceStandard} not supported!";
            }
             int availablePower = robot.Sum(x => x.BatteryLevel);

            if(availablePower < totalPowerNeeded) 
            {
                return $"{serviceName} cannot be executed! {totalPowerNeeded - availablePower} more power needed.";
            }
            int count = 0;
            foreach (var rob in robot)
            {
                count++;
                if (rob.BatteryLevel >= totalPowerNeeded)
                {
                    rob.ExecuteService(totalPowerNeeded);
                    break;
                }
                totalPowerNeeded -= rob.BatteryLevel;
                rob.ExecuteService(totalPowerNeeded);
                

            }
            return $"{serviceName} is performed successfully with {count} robots.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<IRobot> robot = robots.Models()
               .OrderByDescending(x => x.BatteryLevel)
               .ThenBy(x => x.BatteryCapacity);

            foreach (var robo in robot)
            {
                sb.AppendLine(robo.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            IEnumerable<IRobot> robo = robots.Models().Where(x => x.Model == model);
            int count = 0;
            foreach (var robot in robo)
            {
                if(robot.BatteryLevel < robot.BatteryCapacity / 2)
                {
                    count++;
                    robot.Eating(minutes);
                }

            }
            return $"Robots fed: {count}";
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {

            ISupplement supply = supplements
                .Models()
                .FirstOrDefault(x => x.GetType().Name == supplementTypeName);

            IRobot robot = robots.Models()
                .FirstOrDefault(x => x.Model == model && !x.InterfaceStandards.Contains(supply.InterfaceStandard));

            if(robot is null)
            {
                return $"All {model} are already upgraded!";
            }else
            {
                robot.InstallSupplement(supply);
                supplements.RemoveByName(supplementTypeName);
                return $"{model} is upgraded with {supplementTypeName}.";
            }

        }
    }
}
