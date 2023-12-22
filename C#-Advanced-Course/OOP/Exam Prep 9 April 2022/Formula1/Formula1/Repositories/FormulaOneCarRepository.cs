using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private List<IFormulaOneCar> formulaOneCars;

        public FormulaOneCarRepository()
        {
            formulaOneCars = new List<IFormulaOneCar>();
        }
        public IReadOnlyCollection<IFormulaOneCar> Models => formulaOneCars.AsReadOnly();

        public void Add(IFormulaOneCar model)
        {
            formulaOneCars.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            return formulaOneCars.FirstOrDefault(x => x.Model == name);
        }

        public bool Remove(IFormulaOneCar model)
        {

            return formulaOneCars.Remove(model);
        }
    }
}
