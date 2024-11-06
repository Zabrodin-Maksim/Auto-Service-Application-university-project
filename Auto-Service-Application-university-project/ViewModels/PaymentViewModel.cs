using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.ViewModels
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
