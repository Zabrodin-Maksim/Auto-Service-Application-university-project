using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        #region Private Fields
        private MainViewModel _mainViewModel;

        private string _searchText;

        private ICollectionView _clients;
        private Client _selectedClient;

        private string _errorMasage;

        private string _carSPZ;
        private string _carBrand;
        private string _carSymptoms;

        private ObservableCollection<Office> _offices;
        private Office _officeSelected;

        private ObservableCollection<ServiceType> _serviceTypes;
        private ServiceType _serviceTypeSelected;

        private Visibility _visibilitySpeciality;
        private Visibility _visibilityRadiusWheel;

        private string _servisTypeSpeciality;
        private string _serviceTypeRadiusWheel;

        private DateTime _selectedDate;
        #endregion

        #region Properties
        public ObservableCollection<Client> Clients { get; set; }
        public ICollectionView FilteredItems { get => _clients; set => SetProperty(ref _clients, value, nameof(Clients)); }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value, nameof(SearchText));
                FilteredItems.Refresh(); 
            }
        }

        public Client SelectedClient
        {
            get => _selectedClient; set
            {
                SetProperty(ref _selectedClient, value, nameof(SelectedClient));
            }
        }

        public string CarSPZ
        {
            get => _carSPZ; set
            {
                if (value.Length <= 7)
                {
                    SetProperty(ref _carSPZ, value, nameof(CarSPZ));
                }
                else
                {
                    ErrorMessage = "Maximum 7 charakters";
                }
            }
        }
        public string CarBrand { get => _carBrand; set => SetProperty(ref _carBrand, value, nameof(CarBrand)); }
        public string CarSymptoms { get => _carSymptoms; set => SetProperty(ref _carSymptoms, value, nameof(CarSymptoms)); }

        public ObservableCollection<Office> Offices { get => _offices; set => SetProperty(ref _offices, value, nameof(Offices)); }
        public Office SelectedOffice { get => _officeSelected; set => SetProperty(ref _officeSelected, value, nameof(SelectedOffice)); }

        public Visibility VisibleServisTypeSpec { get => _visibilitySpeciality; set => SetProperty(ref _visibilitySpeciality, value, nameof(VisibleServisTypeSpec)); }
        public Visibility VisibilityRadiusWheel { get => _visibilityRadiusWheel; set => SetProperty(ref _visibilityRadiusWheel, value, nameof(VisibilityRadiusWheel)); }

        public string ServisTypeSpeciality { get => _servisTypeSpeciality; set => SetProperty(ref _servisTypeSpeciality, value, nameof(ServisTypeSpeciality)); }
        public string RadiusWheel
        {
            get => _serviceTypeRadiusWheel;
            set
            {
                if (value.All(char.IsDigit))
                {
                    SetProperty(ref _serviceTypeRadiusWheel, value, nameof(RadiusWheel));
                }
            }
        }

        public ObservableCollection<ServiceType> ServiceTypes { get => _serviceTypes; set => SetProperty(ref _serviceTypes, value, nameof(ServiceTypes)); }
        public ServiceType ServiceTypeSelected
        {
            get => _serviceTypeSelected;
            set
            {
                SetProperty(ref _serviceTypeSelected, value, nameof(ServiceTypeSelected));
                if (_serviceTypeSelected != null)
                {
                    if (_serviceTypeSelected.TypeName == "pneuservise")
                    {
                        VisibleServisTypeSpec = Visibility.Collapsed;
                        VisibilityRadiusWheel = Visibility.Visible;
                    }
                    else
                    {
                        VisibilityRadiusWheel = Visibility.Collapsed;
                        VisibleServisTypeSpec = Visibility.Visible;
                    }
                }
            }
        }

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

            #region Init Lists
            Clients = _mainViewModel.Clients;
            Offices = _mainViewModel.Offices;
            ServiceTypes = _mainViewModel.ServiceTypes;

            // For Filter
            FilteredItems = CollectionViewSource.GetDefaultView(Clients);
            FilteredItems.Filter = FilterItems;
            #endregion

            // Select in choise box
            if (Offices != null && Offices.Count != 0)
            {
                SelectedOffice = Offices[0];
            }

            if (ServiceTypes != null && ServiceTypes.Count != 0)
            {
                ServiceTypeSelected = ServiceTypes[0];
            }

            SelectedDate = DateTime.Now;

            clearCommand = new MyICommand(OnClear);
            addNewOrderCommand = new MyICommand<object>(async parameter => await OnAddNewOrder(parameter));
        }

        private bool FilterItems(object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
                return true; 

            return obj != null && obj.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase);
        }

        private void OnClear()
        {
            SelectedClient = null;
            CarSPZ = "";
            CarBrand = "";
            CarSymptoms = "";
            ServisTypeSpeciality = "";
            RadiusWheel = "";
            ErrorMessage = "";
        }

        private async Task OnAddNewOrder(object param)
        {
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

            ServiceOffer serviceOffer;

            // Adding Service Offer by Service Type Selected
            if (_serviceTypeSelected.TypeName == "pneuservise")
            {
                serviceOffer = new ServiceOffer()
                {
                    Car = car,
                    ServiceType = ServiceTypeSelected,
                    RadiusWheel = int.Parse(RadiusWheel)
                };
            }
            else
            {
                serviceOffer = new ServiceOffer()
                {
                    Car = car,
                    ServiceType = ServiceTypeSelected,
                    Speciality = ServisTypeSpeciality,
                };
            }

            await _mainViewModel.InsertReservation(car.Reservation);
            await _mainViewModel.AddServiceOffer(serviceOffer);

            // Update services Offers List
            await _mainViewModel.FillinOutServiceOffersList();

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
            if (string.IsNullOrEmpty(_carSPZ) || string.IsNullOrEmpty(_carBrand) || _officeSelected == null)
            {
                ErrorMessage = ErrorMessage + " Please fill up all fields";
                return false;
            }

            if (_serviceTypeSelected.TypeName == "pneuservise" && !_serviceTypeRadiusWheel.All(char.IsDigit))
            {
                ErrorMessage = ErrorMessage + " In Radius Wheel use only numbers!";
                return false;
            }
            else
            {
                if (_serviceTypeSelected.TypeName != "pneuservise" && string.IsNullOrEmpty(_servisTypeSpeciality))
                {
                    ErrorMessage = ErrorMessage + " Fill Servis Type Speciality!";
                    return false;
                }
            }

            ErrorMessage = "";
            return true;
        }
    }
}
