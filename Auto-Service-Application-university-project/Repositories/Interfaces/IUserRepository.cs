using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
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
