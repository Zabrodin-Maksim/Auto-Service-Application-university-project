using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Bill Bill { get; set; }
        public Client Client { get; set; }
        public PaymentType PaymentType { get; set; }

        public Card Card { get; set; }

        public Cash Cash { get; set; }
    }
}
