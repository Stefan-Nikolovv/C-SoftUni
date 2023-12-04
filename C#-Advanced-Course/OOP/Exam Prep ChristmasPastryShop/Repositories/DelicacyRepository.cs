using System;
using System.Collections.Generic;
using System.Text;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories.Contracts;

namespace ChristmasPastryShop.Repositories
{
    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private List<IDelicacy> delicacyList;

        public DelicacyRepository()
        {
              delicacyList = new List<IDelicacy>();
        }
        public IReadOnlyCollection<IDelicacy> Models => delicacyList.AsReadOnly();

        public void AddModel(IDelicacy model)
        {
            delicacyList.Add(model);
        }
    }
}
