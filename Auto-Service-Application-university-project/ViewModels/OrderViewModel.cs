using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        #region Private Fields
        private MainViewModel _mainViewModel;

        private ObservableCollection<Client> _clients;
        private Client _selectedClient;

        private string _errorMasage;

        private string _carSPZ;
        private string _carBrand;
        private string _carSymptoms;

        private ObservableCollection<Office> _offices;
        private Office _officeSelected;

        private DateTime _selectedDate;
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get => _clients; set => SetProperty(ref _clients, value, nameof(Clients)); }
        public Client SelectedClient
        {
            get => _selectedClient; set
            {
                SetProperty(ref _selectedClient, value, nameof(SelectedClient));
            }
        }
        
        public string CarSPZ { get => _carSPZ; set => SetProperty(ref _carSPZ, value, nameof(CarSPZ)); }
        public string CarBrand { get => _carBrand; set => SetProperty(ref _carBrand, value, nameof(CarBrand)); }
        public string CarSymptoms { get => _carSymptoms; set => SetProperty(ref _carSymptoms, value, nameof(CarSymptoms)); }

        public ObservableCollection<Office> Offices { get => _offices; set => SetProperty(ref _offices, value, nameof(Offices)); }
        public Office SelectedOffice { get => _officeSelected; set => SetProperty(ref _officeSelected, value, nameof(SelectedOffice)); }

        public DateTime SelectedDate { get => _selectedDate; set => SetProperty(ref _selectedDate, value, nameof(SelectedDate)); }

        public string ErrorMessage
        {
            get => _errorMasage; set
            {
                SetProperty(ref _errorMasage, value, nameof(ErrorMessage));
            }
        }
        #endregion

        #region Commands
        public ICommand clearCommand { get; }
        public ICommand addNewOrderCommand { get; }
        #endregion

        public OrderViewModel(MainViewModel mainViewModel) 
        { 
            _mainViewModel = mainViewModel;

            Clients = _mainViewModel.Clients;
            Offices = _mainViewModel.Offices;
            
            if (Offices != null && Offices.Count != 0)
            {
                SelectedOffice = Offices[0];
            }
            SelectedDate = DateTime.Now;

            clearCommand = new MyICommand(OnClear);
            addNewOrderCommand = new MyICommand<object>(async parameter => await OnAddNewOrder(parameter));
        }

        private void OnClear()
        {

            SelectedClient = null;
        }

        private async Task OnAddNewOrder(object param)
        {
            //TODO: ДОДЕЛАТЬ OnAddNewOrder
            if (!CheckAllInputs())
            {
                return;
            }

            Car car = new Car()
            {
                SPZ = _carSPZ,
                CarBrand = _carBrand,
                Symptoms = _carSymptoms,
                Reservation = new Reservation()
                {
                    DateReservace = SelectedDate,
                    Office = SelectedOffice,
                    Client = SelectedClient
                }
            };

            await _mainViewModel.AddReservations(car);
            OnClear();

        }

        private bool CheckAllInputs()
        {
            if (SelectedClient == null)
            {
                ErrorMessage = "";
                ErrorMessage = "Select Client!";
                return false;
            }
            if (string.IsNullOrEmpty(_carSPZ) || string.IsNullOrEmpty(_carBrand) || _officeSelected == null || _selectedDate == null)
            {
                ErrorMessage = ErrorMessage + " Please fill up all fields";
                return false;
            }
            ErrorMessage = "";
            return true;
        }
    }
}
