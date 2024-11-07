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
    public class FixingViewModel : ViewModelBase
    {
        private readonly IFixingRepository _fixingRepository;

        public ObservableCollection<Fixing> Fixings { get; set; }

        public FixingViewModel(IFixingRepository fixingRepository)
        {
            _fixingRepository = fixingRepository;
            Fixings = new ObservableCollection<Fixing>(_fixingRepository.GetAll());
        }
    }
}
