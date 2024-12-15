using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Auto_Service_Application_university_project.Enums;
using Auto_Service_Application_university_project.ViewModels.HelperViewModels;
using System.Diagnostics;
using Auto_Service_Application_university_project.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.ConstrainedExecution;
using System.Windows.Controls;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields
        // Navigation
        private readonly MyNavigationService _navigationService;
        private ViewModelBase _currentViewModel;

        // Visibilities 
        private Visibility _isVisibleClients;
        private Visibility _isVisibleOrder;
        private Visibility _isVisibleVisit;
        private Visibility _isVisibleAdmin;
        private Visibility _isVisiblePayment;

        #region Data
        // User
        private UserViewModel _userVM;

        // Clients
        private ClientViewModel _clientVM;

        // Office
        private OfficeViewModel _officeVM;

        // Servis Offer
        private ServisOfferViewModel _servisOfferVM;

        // Service Type
        private ServiceTypeViewModel _serviceTypeVM;

        // Service Spare
        private ServiceSpareViewModel _serviceSpareVM;

        // Spare Part
        private SparePartViewModel _sparePartVM;

        // Car 
        private CarViewModel _carVM;

        // Bill
        private BillViewModel _billVM;

        // Payment Type
        private PaymentTypeViewModel _paymentTypeVM;

        // Payment
        private PaymentDataViewModel _paymentVM;

        // Address 
        private AddressViewModel _addressVM;

        // Reservation 
        private ReservationViewModel _reservationVM;

        // Logs
        private LogsViewModel _logsVM;

        // Catalog Oracle
        private OracleObjectViewModel _oracleObjectVM;

        #endregion

        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        // Visibilities 
        public Visibility IsVisibleClients { get => _isVisibleClients; set => SetProperty(ref _isVisibleClients, value, nameof(IsVisibleClients)); }
        public Visibility IsVisibleOrder { get => _isVisibleOrder; set => SetProperty(ref _isVisibleOrder, value, nameof(IsVisibleOrder)); }
        public Visibility IsVisibleVisit { get => _isVisibleVisit; set => SetProperty(ref _isVisibleVisit, value, nameof(IsVisibleVisit)); }
        public Visibility IsVisibleAdmin { get => _isVisibleAdmin; set => SetProperty(ref _isVisibleAdmin, value, nameof(IsVisibleAdmin)); }
        public Visibility IsVisiblePayment { get => _isVisiblePayment; set => SetProperty(ref _isVisiblePayment, value, nameof(IsVisiblePayment)); }


        // User 
        public bool flagUserLogin;

        public User authenticatedUser;

        // Employer
        public Employer authenticatedEmployer;

        // Admin
        public Employer authenticatedAdmin;

        #region Observable Collections

        // Clients
        public ObservableCollection<Client> Clients { get; set; }

        // Office
        public ObservableCollection<Office> Offices { get; set; }

        // Servis Offer
        public ObservableCollection<ServiceOffer> ServiceOffers { get; set; }
        public ObservableCollection<ServiceOffer> ServiceOffersByOffice { get; set; }

        // Service Type
        public ObservableCollection<ServiceType> ServiceTypes { get; set; }

        // Car
        public ObservableCollection<Car> Cars { get; set; }

        // Spare Part
        public ObservableCollection<SparePart> SpareParts { get; set; }

        #endregion

        #endregion

        #region Commands
        //Navigation
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegistration { get; }
        public ICommand NavigateToClients { get; }
        public ICommand NavigateToOrder { get; }
        public ICommand NavigateToVisit { get; }
        public ICommand NavigateToPayment { get; }
        public ICommand NavigateToAdmin { get; }

        // User Logout
        public ICommand UserLogout { get; }

        #endregion

        public MainViewModel()
        {
            #region Navigation
            // Navigation init
            _navigationService = new MyNavigationService(this);

            // Navigation Commands
            NavigateToLoginCommand = new MyICommand(() => _navigationService.Navigate(ViewTupes.LoginView));
            NavigateToRegistration = new MyICommand(() => _navigationService.Navigate(ViewTupes.Registration));
            NavigateToClients = new MyICommand(() => _navigationService.Navigate(ViewTupes.Clients));
            NavigateToOrder = new MyICommand(() => _navigationService.Navigate(ViewTupes.Order));
            NavigateToVisit = new MyICommand(() => _navigationService.Navigate(ViewTupes.Visit));
            NavigateToPayment = new MyICommand(() => _navigationService.Navigate(ViewTupes.Payment));
            NavigateToAdmin = new MyICommand(() => _navigationService.Navigate(ViewTupes.Admin));

            // First Page
            NavigateToLoginCommand.Execute(null);
            #endregion

            #region Init Data VM
            _userVM = new UserViewModel();
            _clientVM = new ClientViewModel();
            _officeVM = new OfficeViewModel();
            _servisOfferVM = new ServisOfferViewModel();
            _serviceTypeVM = new ServiceTypeViewModel();
            _serviceSpareVM = new ServiceSpareViewModel();
            _sparePartVM = new SparePartViewModel();
            _carVM = new CarViewModel();
            _billVM = new BillViewModel();
            _paymentTypeVM = new PaymentTypeViewModel();
            _paymentVM = new PaymentDataViewModel();
            _addressVM = new AddressViewModel();
            _reservationVM = new ReservationViewModel();
            _logsVM = new LogsViewModel();
            _oracleObjectVM = new OracleObjectViewModel();
            #endregion

            UserLogout = new MyICommand(UserLogOut);

            // For start when user not login
            HideAllVisibilites();
        }

        private void UserLogOut()
        {
            if (flagUserLogin)
            {
                // When logout do:
                authenticatedUser = null;
                flagUserLogin = false;
                authenticatedAdmin = null;
                authenticatedEmployer = null;

                // Hide all buttons
                HideAllVisibilites();

                // Navigate to login page
                NavigateToLoginCommand.Execute(null);

                // Clear all Lists
                Clients.Clear();
                Offices.Clear();
                ServiceOffers.Clear();
                ServiceTypes.Clear();
                ServiceOffers.Clear();
                Cars.Clear();
                SpareParts.Clear();
            }
            else
            {
                MessageBox.Show("User not Authenticated!", "User Logout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        #region Visibilities Methods
        // What will allowed see authenticated User
        private void ShowForUser()
        {
            IsVisibleClients = Visibility.Visible;
            IsVisibleOrder = Visibility.Visible;
            IsVisiblePayment = Visibility.Visible;
        }

        // What will allowed see authenticated Employee
        private void ShowForEmployee()
        {
            IsVisibleVisit = Visibility.Visible;
        }

        // What will allowed see authenticated Admin
        private void ShowForaAdmin()
        {
            IsVisibleClients = Visibility.Visible;
            IsVisibleOrder = Visibility.Visible;
            IsVisibleVisit = Visibility.Visible;
            IsVisibleAdmin = Visibility.Visible;
            IsVisiblePayment = Visibility.Visible;
        }

        // Hide pages in menu for non login user or start condition
        private void HideAllVisibilites()
        {
            IsVisibleClients = Visibility.Collapsed;
            IsVisibleOrder = Visibility.Collapsed;
            IsVisibleVisit = Visibility.Collapsed;
            IsVisibleAdmin = Visibility.Collapsed;
            IsVisiblePayment = Visibility.Collapsed;
        }
        #endregion

        #region User Data Methods
        public async Task AddNewUser(User newUser)
        {
            try
            {
                await _userVM.AddNewUser(newUser);
                Debug.WriteLine($"[INFO] New User added: {newUser.Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[INFO]Error adding new User: {ex.Message}");
            }
        }

        public async Task<ObservableCollection<User>> GetAllUsers()
        {
            try
            {
                return await _userVM.GetAllUsers();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[INFO]Error Get all User: {ex.Message}");
            }
            return null;
        }

        public async Task AuthenticateUser(string userName, string userPassword)
        {
            try
            {
                // Hide before authorization
                HideAllVisibilites();

                authenticatedUser = await _userVM.Authorization(userName, userPassword);
                if (authenticatedUser == null)
                {
                    flagUserLogin = false;
                    authenticatedUser = null;
                }
                else
                {
                    // When successful authentication occurred
                    Debug.WriteLine($"[INFO] User Authorization successfully: {authenticatedUser.Name}");
                    flagUserLogin = true;

                    //Fields all necessary Lists
                    await FillinOutClientsLists();
                    await FillinOutOfficesList();
                    await FillinOutServiceTypesList();
                    await FillinOutServiceOffersList();
                    await GetAllCars();
                    await GetAllSpareParts();

                    // Allow see meunu pages by user role (1- Admin), (2- Employeer), (3- User)
                    switch (authenticatedUser.RoleId)
                    {
                        case 1:
                            authenticatedAdmin = await _userVM.GetEmployerByPhone(authenticatedUser.Phone);
                            Debug.WriteLine($"[INFO] Admin Authorization successfully: {authenticatedAdmin.Phone}");
                            ShowForaAdmin();
                            break;

                        case 2:
                            ShowForEmployee();

                            authenticatedEmployer = await _userVM.GetEmployerByPhone(authenticatedUser.Phone);
                            Debug.WriteLine($"[INFO] Employer Authorization successfully: {authenticatedEmployer.Phone}");
                            break;

                        case 3: ShowForUser(); break;
                    }
                }

            }
            catch (Exception ex)
            {
                flagUserLogin = false;
                authenticatedUser = null;
                Debug.WriteLine($"[INFO]Error Authenticate User: {ex.Message}");
            }
        }

        public async Task UpdateUser(User user)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update User: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.DeleteUser(userId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete User: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteEmployerAsync(int employerId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.DeleteEmployerAsync(employerId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Employer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ObservableCollection<Employer>> GetAllEmployersAsync()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _userVM.GetAllEmployersAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Employers: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task UpdateEmployerAsync(Employer employer)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.UpdateEmployerAsync(employer);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Employer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task AddEmployerAsync(Employer employer)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.AddEmployerAsync(employer);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error add Employer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task AssignRole(int userId, int roleId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.AssignRole(userId, roleId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Assign User role: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task AddEmployer(int userId, int officeId, string speciality)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _userVM.InsertEmployer(userId, officeId, speciality);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add Employer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<Employer> GetEmployerByPhone(long phone)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _userVM.GetEmployerByPhone(phone);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add Employer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<Employer> GetEmployer(int employerId)
        {
            try
            {
                return await _userVM.GetEmployer(employerId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[INFO]Error adding new User: {ex.Message}");
            }
            return null;
        }
        #endregion

        #region Client Data Mehtods

        public async Task<ObservableCollection<Client>> GetAllClientsAsync()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _clientVM.GetAllClients();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get Client: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task UpdateClient(int clientId, string clientName, Address clientAdress, int Phone)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _clientVM.UpdateClient(clientId, clientName, clientAdress, Phone);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Client: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task AddClient(string clientName, Address clientAdress, int Phone)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _clientVM.AddClient(clientName, clientAdress, Phone);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Client: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteClient(int clientId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _clientVM.DeleteClient(clientId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Client: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task FillinOutClientsLists()
        {
            if (flagUserLogin)
            {
                try
                {
                    Clients = await _clientVM.GetAllClients();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in Out Clients Lists: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<Client> GetClientById(int clientId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _clientVM.GetClientById(clientId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get Client by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Office Data Methods
        public async Task FillinOutOfficesList()
        {
            if (flagUserLogin)
            {
                try
                {
                    Offices = await _officeVM.GetAllOffices();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in Out Offices Lists: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdateOfficeAsync(Office office)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _officeVM.UpdateOffice(office);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteOfficeAsync(int officeId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _officeVM.DeleteOffice(officeId);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<Office> GetOfficeAsync(int officeId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _officeVM.GetOffice(officeId);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get by id Office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task InsertOfficeAsync(Office office)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _officeVM.AddOffice(office);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }
        #endregion

        #region Servis Offer Data Methods
        public async Task AddServiceOffer(ServiceOffer offer)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _servisOfferVM.AddServiceOffer(offer);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Service Offer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task FillinOutServiceOffersList()
        {
            if (flagUserLogin)
            {
                try
                {
                    ServiceOffers = await _servisOfferVM.GetAllServiceOffers();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in Out Service Offers List: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ObservableCollection<ServiceOffer>> FillinOutServiceOffersListByOffice(int officeId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _servisOfferVM.GetServiceOffersByOfficeIdAsync(officeId);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in Out Service Offers List: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task UpdateServiceOffer(ServiceOffer offer)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _servisOfferVM.UpdateServiceOffer(offer);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Service Offer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteServiceOffer(int offerId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _servisOfferVM.DeleteServiceOffer(offerId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Service Offer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ServiceOffer> GetServiceOffer(int offerId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _servisOfferVM.GetServiceOffer(offerId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get Service Offer: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Service Types Data Methods
        public async Task FillinOutServiceTypesList()
        {
            if (flagUserLogin)
            {
                try
                {
                    ServiceTypes = await _serviceTypeVM.GetAllServiceTypes();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in Out Offices Lists: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }
        #endregion

        #region Service Spare Data Methods
        public async Task AddServiceSpare(SparePart sparePart, ServiceOffer serviceOffer)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _serviceSpareVM.AddServiceSpare(sparePart, serviceOffer);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Service Spare: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task RemoveServiceSpare(int serviceOfferId, int sparePartId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _serviceSpareVM.RemoveServiceSpare(serviceOfferId, sparePartId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Service Spare: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ObservableCollection<ServiceSpare>> GetServiceSparesByOffer(int serviceOfferId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _serviceSpareVM.GetServiceSparesByOffer(serviceOfferId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Service Spare by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<ObservableCollection<ServiceSpare>> GetAllServiceSparesAsync()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _serviceSpareVM.GetAllServiceSpares();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Service Spare: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Spare Part Data Methods
        public async Task InsertSparePart(SparePart sparePart)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _sparePartVM.AddSparePart(sparePart);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Spare Part: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdateSparePart(SparePart sparePart)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _sparePartVM.UpdateSparePart(sparePart);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Spare Part: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteSparePart(int sparePartId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _sparePartVM.DeleteSparePart(sparePartId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Spare Part: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<SparePart> GetSparePartById(int sparePartId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _sparePartVM.GetSparePartById(sparePartId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get Spare Part by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task GetAllSpareParts()
        {
            if (flagUserLogin)
            {
                try
                {
                    SpareParts = await _sparePartVM.GetAllSpareParts();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get all Spare Parts: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ObservableCollection<SparePart>> GetSparePartsByOffice(int officeId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _sparePartVM.GetSparePartsByOffice(officeId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get Spare Part by office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Car Data Methods
        public async Task AddCar(Car car)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _carVM.AddCar(car);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Car: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdateCar(Car car)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _carVM.UpdateCar(car);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Car: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteCar(int carId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _carVM.DeleteCar(carId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Car: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<Car> GetCar(int carId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _carVM.GetCar(carId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get Car: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task GetAllCars()
        {
            if (flagUserLogin)
            {
                try
                {
                    Cars = await _carVM.GetAllCars();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in out list of the Cars: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }
        #endregion

        #region Bill Data Methods
        public async Task AddBill(Bill bill)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _billVM.AddBill(bill);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Bill: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdateBill(Bill bill)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _billVM.UpdateBill(bill);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Bill: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteBill(int billId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _billVM.DeleteBillAsync(billId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Bill: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<Bill> GetBillById(int billId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _billVM.GetBillByIdAsync(billId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get Bill by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<ObservableCollection<Bill>> GetAllBills()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _billVM.GetAllBillsAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get all Bills: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<ObservableCollection<Bill>> GetBillsByOfficeIdAsync(int officeId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _billVM.GetBillsByOfficeId(officeId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get all Bills by office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Payment Type Data Methods
        public async Task<ObservableCollection<PaymentType>> GetAllPaymentTypes()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _paymentTypeVM.GetAllPaymentTypes();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Get all Payment Types: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Payment Data Methods
        public async Task AddPayment(Payment payment)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _paymentVM.AddPayment(payment);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Payment: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdatePayment(Payment payment)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _paymentVM.UpdatePayment(payment);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Payment: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeletePayment(int paymentId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _paymentVM.DeletePayment(paymentId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Payment: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ObservableCollection<Payment>> GetAllPayments()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _paymentVM.GetAllPayments();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Payments: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _paymentVM.GetPaymentByIdAsync(paymentId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Payments by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<FileStorage> FileStorageAsync(FileStorage file)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _paymentVM.AddFile(file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Payments by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }


        #endregion

        #region Address Data Methods
        public async Task AddAddress(Address address)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _addressVM.AddAddress(address);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Address: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdateAddress(Address address)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _addressVM.UpdateAddress(address);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Address: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteAddress(int addressId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _addressVM.DeleteAddress(addressId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Address: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _addressVM.GetAddressById(addressId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get Address by id: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<ObservableCollection<Address>> GetAllAddresses()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _addressVM.GetAllAddresses();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Address: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Reservation Data Methods
        public async Task InsertReservation(Reservation reservation)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _reservationVM.InsertReservation(reservation);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Reservation: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task UpdateReservation(Reservation reservation)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _reservationVM.UpdateReservation(reservation);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Update Reservation: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task DeleteReservation(int reservationId)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _reservationVM.DeleteReservation(reservationId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Delete Reservation: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }

        public async Task<ObservableCollection<Reservation>> GetAllReservations()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _reservationVM.GetAllReservations();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Reservation: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<ObservableCollection<Reservation>> GetReservationsByClient(int clientId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _reservationVM.GetReservationsByClient(clientId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Reservation by Client: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }

        public async Task<ObservableCollection<Reservation>> GetReservationsByOffice(int officeId)
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _reservationVM.GetReservationsByOffice(officeId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Reservation by Office: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Logs Data Methods
        public async Task<ObservableCollection<Logs>> GetAllLogsAsync()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _logsVM.GetAllLogsAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all Logs: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion

        #region Catalog Oracle Methods
        public async Task<ObservableCollection<OracleObject>> GetSystemObjectsAsync()
        {
            if (flagUserLogin)
            {
                try
                {
                    return await _oracleObjectVM.GetSystemObjectsAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error get all CATALOGS ORACLE: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
            return null;
        }
        #endregion
    }
}
