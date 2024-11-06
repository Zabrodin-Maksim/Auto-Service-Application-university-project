using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IOfficeRepository
    {
        IEnumerable<Office> GetAll();
        Office GetById(int id);
        void Add(Office office);
        void Update(Office office);
        void Delete(int id);
    }
}
