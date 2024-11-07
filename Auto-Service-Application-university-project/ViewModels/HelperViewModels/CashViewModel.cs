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
