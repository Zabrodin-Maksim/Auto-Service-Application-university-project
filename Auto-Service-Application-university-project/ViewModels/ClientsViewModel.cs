using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class ClientsViewModel : ViewModelBase
    {

        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        private Client _selectedClient;

        private string _clientName;
        private string _clientPhone;
        private string _clientCountry;
        private string _clientCity;
        private string _clientIndex;
        private string _clientStreet;
        private string _clintHouseNumber;

        private string _errorMasage;
        #endregion

        #region Properties
        public Client SelectedClient { get => _selectedClient; set { 
                SetProperty(ref _selectedClient, value, nameof(SelectedClient));
                FillAllParameters(value);
            } 
        }
        public ObservableCollection<Client> Clients { get; set; }

        public string ClientName 
        { 
            get => _clientName; set 
            {
                SetProperty(ref _clientName, value, nameof(ClientName)); 
            } 
        }
        public string ClientPhone
        {
            get => _clientPhone; set
            {
                if (CheckInputsNumber(value))
                {
                    if (value.Length <= 12)
                    {
                        SetProperty(ref _clientPhone, value, nameof(ClientPhone));
                    }else
                    {
                        ErrorMessage = "Maximum 12 numbers!";
                    }
                }
            }
        }
        public string ClientCountry
        {
            get => _clientCountry; set
            {
                SetProperty(ref _clientCountry, value, nameof(ClientCountry));
            }
        }
        public string ClientCity
        {
            get => _clientCity; set
            {
                SetProperty(ref _clientCity, value, nameof(ClientCity));
            }
        }
        public string ClientIndex
        {
            get => _clientIndex; set
            {
                if (CheckInputsNumber(value))
                {
                    SetProperty(ref _clientIndex, value, nameof(ClientIndex));
                }
            }
        }
        public string ClientStreet
        {
            get => _clientStreet; set
            {
                SetProperty(ref _clientStreet, value, nameof(ClientStreet));
            }
        }
        public string ClientHouseNumber
        {
            get => _clintHouseNumber; set
            {
                if (CheckInputsNumber(value))
                {
                    SetProperty(ref _clintHouseNumber, value, nameof(ClientHouseNumber));
                }
            }
        }

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
        public ICommand deleteCommand {  get; }
        public ICommand addUpdateCommand { get; }

        #endregion

        public ClientsViewModel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;

            Clients = _mainViewModel.Clients;
            //Clients = new ObservableCollection<Client>()
            //{
            //    new Client()
            //    {
            //        ClientName = "r",
            //        Phone = 111,
            //        Address = new Address()
            //        {
            //            Country = "wed",
            //            City = "wed",
            //            IndexAdd =2222,
            //            Street = "wed",
            //            HouseNumber = 222
            //        }
            //    },
            //    new Client()
            //    {
            //        ClientName = "r",
            //        Phone = 111,
            //        Address = new Address()
            //        {
            //            Country = "wed",
            //            City = "wed",
            //            IndexAdd =2222,
            //            Street = "wed",
            //            HouseNumber = 222
            //        }
            //    },
            //    new Client()
            //    {
            //        ClientName = "r",
            //        Phone = 111,
            //        Address = new Address()
            //        {
            //            Country = "wed",
            //            City = "wed",
            //            IndexAdd =2222,
            //            Street = "wed",
            //            HouseNumber = 222
            //        }
            //    },
            //    new Client()
            //    {
            //        ClientName = "r",
            //        Phone = 111,
            //        Address = new Address()
            //        {
            //            Country = "wed",
            //            City = "wed",
            //            IndexAdd =2222,
            //            Street = "wed",
            //            HouseNumber = 222
            //        }
            //    }
            //};

            clearCommand = new MyICommand(OnClear);
            deleteCommand = new MyICommand<object>(async parametr => await OnDelete(parametr));
            addUpdateCommand = new MyICommand<object>(async parametr => await OnAddUpdate(parametr));
        }

        private void FillAllParameters(Client client)
        {
            if (client == null)
            {
                ClientName = "";
                ClientPhone = "";
                ClientCountry = "";
                ClientCity = "";
                ClientIndex = "";
                ClientStreet = "";
                ClientHouseNumber = "";
            }
            else
            {
                ClientName = client.ClientName;
                ClientPhone = client.Phone.ToString();
                ClientCountry = client.Address.Country;
                ClientCity = client.Address.City;
                ClientIndex = client.Address.IndexAdd.ToString();
                ClientStreet = client.Address.Street;
                ClientHouseNumber = client.Address.HouseNumber.ToString();
            }
        }
        
        private void OnClear()
        {
            SelectedClient = null;
        }

        private async Task OnDelete(object param)
        {
            if (SelectedClient == null)
            {

            }
            else
            {
                // TODO: РЕПОЗИТОРИЙ С ЭТИМ
            }
            OnClear();
        }

        private async Task OnAddUpdate(object param)
        {
            if (SelectedClient == null)
            {
                // TODO: РЕАЛИЗОВАТЬ ДОБОВЛЕНИЕ НОВГО КЛИЕНТА
            }
            else
            {
                // TODO: РЕАЛИЗОВАТЬ ОБНОВЛЕНИЕ ДАННЫХ У КЛИЕНТА
            }
        }

        private bool CheckInputsNumber(string parameterCheck)
        {
            if (parameterCheck.All(char.IsDigit))
            {
                ErrorMessage = "";
                return true;
            }else
            {
                ErrorMessage = "Wrong input format!!!";
                return false;
            }
        }
    }
}
