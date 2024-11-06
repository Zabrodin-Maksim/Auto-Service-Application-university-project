using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface ICheckkRepository
    {
        IEnumerable<Checkk> GetAll();
        Checkk GetById(int id);
        void Add(Checkk checkk);
        void Update(Checkk checkk);
        void Delete(int id);
    }
}
