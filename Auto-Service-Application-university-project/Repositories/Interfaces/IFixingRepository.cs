using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
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
