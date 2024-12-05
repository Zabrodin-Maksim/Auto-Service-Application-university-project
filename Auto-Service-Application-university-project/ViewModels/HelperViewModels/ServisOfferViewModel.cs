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
    public class ServisOfferViewModel
    {
        private ServisOfferRepository _reservationRepository;

        public ServisOfferViewModel()
        {
            _reservationRepository = new ServisOfferRepository();
        }

        public async Task AddServiceOffer(ServiceOffer offer)
        {
            await _reservationRepository.InsertServiceOfferAsync(offer);
        }

        public async Task<ObservableCollection<ServiceOffer>> GetAllServiceOffers()
        {
            return await _reservationRepository.GetAllServiceOffersAsync();
        }

        public async Task UpdateServiceOffer(ServiceOffer offer)
        {
            await _reservationRepository.UpdateServiceOfferAsync(offer);
        }

        public async Task DeleteServiceOffer(int offerId)
        {
            await _reservationRepository.DeleteServiceOfferAsync(offerId);
        }

        public async Task<ServiceOffer> GetServiceOffer(int offerId)
        {
            return await _reservationRepository.GetServiceOfferAsync(offerId);
        }
    }
}
