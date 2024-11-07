using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IPneuserviseRepository
    {
        IEnumerable<Pneuservise> GetAll();
        Pneuservise GetById(int id);
        void Add(Pneuservise pneuservise);
        void Update(Pneuservise pneuservise);
        void Delete(int id);
    }
}
