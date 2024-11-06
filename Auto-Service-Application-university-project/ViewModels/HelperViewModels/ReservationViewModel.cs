using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.ViewModels.HelperViewModels
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
