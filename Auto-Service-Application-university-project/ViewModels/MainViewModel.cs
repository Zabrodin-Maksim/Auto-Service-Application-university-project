using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Auto_Service_Application_university_project.Enums;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class MainViewModel :ViewModelBase
    {
        #region Private Fields
        // Navigation
        private readonly MyNavigationService _navigationService;
        private ViewModelBase _currentViewModel;

        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }
        #endregion

        #region Commands
        //Navigation
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegistration { get; }

        #endregion

        public MainViewModel()
        {
            #region Navigation
            // Navigation init
            _navigationService = new MyNavigationService(this);

            // Navigation Commands
            NavigateToLoginCommand = new MyICommand(() => _navigationService.Navigate(ViewTupes.LoginView));
            NavigateToRegistration = new MyICommand(() => _navigationService.Navigate(ViewTupes.Registration));

            // First Page
            NavigateToLoginCommand.Execute(null);
            #endregion


        }
    }
}
// TODO: Реализовать Text Fields в Login View Model
