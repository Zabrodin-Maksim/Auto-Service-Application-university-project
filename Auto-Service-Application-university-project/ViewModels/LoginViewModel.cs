﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class LoginViewModel : ViewModelBase 
    {

        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        private string _email;
        private string _password;
        #endregion

        #region Properties
        public string Email { get => _email; set => SetProperty(ref _email, value, nameof(Email)); }
        public string Password { get => _password; set => SetProperty(ref _password, value, nameof(Password)); }

        #endregion

        #region Commands


        #endregion

        public LoginViewModel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }

    }
}
