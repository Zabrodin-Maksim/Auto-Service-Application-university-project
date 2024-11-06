using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IEmployerRepository
    {
        IEnumerable<Employer> GetAll();
        Employer GetByPhone(string phone);
        void Add(Employer employer, User currentUser);
        void Update(Employer employer, User currentUser);
        void Delete(string phone, User currentUser);
    }
}
