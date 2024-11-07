using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IEmployerRepository
    {
        IEnumerable<Employer> GetAll();
        Employer GetByPhone(string phone);
        void Add(Employer employer, User currentUser);
        void Update(Employer employer, User currentUser);
        void Delete(string phone, User currentUser);
    }
}
