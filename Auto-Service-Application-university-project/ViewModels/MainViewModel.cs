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
        private Visibility _isVisibleEmployee;

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
        public Visibility IsVisibleEmployee { get => _isVisibleEmployee; set => SetProperty(ref _isVisibleEmployee, value, nameof(IsVisibleEmployee)); }


        // User 
        public bool flagUserLogin;

        public User authenticatedUser;

        // Employer
        public Employer authenticatedEmployer;

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
        // User Logout
        public ICommand UserLogout {  get; }

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

                //Hide all buttons
                HideAllVisibilites();

                //TODO: ОТЧИЩЕНИЕ ВСЕХ ЛИСТОВ
                Clients.Clear();
                Offices.Clear();
                ServiceOffers.Clear();
                ServiceTypes.Clear();
                ServiceOffers.Clear();
                ServiceOffersByOffice.Clear();
                Cars.Clear();
                SpareParts.Clear();
                //SparePartsByOffice.Clear();

                // Navigate to login page
                NavigateToLoginCommand.Execute(null);
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
        }

        // What will allowed see authenticated Employee
        private void ShowForEmployee()
        {
            IsVisibleVisit = Visibility.Visible;
            IsVisibleEmployee = Visibility.Visible;
        }

        // What will allowed see authenticated Admin
        private void ShowForaAdmin()
        {
            IsVisibleClients = Visibility.Visible;
            IsVisibleOrder = Visibility.Visible;
            IsVisibleVisit = Visibility.Visible;
            IsVisibleEmployee = Visibility.Visible;
        }

        // Hide pages in menu for non login user or start condition
        private void HideAllVisibilites()
        {
            IsVisibleClients = Visibility.Collapsed;
            IsVisibleOrder = Visibility.Collapsed;
            IsVisibleVisit = Visibility.Collapsed;
            IsVisibleEmployee = Visibility.Collapsed;
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

                    //TODO: ВСЕ СПИСКИ ЗАПОЛНЯЮТСЯ ТУТ
                    //Fields all necessary Lists
                    await FillinOutClientsLists();
                    await FillinOutOfficesList();
                    await FillinOutServiceTypesList();
                    await FillinOutServiceOffersList();
                    await FillinOutServiceOffersListByOffice();
                    await GetAllCars();
                    await GetAllSpareParts();

                    // Allow see meunu pages by user role (1- Admin), (2- Employeer), (3- User)
                    switch (authenticatedUser.RoleId)
                    {
                        case 1: ShowForaAdmin(); break;
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
        #endregion

        #region Client Data Mehtods

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

        public async Task FillinOutServiceOffersListByOffice()
        {
            if (flagUserLogin)
            {
                try
                {
                    ServiceOffersByOffice = await _servisOfferVM.GetAllServiceOffers();

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
        public async Task AddServiceSpare(ServiceSpare serviceSpare, ServiceOffer serviceOffer)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _serviceSpareVM.AddServiceSpare(serviceSpare, serviceOffer);
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
                    Debug.WriteLine($"[INFO]Error Add new Service Spare: {ex.Message}");
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
                    Debug.WriteLine($"[INFO]Error Add new Service Spare: {ex.Message}");
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
                    Debug.WriteLine($"[INFO]Error Add new Service Spare: {ex.Message}");
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
        #endregion

        // TODO: Реализовать после авторитизации заполнение всех листов

    }
}
