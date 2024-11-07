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
