

using System.Collections.Generic;
using System.Linq;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private List<ITeam> _team;
        public TeamRepository()
        {
             _team = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models
        {
            get { return _team.AsReadOnly(); }
        }

        public void AddModel(ITeam model)
        {
            _team.Add(model);
        }

        public bool ExistsModel(string name)
        {
            return _team.Any(t => t.Name == name);
        }

        public ITeam GetModel(string name)
        {
            var isExist = _team.FirstOrDefault(t => t.Name == name);

            if (isExist != null)
            {
                return isExist;
            }
            return null;
        }

        public bool RemoveModel(string name)
        {
            var isExist = _team.FirstOrDefault(t => t.Name == name);

            if (isExist != null)
            {
                _team.Remove(isExist);
                return true;
            }
            return false;
        }
    }
}
