using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }
        public string Speciality { get; set; }
        public decimal Price { get; set; }
        public int StockAvailability { get; set; }
        public int OfficeOfficeId { get; set; }
    }
}
