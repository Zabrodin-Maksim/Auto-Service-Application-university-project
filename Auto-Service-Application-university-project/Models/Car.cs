using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Auto_Service_Application_university_project.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string SPZ { get; set; }
        public string CarBrand { get; set; }
        public string Symptoms { get; set; }
        public Reservation Reservation { get; set; }

        public override string ToString()
        {
            return $"{SPZ}, {CarBrand}, reservation: {Reservation}";
        }
    }
}
