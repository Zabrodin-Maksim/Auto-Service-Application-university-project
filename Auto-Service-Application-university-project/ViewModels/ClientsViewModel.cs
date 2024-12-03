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

        private ObservableCollection<Client> _clients;

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
        public Client SelectedClient
        {
            get => _selectedClient; set
            {
                SetProperty(ref _selectedClient, value, nameof(SelectedClient));
                FillAllParameters(value);
            }
        }
        public ObservableCollection<Client> Clients { get => _clients; set => SetProperty(ref _clients, value, nameof(Clients)); }

        public string ClientName
        {
            get => _clientName; set
            {
                if (_selectedClient != null)
                {
                    if (CheckInputsNotNull(value, "Client Name"))
                    {
                        SetProperty(ref _clientName, value, nameof(ClientName));
                    }
                }
                else
                {
                    SetProperty(ref _clientName, value, nameof(ClientName));
                }
            }
        }
        public string ClientPhone
        {
            get => _clientPhone; set
            {

                if (CheckInputsNumber(value))
                {
                    if (_selectedClient != null)
                    {
                        if (value.Length <= 9 && value.Length >= 1)
                        {
                            SetProperty(ref _clientPhone, value, nameof(ClientPhone));
                        }
                        else
                        {
                            ErrorMessage = "Maximum 9 numbers and minimum 1 number!";
                        }
                    }
                    else
                    {
                        if (value.Length <= 9)
                        {
                            SetProperty(ref _clientPhone, value, nameof(ClientPhone));
                        }
                        else
                        {
                            ErrorMessage = "Maximum 9 numbers and minimum 1 number!";
                        }
                    }
                }
            }
        }
        public string ClientCountry
        {
            get => _clientCountry; set
            {
                if (_selectedClient != null)
                {
                    if (CheckInputsNotNull(value, "Country"))
                    {
                        SetProperty(ref _clientCountry, value, nameof(ClientCountry));
                    }
                }
                else
                {
                    SetProperty(ref _clientCountry, value, nameof(ClientCountry));
                }
            }
        }
        public string ClientCity
        {
            get => _clientCity; set
            {
                if (_selectedClient != null)
                {
                    if (CheckInputsNotNull(value, "City"))
                    {
                        SetProperty(ref _clientCity, value, nameof(ClientCity));
                    }
                }
                else
                {
                    SetProperty(ref _clientCity, value, nameof(ClientCity));
                }
            }
        }
        public string ClientIndex
        {
            get => _clientIndex; set
            {
                if (CheckInputsNumber(value))
                {
                    if (_selectedClient != null)
                    {
                        if (value.Length <= 9 && value.Length >= 1)
                        {
                            SetProperty(ref _clientIndex, value, nameof(ClientIndex));
                        }
                        else
                        {
                            ErrorMessage = "Maximum 9 numbers and minimum 1 number!";
                        }
                    }
                    else
                    {
                        if (value.Length <= 9)
                        {
                            SetProperty(ref _clientIndex, value, nameof(ClientIndex));
                        }
                        else
                        {
                            ErrorMessage = "Maximum 9 numbers and minimum 1 number!";
                        }
                    }
                }
            }
        }
        public string ClientStreet
        {
            get => _clientStreet; set
            {
                if (_selectedClient != null)
                {
                    if (CheckInputsNotNull(value, "Street"))
                    {
                        SetProperty(ref _clientStreet, value, nameof(ClientStreet));
                    }
                }
                else
                {
                    SetProperty(ref _clientStreet, value, nameof(ClientStreet));
                }

            }
        }
        public string ClientHouseNumber
        {
            get => _clintHouseNumber; set
            {
                if (CheckInputsNumber(value))
                {
                    if (_selectedClient != null)
                    {
                        if (value.Length <= 9 && value.Length >= 1)
                        {
                            SetProperty(ref _clintHouseNumber, value, nameof(ClientHouseNumber));
                        }
                        else
                        {
                            ErrorMessage = "Maximum 9 numbers and minimum 1 number!";
                        }
                    }
                    else
                    {
                        if (value.Length <= 9)
                        {
                            SetProperty(ref _clintHouseNumber, value, nameof(ClientHouseNumber));
                        }
                        else
                        {
                            ErrorMessage = "Maximum 9 numbers and minimum 1 number!";
                        }
                    }
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
        public ICommand deleteCommand { get; }
        public ICommand addUpdateCommand { get; }

        #endregion

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            Clients = _mainViewModel.Clients;

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
            if (_mainViewModel.authenticatedUser.RoleId <= 2)
            {
                if (SelectedClient == null)
                {
                    ErrorMessage = "No selected Client!";
                }
                else
                {

                    await _mainViewModel.DeleteClient(SelectedClient.ClientId);
                    await _mainViewModel.FillinOutClientsLists();
                    Clients = _mainViewModel.Clients;
                    OnClear();
                }
            }
            else
            {
                ErrorMessage = "Not access, you need admin or employee access!";
            }

        }

        private async Task OnAddUpdate(object param)
        {

            if (SelectedClient == null)
            {
                if (CheckAllInputsForAdd())
                {
                    await _mainViewModel.AddClient(ClientName, new Address()
                    {
                        Country = ClientCountry,
                        City = ClientCity,
                        IndexAdd = int.Parse(ClientIndex),
                        Street = ClientStreet,
                        HouseNumber = int.Parse(ClientHouseNumber)
                    }, int.Parse(ClientPhone)
                    );
                    await _mainViewModel.FillinOutClientsLists();
                    Clients = _mainViewModel.Clients;
                }
                else
                {
                    ErrorMessage = "";
                    ErrorMessage = "Somes field is empty!";
                }
            }
            else
            {
                await _mainViewModel.UpdateClient(SelectedClient.ClientId, ClientName, new Address()
                {
                    Country = ClientCountry,
                    City = ClientCity,
                    IndexAdd = int.Parse(ClientIndex),
                    Street = ClientStreet,
                    HouseNumber = int.Parse(ClientHouseNumber)
                }, int.Parse(ClientPhone)
                );
                await _mainViewModel.FillinOutClientsLists();
                Clients = _mainViewModel.Clients;
            }

        }

        private bool CheckInputsNumber(string parameterCheck)
        {
            if (parameterCheck.All(char.IsDigit))
            {
                ErrorMessage = "";
                return true;
            }
            else
            {
                ErrorMessage = "";
                ErrorMessage = "Wrong input format!!!";
                return false;
            }
        }

        private bool CheckInputsNotNull(string input, string nameInput)
        {
            if (string.IsNullOrEmpty(input))
            {
                ErrorMessage = "";
                ErrorMessage = $"{nameInput} is empty!";
                return false;
            }
            else { return true; }
        }

        private bool CheckAllInputsForAdd()
        {
            var fields = new[] { ClientName, ClientCountry, ClientCity, ClientIndex, ClientStreet, ClientHouseNumber, ClientPhone };
            return fields.All(field => !string.IsNullOrEmpty(field));
        }
    }
}
