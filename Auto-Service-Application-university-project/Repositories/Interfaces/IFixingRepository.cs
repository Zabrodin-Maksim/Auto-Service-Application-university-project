using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IFixingRepository
    {
        IEnumerable<Fixing> GetAll();
        Fixing GetById(int id);
        void Add(Fixing fixing);
        void Update(Fixing fixing);
        void Delete(int id);
    }
}
