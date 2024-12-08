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
    public class AddressViewModel
    {
        private AddressRepository _addressRepository;

        public AddressViewModel()
        {
            _addressRepository = new AddressRepository();
        }

        public async Task AddAddress(Address address)
        {
            await _addressRepository.InsertAddressAsync(address);        
        }

        public async Task UpdateAddress(Address address)
        {
            await _addressRepository.UpdateAddressAsync(address);
        }

        public async Task DeleteAddress(int addressId)
        {
            await _addressRepository.DeleteAddressAsync(addressId);
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            return await _addressRepository.GetAddressByIdAsync(addressId);
        }

        public async Task<ObservableCollection<Address>> GetAllAddresses()
        {
            return await _addressRepository.GetAllAddressesAsync();
        }
    }
}
