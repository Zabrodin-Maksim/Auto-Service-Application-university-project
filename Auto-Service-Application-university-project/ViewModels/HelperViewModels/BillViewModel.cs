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
    public class BillViewModel : ViewModelBase
    {
        private readonly IBillRepository _billRepository;

        public ObservableCollection<Bill> Bills { get; set; }

        public BillViewModel(IBillRepository billRepository)
        {
            _billRepository = billRepository;
            Bills = new ObservableCollection<Bill>(_billRepository.GetAll());
        }
    }
}
