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
    public class ServiceSpareViewModel : ViewModelBase
    {
        private readonly IServiceSpareRepository _serviceSpareRepository;

        public ObservableCollection<ServiceSpare> ServiceSpares { get; set; }

        public ServiceSpareViewModel(IServiceSpareRepository serviceSpareRepository)
        {
            _serviceSpareRepository = serviceSpareRepository;
            ServiceSpares = new ObservableCollection<ServiceSpare>(_serviceSpareRepository.GetAll());
        }
    }
}
