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
