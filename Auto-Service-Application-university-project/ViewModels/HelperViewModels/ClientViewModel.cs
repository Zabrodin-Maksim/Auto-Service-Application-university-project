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
    public class ClientViewModel : ViewModelBase
    {
        private readonly IClientRepository _clientRepository;

        public ObservableCollection<Client> Clients { get; set; }

        public ClientViewModel(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
        }
    }
}
