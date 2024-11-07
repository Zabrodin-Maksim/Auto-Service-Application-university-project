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
    public class ServiceSpareViewModel : ViewModelBase
    {
        private readonly IServiceSpareRepository _serviceSpareRepository;

        public ObservableCollection<ServiceSpare> ServiceSpares { get; set; }

        public ServiceSpareViewModel(IServiceSpareRepository serviceSpareRepository)
        {
            _serviceSpareRepository = serviceSpareRepository;
            ServiceSpares = new ObservableCollection<ServiceSpare>(_serviceSpareRepository.GetAll());
        }
    }
}
