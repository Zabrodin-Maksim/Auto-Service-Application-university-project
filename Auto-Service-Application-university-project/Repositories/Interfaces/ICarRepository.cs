using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
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
