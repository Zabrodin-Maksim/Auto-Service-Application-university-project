using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetByDate(DateTime date);
        void Add(Reservation reservation, User currentUser);
        void Update(Reservation reservation, User currentUser);
        void Delete(DateTime date, User currentUser);
    }
}
