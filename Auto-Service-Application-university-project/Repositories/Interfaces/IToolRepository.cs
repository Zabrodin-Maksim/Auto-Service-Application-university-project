using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IToolRepository
    {
        IEnumerable<Tool> GetAll();
        Tool GetById(int id);
        void Add(Tool tool);
        void Update(Tool tool);
        void Delete(int id);
    }
}
