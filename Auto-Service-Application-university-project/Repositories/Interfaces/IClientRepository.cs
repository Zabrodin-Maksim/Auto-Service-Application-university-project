using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client GetByPhone(string phone);
        void Add(Client client, User currentUser);
        void Update(Client client, User currentUser);
        void Delete(string phone, User currentUser);
    }
}
