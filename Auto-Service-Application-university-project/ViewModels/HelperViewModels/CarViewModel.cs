using Auto_Service_Application_university_project.Data;
using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class CarViewModel
    {
        private CarRepository _carRepository;

        public CarViewModel()
        {
            _carRepository = new CarRepository();
        }

        public async Task AddCar(Car car)
        {
            await _carRepository.InsertCarAsync(car);
        }

        public async Task UpdateCar(Car car)
        {
            await _carRepository.UpdateCarAsync(car);
        }

        public async Task DeleteCar(int carId)
        {
            await _carRepository.DeleteCarAsync(carId);
        }

        public async Task<Car> GetCar(int carId)
        {
            return await _carRepository.GetCarAsync(carId);
        }

        public async Task<ObservableCollection<Car>> GetAllCars()
        {
            return await _carRepository.GetAllCarsAsync();
        }
    }
}
