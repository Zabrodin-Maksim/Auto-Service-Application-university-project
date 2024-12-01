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
    }
}
