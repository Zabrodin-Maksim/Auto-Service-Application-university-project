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
    public class ToolViewModel : ViewModelBase
    {
        private readonly IToolRepository _toolRepository;

        public ObservableCollection<Tool> Tools { get; set; }

        public ToolViewModel(IToolRepository toolRepository)
        {
            _toolRepository = toolRepository;
            Tools = new ObservableCollection<Tool>(_toolRepository.GetAll());
        }
    }
}
