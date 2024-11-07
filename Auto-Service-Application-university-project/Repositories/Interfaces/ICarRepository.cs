using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car GetBySpz(string spz);
        void Add(Car car, User currentUser);
        void Update(Car car, User currentUser);
        void Delete(string spz, User currentUser);
    }
}
