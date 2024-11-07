using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ObservableCollection<Reservation> Reservations { get; set; }

        public ReservationViewModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
            Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
        }
    }
}
