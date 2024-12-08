using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class ServiceOffer
    {
        public int OfferId { get; set; }
        public int? PricePerHour { get; set; }
        public DateTime? DateOffer { get; set; }
        public Employer Employer { get; set; } 
        public Car Car { get; set; } 
        public ServiceType ServiceType { get; set; } 
        public int? WorkingHours { get; set; }
         
        // Специфичные сервисные данные
        public string Speciality { get; set; } // for 'service_checks', 'fixing', 'others'
        public int? RadiusWheel { get; set; } // for 'pneuservise'

        // Info for List
        public string CarInfo { get => Car.SPZ + " " + Car.CarBrand + " " + Car.Symptoms ; }
        public string DateReservation { get => Car.Reservation.DateReservace.ToString("dd.MM.yyyy"); }
        public string Typ { get => ServiceType.ToString(); }

        public string SmallInfoReservation { get => $"{Car.SPZ} {Car.Reservation.Client.ClientName}"; }
        public string InfoEmployer { get => Employer.NameEmployee; }

        public override string ToString()
        {
            return $"Price Per Hour {PricePerHour},Working Hours {WorkingHours}, Date Offer {DateOffer}, Car {Car.SPZ}, Service Type {ServiceType}, ";
        }
    }
}
