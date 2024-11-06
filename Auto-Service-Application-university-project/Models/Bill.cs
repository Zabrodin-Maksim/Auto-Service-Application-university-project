using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int ServiseOfferOfferId { get; set; }
        public DateTime DateBill { get; set; }
        public decimal Price { get; set; }
    }
}
