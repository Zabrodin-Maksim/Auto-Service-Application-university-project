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
    public class CarViewModel : ViewModelBase
    {
        private readonly ICarRepository _carRepository;

        public ObservableCollection<Car> Cars { get; set; }

        public CarViewModel(ICarRepository carRepository)
        {
            _carRepository = carRepository;
            Cars = new ObservableCollection<Car>(_carRepository.GetAll());
        }
    }
}
