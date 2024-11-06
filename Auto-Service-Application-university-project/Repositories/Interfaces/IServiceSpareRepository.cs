using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IServiceSpareRepository
    {
        IEnumerable<ServiceSpare> GetAll();
        ServiceSpare GetByIds(int sparePartId, int serviseOfferId);
        void Add(ServiceSpare serviceSpare);
        void Update(ServiceSpare serviceSpare, int oldSparePartId, int oldServiseOfferId);
        void Delete(int sparePartId, int serviseOfferId);
    }
}
