using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class ServiceSpareViewModel
    {
        private ServiceSpareRepository _repository;

        public ServiceSpareViewModel()
        {
            _repository = new ServiceSpareRepository();
        }

        public async Task AddServiceSpare(ServiceSpare serviceSpare, ServiceOffer serviceOffer)
        {
            await _repository.AddServiceSpareAsync(serviceSpare, serviceOffer);
        }
    }
}
