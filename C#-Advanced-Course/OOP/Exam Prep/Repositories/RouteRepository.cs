using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;
        public RouteRepository()
        {
            routes = new List<IRoute>();
        }
        public void AddModel(IRoute model)
        {
            this.routes.Add(model);
        }

        public IRoute FindById(string identifier)
        {
            var intIdentifier =  int.Parse(identifier);
            return this.routes.FirstOrDefault(x => x.RouteId == intIdentifier);
        }

        public IReadOnlyCollection<IRoute> GetAll()
        {
            return this.routes;
        }

        public bool RemoveById(string identifier)
        {
            var route = this.FindById(identifier);
            return routes.Remove(route);    
        }
    }
}
