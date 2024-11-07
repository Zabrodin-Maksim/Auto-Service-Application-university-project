using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface ICheckkRepository
    {
        IEnumerable<Checkk> GetAll();
        Checkk GetById(int id);
        void Add(Checkk checkk);
        void Update(Checkk checkk);
        void Delete(int id);
    }
}
