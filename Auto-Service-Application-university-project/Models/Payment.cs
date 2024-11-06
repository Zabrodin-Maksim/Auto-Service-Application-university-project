using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BillBillId { get; set; }
        public int ClientClientId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
