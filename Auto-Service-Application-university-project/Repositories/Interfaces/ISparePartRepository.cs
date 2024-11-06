using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
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
