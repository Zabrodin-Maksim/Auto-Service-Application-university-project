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
    public class CheckkViewModel : ViewModelBase
    {
        private readonly ICheckkRepository _checkkRepository;

        public ObservableCollection<Checkk> Checkks { get; set; }

        public CheckkViewModel(ICheckkRepository checkkRepository)
        {
            _checkkRepository = checkkRepository;
            Checkks = new ObservableCollection<Checkk>(_checkkRepository.GetAll());
        }
    }
}
