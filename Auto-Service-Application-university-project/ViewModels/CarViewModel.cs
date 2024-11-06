using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.ViewModels
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
