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
    public class BillViewModel
    {
        private BillRepository _billRepository;

        public BillViewModel() 
        {
            _billRepository = new BillRepository();
        }

        public async Task AddBill(Bill bill)
        {
            await _billRepository.InsertBillAsync(bill);
        }

        public async Task UpdateBill(Bill bill)
        {
            await _billRepository.UpdateBillAsync(bill);
        }

        public async Task DeleteBillAsync(int billId)
        {
            await _billRepository.DeleteBillAsync(billId);
        }

        public async Task<Bill> GetBillByIdAsync(int billId)
        {
            return await _billRepository.GetBillByIdAsync(billId);
        }

        public async Task<ObservableCollection<Bill>> GetAllBillsAsync()
        {
            return await _billRepository.GetAllBillsAsync();
        }
    }
}
