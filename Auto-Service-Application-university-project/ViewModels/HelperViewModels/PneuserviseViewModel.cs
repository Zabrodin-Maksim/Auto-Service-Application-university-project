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
    public class PneuserviseViewModel : ViewModelBase
    {
        private readonly IPneuserviseRepository _pneuserviseRepository;

        public ObservableCollection<Pneuservise> Pneuservices { get; set; }

        public PneuserviseViewModel(IPneuserviseRepository pneuserviseRepository)
        {
            _pneuserviseRepository = pneuserviseRepository;
            Pneuservices = new ObservableCollection<Pneuservise>(_pneuserviseRepository.GetAll());
        }
    }
}
