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

        public async Task RemoveServiceSpare(int serviceOfferId, int sparePartId)
        {
            await _repository.RemoveServiceSpareAsync(serviceOfferId, sparePartId);
        }

        public async Task<ObservableCollection<ServiceSpare>> GetServiceSparesByOffer(int serviceOfferId)
        {
            return await _repository.GetServiceSparesByOfferAsync(serviceOfferId);
        }

        public async Task<ObservableCollection<ServiceSpare>> GetAllServiceSpares()
        {
            return await _repository.GetAllServiceSparesAsync();
        }
    }
}
