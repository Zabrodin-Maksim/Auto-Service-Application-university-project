using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class ReservationViewModel
    {
        private ReservationRepository _reservationRepository;

        public ReservationViewModel()
        {
            _reservationRepository = new ReservationRepository();
        }

        public async Task AddReservation(Reservation reservation)
        {
            await _reservationRepository.InsertReservationAsync(reservation);
        }

        public async Task<ObservableCollection<Reservation>> GetAllReservations()
        {
            return await _reservationRepository.GetAllReservationsAsync();
        }
    }
}
