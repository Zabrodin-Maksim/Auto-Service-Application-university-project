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
