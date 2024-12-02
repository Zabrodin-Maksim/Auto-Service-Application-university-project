using Auto_Service_Application_university_project.Enums;
using Auto_Service_Application_university_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Services
{
    public class MyNavigationService
    {
        private readonly MainViewModel _mainViewModel;

        public MyNavigationService(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Navigate(ViewTupes viewType)
        {
            switch (viewType)
            {
                case ViewTupes.LoginView:
                    _mainViewModel.CurrentViewModel = new LoginViewModel(_mainViewModel); // To Login
                    break;

                case ViewTupes.Registration:
                    _mainViewModel.CurrentViewModel = new RegistrationViewModel(_mainViewModel); // To Registration
                    break;
                case ViewTupes.Clients:
                    _mainViewModel.CurrentViewModel = new ClientsViewModel(_mainViewModel); // To Clients
                    break;
                case ViewTupes.Order:
                    _mainViewModel.CurrentViewModel = new OrderViewModel(_mainViewModel); // To Clients
                    break;
            }
        }

    }
}
