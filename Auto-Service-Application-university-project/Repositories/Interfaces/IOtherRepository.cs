using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IOtherRepository
    {
        IEnumerable<Other> GetAll();
        Other GetById(int id);
        void Add(Other other);
        void Update(Other other);
        void Delete(int id);
    }
}
