using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Auto_Service_Application_university_project.Models;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        // Number of register part
        private int _currentPart;

        // First Part
        private Visibility _visibilityFirst;

        private string _userName;
        private string _userSurname;
        private string _userTelephoneNumber;
        private string _errorMessage;

        // Second Part
        private Visibility _visibilitySecond = Visibility.Collapsed;

        private string _userCountry;
        private string _userCity;
        private string _userStreet;
        private string _userHouseNumber;
        private string _userPostCode;

        // Third Part
        private Visibility _visibilityThird = Visibility.Collapsed;
        private Visibility visibilityProgres = Visibility.Collapsed;

        private string _userEmail;
        private string _userPassword;
        #endregion

        #region Properties

        public int CurrentPart
        {
            get => _currentPart;
            set
            {
                _currentPart = value;
                ChangePage();
            }
        }

        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }

        #region First Part
        public Visibility VisibilityFirst { get => _visibilityFirst; private set => SetProperty(ref _visibilityFirst, value, nameof(VisibilityFirst)); }
        public string UserName { get => _userName; set => SetProperty(ref _userName, value, nameof(UserName)); }
        public string UserSurname { get => _userSurname; set => SetProperty(ref _userSurname, value, nameof(UserSurname)); }
        public string UserTelephoneNumber { get => _userTelephoneNumber; set  
            { 
                if(CheckInputsNumber(value))
                { 
                    if (value.Length <=9)
                    { SetProperty(ref _userTelephoneNumber, value, nameof(UserTelephoneNumber)); }
                    else
                    {
                        ErrorMessage = "Maximum 9 numbers of Phone";
                    }
                }
            } }
        
        #endregion

        #region Second Part
        public Visibility VisibilitySecond { get => _visibilitySecond; private set => SetProperty(ref _visibilitySecond, value, nameof(VisibilitySecond)); }
        public string UserCountry { get => _userCountry; set => SetProperty(ref _userCountry, value, nameof(UserCountry)); }
        public string UserCity { get => _userCity; set => SetProperty(ref _userCity, value, nameof(UserCity)); }
        public string UserStreet { get => _userStreet; set => SetProperty(ref _userStreet, value, nameof(UserStreet)); }
        public string UserHouseNumber { get => _userHouseNumber; set  
            {
                if (CheckInputsNumber(value))
                {
                    SetProperty(ref _userHouseNumber, value, nameof(UserHouseNumber));
                }
            } }
        public string UserPostCode { get => _userPostCode; set 
            {
                if (CheckInputsNumber(value))
                { 
                    SetProperty(ref _userPostCode, value, nameof(UserPostCode)); 
                }
            } }
        #endregion

        #region Third Part
        public Visibility VisibilityThird { get => _visibilityThird; private set => SetProperty(ref _visibilityThird, value, nameof(VisibilityThird)); }
        public Visibility VisibilityProgres { get => visibilityProgres; private set => SetProperty(ref visibilityProgres, value, nameof(VisibilityProgres)); }

        public string UserEmail { get => _userEmail; set => SetProperty(ref _userEmail, value, nameof(UserEmail)); }
        #endregion

        #endregion

        #region Commands
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand FinishCommand { get; }

        #endregion

        public RegistrationViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _currentPart = 0;

            NextCommand = new MyICommand(OnNext);
            PrevCommand = new MyICommand(OnBack);
            CancelCommand = new MyICommand(OnCancel);

            FinishCommand = new MyICommand<object>(async parameter => await OnFinish(parameter));
        }

        #region Navigation between parts
        // Method for change register part to next one
        private void OnNext()
        {
            // first part
            if (_currentPart == 0 && CheckInputFirstPart() && TelephoneNumberIsOk())
            {

                CurrentPart++;
            }
            else

                // second part
                if (_currentPart == 1 && CheckInputSecondPart())
            {
                CurrentPart++;
            }

        }

        // Method for change register part to back one
        private void OnBack()
        {
            CurrentPart--;
        }

        // Method exit from Registration page
        private void OnCancel()
        {
            _mainViewModel.NavigateToLoginCommand.Execute(null);
        }
       

        // Helper method for change register part
        private void ChangePage()
        {
            switch (_currentPart)
            {
                case 0:
                    VisibilitySecond = Visibility.Collapsed;
                    VisibilityThird = VisibilitySecond;
                    VisibilityFirst = Visibility.Visible;
                    break;

                case 1:
                    VisibilityFirst = Visibility.Collapsed;
                    VisibilityThird = VisibilityFirst;
                    VisibilitySecond = Visibility.Visible;
                    break;

                case 2:
                    VisibilitySecond = Visibility.Collapsed;
                    VisibilityFirst = VisibilitySecond;
                    VisibilityThird = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region Methods for First part
        // Method for check telephone number input
        private bool TelephoneNumberIsOk()
        {
            if (_userTelephoneNumber.All(char.IsDigit) && _userTelephoneNumber.Length <= 12 && !string.IsNullOrEmpty(_userTelephoneNumber))
            {
                ErrorMessage = "";
                return true;
            }
            else
            {
                ErrorMessage = "";
                ErrorMessage = "Enter a valid phone number without + \nfor example '420123456789'";
                return false;
            }
        }

        // Method for check everything inputs in first Part
        private bool CheckInputFirstPart()
        {
            if (new[] { _userName, _userSurname, _userTelephoneNumber }.Any(string.IsNullOrEmpty))
            {
                ErrorMessage = "";
                ErrorMessage = "Fill in all the fields to continue.";
                return false;
            }
            ErrorMessage = "";
            return true;
        }
        #endregion

        #region Methods for Second part
        // Method for check everything inputs in second Part
        private bool CheckInputSecondPart()
        {
            if (new[] { _userCountry, _userCity, _userStreet, _userHouseNumber, _userPostCode }.Any(string.IsNullOrEmpty))
            {
                
                ErrorMessage = "";
                ErrorMessage = "Fill in all the fields to continue.";
                return false;
            }
            try
            {
                int.Parse(_userHouseNumber);
                int.Parse(_userPostCode);
            }
            catch (Exception ex)
            {
                ErrorMessage = "";
                ErrorMessage = "House Number and Post code without space.";
                return false;
            }
            ErrorMessage = "";
            return true;
        }
        #endregion

        #region Methods for Third part
        private async Task OnFinish(object parameter)
        {
            VisibilityProgres = Visibility.Visible;

            if (parameter is PasswordBox passwordBox)
            {
                _userPassword = passwordBox.Password;
                if (CheckInputTirdPart())
                {
                    User newUser = new User()
                    {
                        Name = _userName + " " + _userSurname,
                        Phone = int.Parse(_userTelephoneNumber),
                        Address = new Address()
                        {
                            Country = _userCountry,
                            City = _userCity,
                            Street = _userStreet,
                            HouseNumber = int.Parse(_userHouseNumber),
                            IndexAdd = int.Parse(_userPostCode)
                        },
                        Username = _userEmail,
                        Password = _userPassword

                    };

                    await _mainViewModel.AddNewUser(newUser);
                    _mainViewModel.NavigateToLoginCommand.Execute(null);
                }
            }
            else
            {
                VisibilityProgres = Visibility.Collapsed;
                ErrorMessage = "";
                ErrorMessage = "Fill in all the fields to continue.";
            }

        }

        private bool CheckInputTirdPart()
        {
            if (new[] { _userEmail, _userPassword }.Any(string.IsNullOrEmpty))
            {
                VisibilityProgres = Visibility.Collapsed;
                ErrorMessage = "";
                ErrorMessage = "Fill in all the fields to continue.";
                return false;
            }
            ErrorMessage = "";
            return true;
        }
        #endregion

        private bool CheckInputsNumber(string parameterCheck)
        {
            if (parameterCheck.All(char.IsDigit))
            {
                ErrorMessage = "";
                return true;
            }
            else
            {
                ErrorMessage = "Wrong input format!!!";
                return false;
            }
        }
    }
}
