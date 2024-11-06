using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface ICashRepository
    {
        IEnumerable<Cash> GetAll();
        Cash GetById(int id);
        void Add(Cash cash);
        void Update(Cash cash);
        void Delete(int id);
    }
}
