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
