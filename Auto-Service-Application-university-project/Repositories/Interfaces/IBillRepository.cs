using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IBillRepository
    {
        IEnumerable<Bill> GetAll();
        Bill GetById(int id);
        void Add(Bill bill);
        void Update(Bill bill);
        void Delete(int id);
    }
}
