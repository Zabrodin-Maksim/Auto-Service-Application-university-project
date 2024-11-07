using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public ObservableCollection<Payment> Payments { get; set; }

        public PaymentViewModel(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            Payments = new ObservableCollection<Payment>(_paymentRepository.GetAll());
        }
    }
}
