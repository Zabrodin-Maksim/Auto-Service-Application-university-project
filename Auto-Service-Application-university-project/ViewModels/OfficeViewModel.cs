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
    public class OfficeViewModel : ViewModelBase
    {
        private readonly IOfficeRepository _officeRepository;

        public ObservableCollection<Office> Offices { get; set; }

        public OfficeViewModel(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
            Offices = new ObservableCollection<Office>(_officeRepository.GetAll());
        }
    }
}
