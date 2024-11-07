using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IToolRepository
    {
        IEnumerable<Tool> GetAll();
        Tool GetById(int id);
        void Add(Tool tool);
        void Update(Tool tool);
        void Delete(int id);
    }
}
