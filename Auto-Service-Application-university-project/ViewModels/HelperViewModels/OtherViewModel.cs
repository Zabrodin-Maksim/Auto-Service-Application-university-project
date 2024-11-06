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
