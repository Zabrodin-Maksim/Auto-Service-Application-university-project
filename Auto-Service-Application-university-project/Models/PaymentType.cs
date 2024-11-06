using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class PaymentType
    {
        public int PaymentTypeId { get; set; }      // Первичный ключ
        public string TypeName { get; set; }        // Название типа платежа
    }
}
