using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client GetByPhone(string phone);
        void Add(Client client, User currentUser);
        void Update(Client client, User currentUser);
        void Delete(string phone, User currentUser);
    }
}
