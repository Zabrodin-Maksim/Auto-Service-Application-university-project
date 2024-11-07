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
    public class OtherViewModel : ViewModelBase
    {
        private readonly IOtherRepository _otherRepository;

        public ObservableCollection<Other> Others { get; set; }

        public OtherViewModel(IOtherRepository otherRepository)
        {
            _otherRepository = otherRepository;
            Others = new ObservableCollection<Other>(_otherRepository.GetAll());
        }
    }
}
