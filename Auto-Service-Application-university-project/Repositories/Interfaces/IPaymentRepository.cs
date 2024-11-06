using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetByBillId(int billId);
        void Add(Payment payment, User currentUser);
        void Update(Payment payment, User currentUser);
        void Delete(int billId, User currentUser);
    }
}
