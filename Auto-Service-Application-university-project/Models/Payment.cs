using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BillBillId { get; set; }
        public int ClientClientId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
