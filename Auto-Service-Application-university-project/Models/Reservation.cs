using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateReservace { get; set; }
        public int OfficeOfficeId { get; set; }
        public int ClientClientId { get; set; }
    }
}
