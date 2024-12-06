using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public ServiceOffer ServiceOffer { get; set; }
        public DateTime DateBill { get; set; }
        public decimal Price { get; set; }

        public string InfoOfferReservation { get => ServiceOffer.SmallInfoReservation; }
        public string EmployerWasWork { get => ServiceOffer.InfoEmployer; }
        public string PriceAndDate { get => $"{Price.ToString()} {DateBill.ToString("dd.MM.yyyy")}"; }
    }
}
