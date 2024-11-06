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
    public class CashViewModel : ViewModelBase
    {
        private readonly ICashRepository _cashRepository;

        public ObservableCollection<Cash> Cashes { get; set; }

        public CashViewModel(ICashRepository cashRepository)
        {
            _cashRepository = cashRepository;
            Cashes = new ObservableCollection<Cash>(_cashRepository.GetAll());
        }
    }
}
