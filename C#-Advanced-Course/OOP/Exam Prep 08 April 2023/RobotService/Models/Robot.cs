using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private readonly List<int> interfaceStandarts;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            BatteryLevel = batteryCapacity;
            ConvertionCapacityIndex = convertionCapacityIndex;
            interfaceStandarts = new List<int>();
        }

        public string Model
        { get => model;

            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model cannot be null or empty.");
                }
                    model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Battery capacity cannot drop below zero.");
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel
        {
            get;

            private set;
            
        }

        public int ConvertionCapacityIndex
        {
            get;

            private set;
        }

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandarts.AsReadOnly();
        

        public void Eating(int minutes)
        {
           var result =  ConvertionCapacityIndex * minutes;

            if(result >= BatteryCapacity - BatteryLevel) 
            {
                BatteryCapacity = BatteryLevel;      
            }
            BatteryLevel += result;

        }

        public bool ExecuteService(int consumedEnergy)
        {
            
            if(BatteryLevel >= consumedEnergy)
            {
                batteryCapacity -= consumedEnergy;
                return true;
            }
            return false;
        
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandarts.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }

        public override string ToString()
        {
            StringBuilder   sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            string suppInstalled = InterfaceStandards.Any()
                ? $"{string.Join(" ", InterfaceStandards)}"
                : "none";

            sb.AppendLine($"--Supplements installed: {suppInstalled}");
            return sb.ToString().TrimEnd();
        }
    }
}
