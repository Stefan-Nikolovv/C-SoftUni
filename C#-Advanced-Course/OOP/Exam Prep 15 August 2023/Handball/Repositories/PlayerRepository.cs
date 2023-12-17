

using System.Collections.Generic;
using System.Linq;
using System.Net;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private List<IPlayer> _players;
        public PlayerRepository()
        {
             _players = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models { get { return _players.AsReadOnly(); } }

        public void AddModel(IPlayer model)
        {
            _players.Add(model);
        }

        public bool ExistsModel(string name)
        {
           return  _players.Any(p => p.Name == name);

            
        }

        public IPlayer GetModel(string name)
        {
            var isExist = _players.FirstOrDefault(p => p.Name == name);

            if (isExist != null)
            {
                return isExist;
            }
            return null;
        }

        public bool RemoveModel(string name)
        {
            var isExist = _players.FirstOrDefault(p => p.Name == name);

            if (isExist != null)
            {
                _players.Remove(isExist);
                return true;
            }
            return false;
        }
    }
}
