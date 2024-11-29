using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class LoginViewModel : ViewModelBase 
    {
        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        private string _email;
        private string _password;
        private string _errorMessage;

        private Visibility _visibilityLoginProgres = Visibility.Collapsed;
        #endregion

        #region Properties
        public string Email { get => _email; set => SetProperty(ref _email, value, nameof(Email)); }
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }

        public Visibility VisibilityProgres { get => _visibilityLoginProgres; private set => SetProperty(ref _visibilityLoginProgres, value, nameof(VisibilityProgres)); }
        #endregion

        #region Commands
        public ICommand LoginCommand { get; }

        #endregion

        public LoginViewModel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;

            LoginCommand = new MyICommand<object>(OnClickLogIn);
        }
        // TODO: Добавить команду логин через асинк 
        private void OnClickLogIn(object parameter)
        {
            
            VisibilityProgres = Visibility.Visible;
            if (parameter is PasswordBox passwordBox)
            {
                _password = passwordBox.Password;
                if (CheckInput())
                {
                    // TODO: РЕАЛИЗОВАТЬ ВХОД
                }
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password))
            {
                VisibilityProgres = Visibility.Collapsed;
                ErrorMessage = "";
                ErrorMessage = "Fill in all the fields to continue.";
                return false;
            } else
            {
                ErrorMessage = "";
                return true;
            }
        }
    }
}
