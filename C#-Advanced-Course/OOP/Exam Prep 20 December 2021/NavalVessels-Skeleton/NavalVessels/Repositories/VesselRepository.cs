using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private ICollection<IVessel> vessels;

        public VesselRepository()
        {
            vessels = new HashSet<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models => (IReadOnlyCollection<IVessel>)vessels;

        public void Add(IVessel model)
        {
            this.vessels.Add(model);
        }

        public IVessel FindByName(string name)
        {
           return this.vessels.FirstOrDefault(v => v.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return this.vessels.Remove(model);
        }
    }
}
