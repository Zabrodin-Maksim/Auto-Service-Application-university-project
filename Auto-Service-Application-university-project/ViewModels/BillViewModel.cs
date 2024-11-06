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
