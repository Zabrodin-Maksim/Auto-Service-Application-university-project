using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetByDate(DateTime date);
        void Add(Reservation reservation, User currentUser);
        void Update(Reservation reservation, User currentUser);
        void Delete(DateTime date, User currentUser);
    }
}
