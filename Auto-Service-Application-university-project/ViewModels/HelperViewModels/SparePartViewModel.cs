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
