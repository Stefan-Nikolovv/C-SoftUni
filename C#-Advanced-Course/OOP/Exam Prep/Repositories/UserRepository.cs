using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories
{
    internal class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;

        public UserRepository()
        {
            users = new List<IUser>();
        }
        public void AddModel(IUser model)
        {
            this.users.Add(model);
        }

        public IUser FindById(string identifier)
        {
            var user = this.users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
            return user;
        }

        public IReadOnlyCollection<IUser> GetAll()
        {
            return this.users;
        }

        public bool RemoveById(string identifier)
        {
           var user = this.FindById(identifier);
            return users.Remove(user);
        }
    }
}
