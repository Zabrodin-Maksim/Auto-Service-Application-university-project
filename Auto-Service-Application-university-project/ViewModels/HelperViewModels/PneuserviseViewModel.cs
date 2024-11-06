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
    public class PneuserviseViewModel : ViewModelBase
    {
        private readonly IPneuserviseRepository _pneuserviseRepository;

        public ObservableCollection<Pneuservise> Pneuservices { get; set; }

        public PneuserviseViewModel(IPneuserviseRepository pneuserviseRepository)
        {
            _pneuserviseRepository = pneuserviseRepository;
            Pneuservices = new ObservableCollection<Pneuservise>(_pneuserviseRepository.GetAll());
        }
    }
}
