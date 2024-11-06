using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
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
