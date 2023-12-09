using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;

namespace NauticalCatchChallenge.Repositories
{
    public class DiverRepository : IRepository<IDiver>
    {
        private List<IDiver> diver;

        public DiverRepository() 
        { 
            diver = new List<IDiver>();
        }
        public IReadOnlyCollection<IDiver> Models => diver.AsReadOnly();

        public void AddModel(IDiver model)
        {
            diver.Add(model);
        }

        public IDiver GetModel(string name)
        {
            return diver.FirstOrDefault(x => x.Name == name);
        }
    }
}
