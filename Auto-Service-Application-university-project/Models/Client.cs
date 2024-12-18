using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public Address Address { get; set; }
        public int Phone { get; set; }

        public string ShortAdress { get => Address.City + " " + Address.Street + Address.HouseNumber; }

        public static bool IsSecredModeActive { get; set; }

        public override string ToString()
        {
            if (IsSecredModeActive)
            {
                return $"{ClientName}, Phone: +420 *********, Address: ***...";
            }
            else
            {
                return $"{ClientName}, Phone: +420 {Phone}, Address: {ShortAdress}";
            }
        }
    }
}
