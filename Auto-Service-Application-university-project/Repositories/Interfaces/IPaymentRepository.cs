using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetByBillId(int billId);
        void Add(Payment payment, User currentUser);
        void Update(Payment payment, User currentUser);
        void Delete(int billId, User currentUser);
    }
}
