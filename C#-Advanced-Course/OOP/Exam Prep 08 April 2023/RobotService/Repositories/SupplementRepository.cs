using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private readonly List<ISupplement> supplements;
        public SupplementRepository()
        {
            supplements = new List<ISupplement>();
        }
        public void AddNew(ISupplement model)
        {
            supplements.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        }

        public IReadOnlyCollection<ISupplement> Models()
        {
            return supplements.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            var removedSupp = supplements.FirstOrDefault(x => x.GetType().Name == typeName);
            if(removedSupp != null)
            {
                supplements.Remove(removedSupp);
                return true;
            }
            return false;
          
            
        }
    }
}
