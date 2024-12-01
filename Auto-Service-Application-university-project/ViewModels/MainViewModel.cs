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

namespace Auto_Service_Application_university_project.ViewModels
{
    public class MainViewModel :ViewModelBase
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
        #endregion

        #region Commands
        //Navigation
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegistration { get; }
        public ICommand NavigateToClients { get; }


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

            // First Page
            NavigateToLoginCommand.Execute(null);
            #endregion

            #region Init Data VM
            _userVM = new UserViewModel();
            _clientVM = new ClientViewModel();
            #endregion

        }

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
                    await FillinOutAllLists();
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

        // TODO: Реализовать после авторитизации заполнение всех листов
        private async Task FillinOutAllLists()
        {
            if (flagUserLogin)
            {
                Clients = await _clientVM.GetAllClients();
            }
            else
            {
                Debug.WriteLine("[INFO] Non Authoricated");
            }
        }
    }
}
