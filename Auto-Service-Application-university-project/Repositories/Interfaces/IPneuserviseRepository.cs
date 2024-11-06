using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IPneuserviseRepository
    {
        IEnumerable<Pneuservise> GetAll();
        Pneuservise GetById(int id);
        void Add(Pneuservise pneuservise);
        void Update(Pneuservise pneuservise);
        void Delete(int id);
    }
}
