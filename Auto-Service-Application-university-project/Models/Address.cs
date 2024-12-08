using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int IndexAdd { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public override string ToString()
        {
            return $"{Country}, {City}, {IndexAdd}, {Street}, {HouseNumber}";
        }
    }
}
