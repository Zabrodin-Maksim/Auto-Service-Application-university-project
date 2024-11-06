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
    public class SparePartViewModel : ViewModelBase
    {
        private readonly ISparePartRepository _sparePartRepository;

        public ObservableCollection<SparePart> SpareParts { get; set; }

        public SparePartViewModel(ISparePartRepository sparePartRepository)
        {
            _sparePartRepository = sparePartRepository;
            SpareParts = new ObservableCollection<SparePart>(_sparePartRepository.GetAll());
        }
    }
}
