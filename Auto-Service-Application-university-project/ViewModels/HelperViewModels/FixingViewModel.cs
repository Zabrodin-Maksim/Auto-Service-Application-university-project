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
