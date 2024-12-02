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

namespace Auto_Service_Application_university_project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields
        // Navigation
        private readonly MyNavigationService _navigationService;
        private ViewModelBase _currentViewModel;


        #region Data
        // User
        private UserViewModel _userVM;

        // Clients
        private ClientViewModel _clientVM;

        // Office
        private OfficeViewModel _officeVM;

        // Reservation
        private ReservationViewModel _reservationVM;
        #endregion

        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        // User 
        public bool flagUserLogin;

        public User authenticatedUser;

        // Clients
        public ObservableCollection<Client> Clients { get; set; }

        // Office
        public ObservableCollection<Office> Offices { get; set; }

        // Reservation
        public ObservableCollection<Reservation> Reservations { get; set; }
        #endregion

        #region Commands
        //Navigation
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegistration { get; }
        public ICommand NavigateToClients { get; }
        public ICommand NavigateToOrder { get; }

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

            // First Page
            NavigateToLoginCommand.Execute(null);
            #endregion

            #region Init Data VM
            _userVM = new UserViewModel();
            _clientVM = new ClientViewModel();
            _officeVM = new OfficeViewModel();
            _reservationVM = new ReservationViewModel();
            #endregion

            UserLogout = new MyICommand(UserLogOut);
        }

        private void UserLogOut()
        {
            if (flagUserLogin)
            {
                authenticatedUser = null;
                flagUserLogin = false;

                //TODO: ОТЧИЩЕНИЕ ВСЕХ ЛИСТОВ
                Clients.Clear();
                Offices.Clear();
                Reservations.Clear();

                NavigateToLoginCommand.Execute(null);
            }
            else
            {
                MessageBox.Show("User not Authenticated!", "User Logout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        // TODO: РЕАЛИЗОВАТЬ МЕТОД ВИСИБЛЕ ИНТЕРФЕЙСА ПО РОЛИ, В UserLogOut И AuthenticateUser

        

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

        // TODO: В ЗАВИСИМОСТИ ОТ РОЛИ ПОКАЗЫВАТЬ ИНТЕРФЕЙС
        public async Task AuthenticateUser(string userName, string userPassword)
        {
            try
            {
                authenticatedUser = await _userVM.Authorization(userName, userPassword);
                if (authenticatedUser == null)
                {
                    flagUserLogin = false;
                    authenticatedUser = null;
                }
                else
                {
                    Debug.WriteLine($"[INFO] User Authorization successfully: {authenticatedUser.Name}");
                    flagUserLogin = true;

                    //TODO: ВСЕ СПИСКИ ЗАПОЛНЯЮТСЯ ТУТ
                    //Fields all necessary Lists
                    await FillinOutClientsLists();
                    await FillinOutOfficesList();
                    await FillinOutReservationsList();
                }

            }
            catch (Exception ex)
            {
                flagUserLogin = false;
                authenticatedUser = null;
                Debug.WriteLine($"[INFO]Error Authenticate User: {ex.Message}");
            }
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
        #endregion

        #region Reservation Data Methods

        public async Task AddReservations(Car car)
        {
            if (flagUserLogin)
            {
                try
                {
                    await _reservationVM.AddReservation(car);
                    Debug.WriteLine($"[INFO]Add new Reservations: {car.Reservation.Office.ToString}");
                    MessageBox.Show("Reservation was added!", "Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Add new Reservations: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }
        public async Task FillinOutReservationsList()
        {
            if (flagUserLogin)
            {
                try
                {
                    Reservations = await _reservationVM.GetAllReservations();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[INFO]Error Fill in Out Reservations Lists: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }
        #endregion

        // TODO: Реализовать после авторитизации заполнение всех листов

    }
}
