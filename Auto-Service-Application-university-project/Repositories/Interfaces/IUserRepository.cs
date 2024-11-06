using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IUserRepository
    {
        User GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(string username);
        IEnumerable<User> GetAll();
    }
}
