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
    public class OfficeViewModel
    {
        private OfficeRepository _officeRepository;

        public OfficeViewModel()
        {
            _officeRepository = new OfficeRepository();
        }

        public async Task<ObservableCollection<Office>> GetAllOffices()
        {
            return await _officeRepository.GetAllOfficesAsync();
        }
    }
}
