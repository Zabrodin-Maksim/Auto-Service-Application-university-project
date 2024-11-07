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
    public class ServiseOfferViewModel : ViewModelBase
    {
        private readonly IServiseOfferRepository _serviseOfferRepository;

        public ObservableCollection<ServiseOffer> ServiseOffers { get; set; }

        public ServiseOfferViewModel(IServiseOfferRepository serviseOfferRepository)
        {
            _serviseOfferRepository = serviseOfferRepository;
            ServiseOffers = new ObservableCollection<ServiseOffer>(_serviseOfferRepository.GetAll());
        }
    }
}
