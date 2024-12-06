using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class PaymentTypeViewModel
    {
        private PaymentTypeRepository _paymentTypeRepository;

        public PaymentTypeViewModel()
        {
            _paymentTypeRepository = new PaymentTypeRepository();
        }

        public async Task<ObservableCollection<PaymentType>> GetAllPaymentTypes()
        {
            return await _paymentTypeRepository.GetAllPaymentTypesAsync();
        }
    }
}
