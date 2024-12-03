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
    public class ServiceTypeViewModel
    {
        private ServiceTypeRepository _servisTypeRepository;

        public ServiceTypeViewModel()
        {
            _servisTypeRepository = new ServiceTypeRepository();
        }

        public async Task<ObservableCollection<ServiceType>> GetAllServiceTypes()
        {
            return await _servisTypeRepository.GetAllServiceTypesAsync();
        }
    }
}
