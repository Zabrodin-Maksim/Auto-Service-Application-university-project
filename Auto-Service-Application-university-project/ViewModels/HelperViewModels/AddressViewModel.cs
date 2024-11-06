using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.ViewModels.HelperViewModels
{
    public class AddressViewModel : ViewModelBase
    {
        private readonly IAddressRepository _addressRepository;

        public ObservableCollection<Address> Addresses { get; set; }

        public AddressViewModel(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
            Addresses = new ObservableCollection<Address>(_addressRepository.GetAll());
        }
    }
}
