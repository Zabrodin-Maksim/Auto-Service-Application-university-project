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
