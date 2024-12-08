using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }
        public string Speciality { get; set; }
        public decimal Price { get; set; }
        public char StockAvailability { get; set; } // 'Y' or 'N'
        public Office Office { get; set; }

        public override string ToString()
        {
            return $"{Speciality}, Price {Price}, Stock Availability {StockAvailability}, Office {Office}";
        }
    }
}
