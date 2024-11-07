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
    public class EmployerViewModel : ViewModelBase
    {
        private readonly IEmployerRepository _employerRepository;

        public ObservableCollection<Employer> Employers { get; set; }

        public EmployerViewModel(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
            Employers = new ObservableCollection<Employer>(_employerRepository.GetAll());
        }
    }
}
