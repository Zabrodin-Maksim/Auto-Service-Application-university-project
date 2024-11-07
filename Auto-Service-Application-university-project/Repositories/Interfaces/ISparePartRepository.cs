using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface ISparePartRepository
    {
        IEnumerable<SparePart> GetAll();
        SparePart GetById(int id);
        void Add(SparePart sparePart);
        void Update(SparePart sparePart);
        void Delete(int id);
    }
}
