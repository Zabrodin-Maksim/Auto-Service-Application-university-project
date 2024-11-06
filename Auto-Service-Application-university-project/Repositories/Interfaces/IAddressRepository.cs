using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAll();
        Address GetById(int id);
        void Add(Address address);
        void Update(Address address);
        void Delete(int id);
    }
}
