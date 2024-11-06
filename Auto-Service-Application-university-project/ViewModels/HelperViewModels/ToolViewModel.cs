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
