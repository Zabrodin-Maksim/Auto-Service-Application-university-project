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
    public class ClientViewModel
    {
        private ClientRepository _clientRepository;

        public ClientViewModel()
        {
            _clientRepository = new ClientRepository();
        }

        public async Task<ObservableCollection<Client>> GetAllClients()
        {
            return await _clientRepository.GetAllClientsAsync();
        }

        public async Task UpdateClient(int clientId, string clientName, Address clientAdress, int Phone)
        {
            await _clientRepository.UpdateClientAsync(new Client()
            {
                ClientId = clientId,
                ClientName = clientName,
                Address = clientAdress,
                Phone = Phone
            });
        }

        public async Task AddClient (string clientName, Address clientAdress, int Phone)
        {
            await _clientRepository.InsertClientAsync(new Client()
            {
                ClientName = clientName,
                Address = clientAdress,
                Phone = Phone
            });
        }

        public async Task DeleteClient(int clientId)
        {
            await _clientRepository.DeleteClientAsync(clientId);
        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await _clientRepository.GetClientPublicAsync(clientId);
        }
    }
}
