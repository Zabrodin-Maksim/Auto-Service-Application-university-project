using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IOtherRepository
    {
        IEnumerable<Other> GetAll();
        Other GetById(int id);
        void Add(Other other);
        void Update(Other other);
        void Delete(int id);
    }
}
