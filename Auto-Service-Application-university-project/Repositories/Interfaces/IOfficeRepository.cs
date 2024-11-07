using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IOfficeRepository
    {
        IEnumerable<Office> GetAll();
        Office GetById(int id);
        void Add(Office office);
        void Update(Office office);
        void Delete(int id);
    }
}
