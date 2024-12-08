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
    public  class PaymentDataViewModel
    {
        private PaymentRepository _paymentRepository;

        private FileStorageRepository _fileStorageRepository;

        public PaymentDataViewModel()
        {
            _paymentRepository = new PaymentRepository();
        }

        public async Task AddPayment(Payment payment)
        {
            await _paymentRepository.InsertPaymentAsync(payment);
        }

        public async Task UpdatePayment(Payment payment)
        {
            await _paymentRepository.UpdatePaymentAsync(payment);
        }

        public async Task DeletePayment(int paymentId)
        {
            await _paymentRepository.DeletePaymentAsync(paymentId);
        }

        public async Task<ObservableCollection<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }

        public async Task AddFile(FileStorage file)
        {
            await _fileStorageRepository.InsertFileAsync(file);
        }
    }
}
