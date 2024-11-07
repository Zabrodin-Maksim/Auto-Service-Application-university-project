using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IServiceSpareRepository
    {
        IEnumerable<ServiceSpare> GetAll();
        ServiceSpare GetByIds(int sparePartId, int serviseOfferId);
        void Add(ServiceSpare serviceSpare);
        void Update(ServiceSpare serviceSpare, int oldSparePartId, int oldServiseOfferId);
        void Delete(int sparePartId, int serviseOfferId);
    }
}
