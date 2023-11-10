using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> robots;

        public RobotRepository()
        {
            robots = new List<IRobot>();
        }
        public void AddNew(IRobot model)
        {
           robots.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return robots.FirstOrDefault(x => x.InterfaceStandards.Contains(interfaceStandard));
        }

        public IReadOnlyCollection<IRobot> Models()
        {
         return robots.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
           return robots.Remove(robots.FirstOrDefault(s => s.GetType().Name == typeName));
        }
    }
}
