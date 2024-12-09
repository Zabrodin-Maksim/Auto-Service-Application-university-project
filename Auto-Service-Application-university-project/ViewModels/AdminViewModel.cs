﻿using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using Auto_Service_Application_university_project.Enums;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Windows.Controls;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        #region Visibilities
        // Filds
        private Visibility _visibilityFilds;

        //List Items
        private Visibility _visibleListItem;

        // Menu
        private Visibility _visibilityMenu;

        #region Buttons tools
        private Visibility _visibilitiButtonsTools;
        #endregion

        #region Combo Boxes
        private Visibility _visibleFirstCombo;
        private Visibility _visibleSecondCombo;
        private Visibility _visibleThirdCombo;
        private Visibility _visibleFourthCombo;
        #endregion

        #region Date
        private Visibility _visibleDate;
        #endregion

        #region Text Fields
        private Visibility _visibleFirstTextBox;
        private Visibility _visibleSecondTextBox;
        private Visibility _visibleThirdTextBox;
        private Visibility _visibleFourthTextBox;
        private Visibility _visibleFifthTextBox;
        #endregion

        #region Buttons role
        private Visibility _visibilitiButtonsRole;
        #endregion

        #endregion


        // List Items
        private ObservableCollection<Object> _listItems;
        private Object _selectedItems;

        // List menu
        private ObservableCollection<Logs> _Listlogs;
        private ObservableCollection<OracleObject> _ListOracleObjects;


        #region Combo Boxes
        // First
        private ObservableCollection<Object> _itemsFirstCombo;
        private Object _selectedFirstCombo;
        private string _TextDiscrFirstCombo;

        // Second
        private ObservableCollection<Object> _itemsSecondCombo;
        private Object _selectedSecondCombo;
        private string _TextDiscrSecondCombo;

        // Third
        private ObservableCollection<Object> _itemsThirdCombo;
        private Object _selectedThirdCombo;
        private string _TextDiscrThirdCombo;

        // Fourth
        private ObservableCollection<Object> _itemsFourthCombo;
        private Object _selectedFourthCombo;
        private string _TextDiscrSixthCombo;

        #endregion

        #region Date
        private DateTime _selectedTime;
        private string _TextDiscrDate;
        #endregion

        #region Text Fields
        // First
        private string _FirstTextBox;
        private string _TextDiscrFirstTextBox;

        // Second
        private string _SecondTextBox;
        private string _TextDiscrSecondTextBox;

        // Third
        private string _ThirdTextBox;
        private string _TextDiscrThirdTextBox;

        //Fourth
        private string _FourthTextBox;
        private string _TextDiscrFourthTextBox;

        // Fifth
        private string _FifthTextBox;
        private string _TextDiscrFifthTextBox;

        // Error
        private string _errorMessage;
        #endregion

        #endregion

        #region Properties
        public EnumAdminMenuItems currentMenuItem { get; set; }

        #region Visibility
        // Fields
        public Visibility VisibilityFilds { get => _visibilityFilds; set => SetProperty(ref _visibilityFilds, value, nameof(VisibilityFilds)); }

        // List 
        public Visibility VisibleListItem { get => _visibleListItem; set => SetProperty(ref _visibleListItem, value, nameof(VisibleListItem)); }

        // Menu
        public Visibility VisibilityMenu { get => _visibilityMenu; set => SetProperty(ref _visibilityMenu, value, nameof(VisibilityMenu)); }

        #region Visibility for Buttons
        public Visibility VisibilitiButtonsTools { get => _visibilitiButtonsTools; set => SetProperty(ref _visibilitiButtonsTools, value, nameof(VisibilitiButtonsTools)); }
        #endregion

        #region Visibility for ComboBox
        public Visibility VisibleFirstCombo { get => _visibleFirstCombo; set => SetProperty(ref _visibleFirstCombo, value, nameof(VisibleFirstCombo)); }
        public Visibility VisibleSecondCombo { get => _visibleSecondCombo; set => SetProperty(ref _visibleSecondCombo, value, nameof(VisibleSecondCombo)); }
        public Visibility VisibleThirdCombo { get => _visibleThirdCombo; set => SetProperty(ref _visibleThirdCombo, value, nameof(VisibleThirdCombo)); }
        public Visibility VisibleFourthCombo { get => _visibleFourthCombo; set => SetProperty(ref _visibleFourthCombo, value, nameof(VisibleFourthCombo)); }
        #endregion

        #region Visibility for Date
        public Visibility VisibleDate { get => _visibleDate; set => SetProperty(ref _visibleDate, value, nameof(VisibleDate)); }
        #endregion

        #region Visibility for TextBox
        public Visibility VisibleFirstTextBox { get => _visibleFirstTextBox; set => SetProperty(ref _visibleFirstTextBox, value, nameof(VisibleFirstTextBox)); }
        public Visibility VisibleSecondTextBox { get => _visibleSecondTextBox; set => SetProperty(ref _visibleSecondTextBox, value, nameof(VisibleSecondTextBox)); }
        public Visibility VisibleThirdTextBox { get => _visibleThirdTextBox; set => SetProperty(ref _visibleThirdTextBox, value, nameof(VisibleThirdTextBox)); }
        public Visibility VisibleFourthTextBox { get => _visibleFourthTextBox; set => SetProperty(ref _visibleFourthTextBox, value, nameof(VisibleFourthTextBox)); }
        public Visibility VisibleFifthTextBox { get => _visibleFifthTextBox; set => SetProperty(ref _visibleFifthTextBox, value, nameof(VisibleFifthTextBox)); }
        #endregion

        #region Visibility for Buttons role
        public Visibility VisibilitiButtonsRole { get => _visibilitiButtonsRole; set => SetProperty(ref _visibilitiButtonsRole, value, nameof(VisibilitiButtonsRole)); }
        #endregion
        #endregion


        // List Items
        public ObservableCollection<object> ListItems { get => _listItems; set => SetProperty(ref _listItems, value, nameof(ListItems)); }
        public object SelectedItems { get => _selectedItems; set => SetProperty(ref _selectedItems, value, nameof(SelectedItems)); }

        // List menu
        public ObservableCollection<Logs> Logs { get => _Listlogs; set => SetProperty(ref _Listlogs, value, nameof(Logs)); }
        public ObservableCollection<OracleObject> SystemCatalog { get => _ListOracleObjects; set => SetProperty(ref _ListOracleObjects, value, nameof(SystemCatalog)); }

        #region Observable Collections and Selected for ComboBox
        // First
        public ObservableCollection<object> ItemsFirstCombo { get => _itemsFirstCombo; set => SetProperty(ref _itemsFirstCombo, value, nameof(ItemsFirstCombo)); }
        public object SelectedFirstCombo
        {
            get => _selectedFirstCombo;
            set => SetProperty(ref _selectedFirstCombo, value, nameof(SelectedFirstCombo));
        }
        public string TextDiscrFirstCombo { get => _TextDiscrFirstCombo; set => SetProperty(ref _TextDiscrFirstCombo, value, nameof(TextDiscrFirstCombo)); }

        // Second
        public ObservableCollection<object> ItemsSecondCombo { get => _itemsSecondCombo; set => SetProperty(ref _itemsSecondCombo, value, nameof(ItemsSecondCombo)); }
        public object SelectedSecondCombo
        {
            get => _selectedSecondCombo;
            set => SetProperty(ref _selectedSecondCombo, value, nameof(SelectedSecondCombo));
        }
        public string TextDiscrSecondCombo { get => _TextDiscrSecondCombo; set => SetProperty(ref _TextDiscrSecondCombo, value, nameof(TextDiscrSecondCombo)); }

        // Third
        public ObservableCollection<object> ItemsThirdCombo { get => _itemsThirdCombo; set => SetProperty(ref _itemsThirdCombo, value, nameof(ItemsThirdCombo)); }
        public object SelectedThirdCombo
        {
            get => _selectedThirdCombo;
            set => SetProperty(ref _selectedThirdCombo, value, nameof(SelectedThirdCombo));
        }
        public string TextDiscrThirdCombo { get => _TextDiscrThirdCombo; set => SetProperty(ref _TextDiscrThirdCombo, value, nameof(TextDiscrThirdCombo)); }

        // Fourth
        public ObservableCollection<object> ItemsFourthCombo { get => _itemsFourthCombo; set => SetProperty(ref _itemsFourthCombo, value, nameof(ItemsFourthCombo)); }
        public object SelectedFourthCombo
        {
            get => _selectedFourthCombo;
            set => SetProperty(ref _selectedFourthCombo, value, nameof(SelectedFourthCombo));
        }
        public string TextDiscrSixthCombo { get => _TextDiscrSixthCombo; set => SetProperty(ref _TextDiscrSixthCombo, value, nameof(TextDiscrSixthCombo)); }
        #endregion

        #region Date
        public DateTime SelectedDate { get => _selectedTime; set => SetProperty(ref _selectedTime, value, nameof(SelectedDate)); }
        public string TextDiscrDate { get => _TextDiscrDate; set => SetProperty(ref _TextDiscrDate, value, nameof(TextDiscrDate)); }
        #endregion

        #region TextBlock

        // First
        public string FirstTextBox { get => _FirstTextBox; set => SetProperty(ref _FirstTextBox, value, nameof(FirstTextBox)); }
        public string TextDiscrFirstTextBox { get => _TextDiscrFirstTextBox; set => SetProperty(ref _TextDiscrFirstTextBox, value, nameof(TextDiscrFirstTextBox)); }

        //Second
        public string SecondTextBox { get => _SecondTextBox; set => SetProperty(ref _SecondTextBox, value, nameof(SecondTextBox)); }
        public string TextDiscrSecondTextBox { get => _TextDiscrSecondTextBox; set => SetProperty(ref _TextDiscrSecondTextBox, value, nameof(TextDiscrSecondTextBox)); }

        // Third
        public string ThirdTextBox { get => _ThirdTextBox; set => SetProperty(ref _ThirdTextBox, value, nameof(ThirdTextBox)); }
        public string TextDiscrThirdTextBox { get => _TextDiscrThirdTextBox; set => SetProperty(ref _TextDiscrThirdTextBox, value, nameof(TextDiscrThirdTextBox)); }

        // Fourth
        public string FourthTextBox { get => _FourthTextBox; set => SetProperty(ref _FourthTextBox, value, nameof(FourthTextBox)); }
        public string TextDiscrFourthTextBox { get => _TextDiscrFourthTextBox; set => SetProperty(ref _TextDiscrFourthTextBox, value, nameof(TextDiscrFourthTextBox)); }

        // Fifth
        public string FifthTextBox { get => _FifthTextBox; set => SetProperty(ref _FifthTextBox, value, nameof(FifthTextBox)); }
        public string TextDiscrFifthTextBox { get => _TextDiscrFifthTextBox; set => SetProperty(ref _TextDiscrFifthTextBox, value, nameof(TextDiscrFifthTextBox)); }

        // Error
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }
        #endregion

        #endregion

        #region Commands
        // For Filds
        public ICommand BackCommand { get; }
        public ICommand AddUpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        // Role Commands
        public ICommand DownRoleCommand { get; }
        public ICommand UpRoleCommand { get; }

        // For Menu
        public ICommand ClientsCommand { get; }
        public ICommand UsersCommand { get; }
        public ICommand EmployersCommand { get; }
        public ICommand CarsCommand { get; }
        public ICommand ReservationsCommand { get; }
        public ICommand ServisOffersCommand { get; }
        public ICommand OfficesCommand { get; }
        public ICommand SpairPartsCommand { get; }
        public ICommand ServisSpairCommand { get; }
        public ICommand BillsCommand { get; }
        public ICommand PaymentsCommand { get; }
        #endregion

        public AdminViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            

            #region Init List
            ListItems = new ObservableCollection<object>();
            #endregion

            #region Init Lists for Combo Boxes
            ItemsFirstCombo = new ObservableCollection<object>();
            ItemsSecondCombo = new ObservableCollection<object>();
            ItemsThirdCombo = new ObservableCollection<object>();
            ItemsFourthCombo = new ObservableCollection<object>();
            #endregion

            #region Init Commands
            // Filds
            BackCommand = new MyICommand<object>(async parametr => await OnBackCommand(parametr));
            AddUpdateCommand = new MyICommand<object>(async parametr => await OnAddUpdateCommand(parametr));
            DeleteCommand = new MyICommand<object>(async parametr => await OnDeleteCommand(parametr));

            //Role
            DownRoleCommand = new MyICommand<object>(async parametr => await OnDownRole(parametr));
            UpRoleCommand = new MyICommand<object>(async parametr => await OnUpRole(parametr));

            // Menu
            ClientsCommand = new MyICommand<object>(async parametr => await OnClientsCommand(parametr));
            UsersCommand = new MyICommand<object>(async parametr => await OnUsersCommand(parametr));
            EmployersCommand = new MyICommand<object>(async parametr => await OnEmployersCommand(parametr));
            CarsCommand = new MyICommand<object>(async parametr => await OnCarsCommand(parametr));
            ReservationsCommand = new MyICommand<object>(async parametr => await OnReservationsCommand(parametr));
            ServisOffersCommand = new MyICommand<object>(async parametr => await OnServisOffersCommand(parametr));
            OfficesCommand = new MyICommand<object>(async parametr => await OnOfficesCommand(parametr));
            SpairPartsCommand = new MyICommand<object>(async parametr => await OnSpairPartsCommand(parametr));
            ServisSpairCommand = new MyICommand<object>(async parametr => await OnServisSpairCommand(parametr));
            BillsCommand = new MyICommand<object>(async parametr => await OnBillsCommand(parametr));
            PaymentsCommand = new MyICommand<object>(async parametr => await OnPaymentsCommand(parametr));
            #endregion

            HideAllFields();
            FillLogsAndCatalog();
        }

        private async Task FillLogsAndCatalog()
        {
            Logs = await _mainViewModel.GetAllLogsAsync();
            SystemCatalog = await _mainViewModel.GetSystemObjectsAsync();
        }

        private void ClearAllInputs()
        {
            SelectedItems = null;
            SelectedFirstCombo = null;
            SelectedSecondCombo = null;
            SelectedThirdCombo = null;
            SelectedFourthCombo = null;
            FirstTextBox = "";
            SecondTextBox = "";
            ThirdTextBox = "";
            FourthTextBox = "";
            FifthTextBox = "";
            ErrorMessage = "";
        }

        private async Task OnBackCommand(object param)
        {
            await FillLogsAndCatalog();

            HideAllFields();
            VisibilityMenu = Visibility.Visible;
            VisibilityFilds = Visibility.Collapsed;

            // Clear all selected items
            ClearAllInputs();
        }

        private async Task OnAddUpdateCommand(object param)
        {
            switch (currentMenuItem)
            {
                case EnumAdminMenuItems.Clients:
                    if (SelectedItems == null)
                    {
                        await AddClient(param);
                    }
                    else
                    {
                        await UpdateClient(param);
                    }
                    break;

                case EnumAdminMenuItems.Users:
                    if (SelectedItems == null)
                    {
                        await AddUser(param);
                    }
                    else
                    {
                        await UpdateUser(param);
                    }
                    break;

                case EnumAdminMenuItems.Employers:
                    if (SelectedItems == null)
                    {
                        await AddEmployer(param);
                    }
                    else
                    {

                    }
                    break;

                case EnumAdminMenuItems.Cars:
                    if (SelectedItems == null)
                    {
                        await AddCar(param);
                    }
                    else
                    {
                        await UpdateCar(param);
                    }
                    break;

                case EnumAdminMenuItems.Reservations:
                    if (SelectedItems == null)
                    {
                        await AddReservation(param);
                    }
                    else
                    {
                        await UpdateReservation(param);
                    }
                    break;

                case EnumAdminMenuItems.ServisOffers:
                    if (SelectedItems == null)
                    {
                    }
                    else
                    {

                    }
                    break;

                case EnumAdminMenuItems.Offices:
                    if (SelectedItems == null)
                    {
                        await AddOffice(param);
                    }
                    else
                    {
                        await UpdateOffice(param);
                    }
                    break;

                case EnumAdminMenuItems.SpairParts:
                    if (SelectedItems == null)
                    {
                        await AddSpairParts(param);
                    }
                    else
                    {
                        await UpdateSpairParts(param);
                    }
                    break;

                case EnumAdminMenuItems.ServisSpair:
                    if (SelectedItems == null)
                    {
                        await AddServiceSpare(param);
                    }
                    else
                    {
                        await AddServiceSpare(param);
                    }
                    break;

                case EnumAdminMenuItems.Bills:
                    if (SelectedItems == null)
                    {
                        await AddBill(param);
                    }
                    else
                    {
                        await UpdateBill(param);
                    }
                    break;

                case EnumAdminMenuItems.Payments:
                    if (SelectedItems == null)
                    {
                        await AddPayment(param);
                    }
                    else
                    {
                        await UpdatePayment(param);
                    }
                    break;
            }
        }

        private async Task OnDeleteCommand(object param)
        {
            if (SelectedItems != null)
            {
                switch (currentMenuItem)
                {
                    case EnumAdminMenuItems.Clients:
                        await DeleteClient(param);
                        break;
                    case EnumAdminMenuItems.Users:
                        await DeleteUser(param);
                        break;
                    case EnumAdminMenuItems.Employers:

                        break;
                    case EnumAdminMenuItems.Cars:
                        await DeleteCar(param);
                        break;
                    case EnumAdminMenuItems.Reservations:
                        await DeleteReservation(param);
                        break;
                    case EnumAdminMenuItems.ServisOffers:
                        break;
                    case EnumAdminMenuItems.Offices:
                        await DeleteOffice(param);
                        break;
                    case EnumAdminMenuItems.SpairParts:
                        await DeleteSpairParts(param);
                        break;
                    case EnumAdminMenuItems.ServisSpair:
                        await DeleteServiceSpare(param);
                        break;
                    case EnumAdminMenuItems.Bills:
                        await DeleteBill(param);
                        break;
                    case EnumAdminMenuItems.Payments:
                        await DeletePayment(param);
                        break;
                }
            }
            else
            {
                ErrorMessage = "Select List Item!";
            }

        }

        private void ChangeMenuOnFields()
        {
            VisibilityMenu = Visibility.Collapsed;
            VisibilityFilds = Visibility.Visible;
            VisibleListItem = Visibility.Visible;
        }

        private void HideAllFields()
        {
            // List Items
            VisibleListItem = Visibility.Collapsed;

            // ComboBoxes 
            VisibleFirstCombo = Visibility.Collapsed;
            VisibleSecondCombo = Visibility.Collapsed;
            VisibleThirdCombo = Visibility.Collapsed;
            VisibleFourthCombo = Visibility.Collapsed;

            // Date
            VisibleDate = Visibility.Collapsed;

            // Text Boxes
            VisibleFirstTextBox = Visibility.Collapsed;
            VisibleSecondTextBox = Visibility.Collapsed;
            VisibleThirdTextBox = Visibility.Collapsed;
            VisibleFourthTextBox = Visibility.Collapsed;
            VisibleFifthTextBox = Visibility.Collapsed;

            // Role Buttons
            VisibilitiButtonsRole = Visibility.Collapsed;


        }


        #region Clients Methods
        private async Task OnClientsCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;
            VisibleSecondTextBox = Visibility.Visible;

            // Fill in List
            var clients = await _mainViewModel.GetAllClientsAsync();
            ListItems = new ObservableCollection<object>(clients.Cast<object>());

            var addreses = await _mainViewModel.GetAllAddresses();
            ItemsFirstCombo = new ObservableCollection<object>(addreses.Cast<object>());

            // Fill Text Discriptions
            TextDiscrFirstCombo = "Address";
            TextDiscrFirstTextBox = "Name";
            TextDiscrSecondTextBox = "Phone";

            currentMenuItem = EnumAdminMenuItems.Clients;
        }

        private async Task UpdateClient(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && SecondTextBox.All(char.IsDigit))
            {
                try
                {
                    // Update Client
                    Client client = (Client)SelectedItems;
                    await _mainViewModel.UpdateClient(client.ClientId, FirstTextBox, (Address)SelectedFirstCombo, int.Parse(SecondTextBox));

                    // Update List of Clients 
                    var clients = await _mainViewModel.GetAllClientsAsync();
                    ListItems = new ObservableCollection<object>(clients.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Client {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddClient(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && SecondTextBox.All(char.IsDigit))
            {
                try
                {
                    // Add Client
                    await _mainViewModel.AddClient(FirstTextBox, (Address)SelectedFirstCombo, int.Parse(SecondTextBox));

                    // Update List of Clients 
                    var clients = await _mainViewModel.GetAllClientsAsync();
                    ListItems = new ObservableCollection<object>(clients.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Client {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteClient(object param)
        {
            try
            {
                Client client = (Client)SelectedItems;
                await _mainViewModel.DeleteClient(client.ClientId);

                // Update List of Clients 
                var clients = await _mainViewModel.GetAllClientsAsync();
                ListItems = new ObservableCollection<object>(clients.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete Client {ex.Message}");
            }
        }
        #endregion

        #region Users Methods
        private async Task OnUsersCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;
            VisibleFirstTextBox = Visibility.Visible;
            VisibleSecondTextBox = Visibility.Visible;
            VisibleThirdTextBox = Visibility.Visible;
            VisibleFifthTextBox = Visibility.Visible;
            VisibilitiButtonsRole = Visibility.Visible;

            // Fill in List
            var users = await _mainViewModel.GetAllUsers();
            ListItems = new ObservableCollection<object>(users.Cast<object>());

            // Fill in Combo box addresses
            var addreses = await _mainViewModel.GetAllAddresses();
            ItemsFirstCombo = new ObservableCollection<object>(addreses.Cast<object>());

            // Fill Text Discriptions
            TextDiscrFirstCombo = "Address";
            TextDiscrFirstTextBox = "Email";
            TextDiscrSecondTextBox = "Password";
            TextDiscrThirdTextBox = "Name";
            TextDiscrFifthTextBox = "Phone";

            currentMenuItem = EnumAdminMenuItems.Users;
        }

        private async Task UpdateUser(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && !string.IsNullOrEmpty(ThirdTextBox) && FifthTextBox.All(char.IsDigit))
            {
                try
                {
                    // Update User
                    User selectedUser = (User)SelectedItems;

                    User user = new User
                    {
                        UserId = selectedUser.UserId,
                        Username = FirstTextBox,
                        Password = SecondTextBox,
                        Name = ThirdTextBox,
                        Phone = int.Parse(FifthTextBox),
                        Address = (Address)SelectedFirstCombo
                    };

                    await _mainViewModel.UpdateUser(user);

                    // Update List of Users 
                    var users = await _mainViewModel.GetAllUsers();
                    ListItems = new ObservableCollection<object>(users.Cast<object>());

                    // Clear
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update User {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddUser(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && !string.IsNullOrEmpty(ThirdTextBox) && FifthTextBox.All(char.IsDigit))
            {
                try
                {
                    // Add User
                    User user = new User
                    {
                        Username = FirstTextBox,
                        Password = SecondTextBox,
                        Name = ThirdTextBox,
                        Phone = int.Parse(FifthTextBox),
                        Address = (Address)SelectedFirstCombo,
                    };
                    await _mainViewModel.AddNewUser(user);

                    // Update List of Users 
                    var users = await _mainViewModel.GetAllUsers();
                    ListItems = new ObservableCollection<object>(users.Cast<object>());

                    // Clear
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add User {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteUser(object param)
        {
            try
            {
                User user = (User)SelectedItems;
                await _mainViewModel.DeleteUserAsync(user.UserId);

                // Update List of Users 
                var users = await _mainViewModel.GetAllUsers();
                ListItems = new ObservableCollection<object>(users.Cast<object>());

                // Clear
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete User {ex.Message}");
            }
        }

        // Role
        private async Task OnUpRole(object param)
        {
            try
            {
                if (SelectedItems != null)
                {
                    User user = (User)SelectedItems;

                    if (user.RoleId > 1)
                    {
                        await _mainViewModel.AssignRole(user.UserId, user.RoleId-1);

                        // For admin dont need add new Employer
                        if (user.RoleId - 1 != 1)
                        {
                            await _mainViewModel.AddEmployer(user.UserId, 1, "");
                        }

                        // Update List of Users 
                        var users = await _mainViewModel.GetAllUsers();
                        ListItems = new ObservableCollection<object>(users.Cast<object>());

                        // Clear
                        ClearAllInputs();
                    }
                    else
                    {
                        ErrorMessage = "Select User has maximum role (Admin or God)";
                    }

                }
                else
                {
                    ErrorMessage = "Select User from list!";
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Up role User {ex.Message}");
            }
        }

        private async Task OnDownRole(object param)
        {
            try
            {
                if (SelectedItems != null)
                {
                    User user = (User)SelectedItems;
                    if (user.RoleId < 3)
                    {
                        await _mainViewModel.AssignRole(user.UserId, user.RoleId+1);

                        // Update List of Users 
                        var users = await _mainViewModel.GetAllUsers();
                        ListItems = new ObservableCollection<object>(users.Cast<object>());

                        // Clear
                        ClearAllInputs();
                    }
                    else
                    {
                        ErrorMessage = "Select User has minimum role (User or Homeless)";
                    }

                }
                else
                {
                    ErrorMessage = "Select User from list!";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Down role User {ex.Message}");
            }
        }
        #endregion

        #region Employers Methods
        private async Task OnEmployersCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;
            VisibleSecondCombo = Visibility.Visible;
            VisibleThirdCombo = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;
            VisibleSecondTextBox = Visibility.Visible;
            VisibleThirdTextBox = Visibility.Visible;

            // Fill in List 
            var eployers = await _mainViewModel.GetAllEmployersAsync();
            ListItems = new ObservableCollection<object>(eployers.Cast<object>());

            // Fill in Combo Boxes
            var office =  _mainViewModel.Offices;
            ItemsFirstCombo = new ObservableCollection<object>(office.Cast<object>());

            var supervisor = await _mainViewModel.GetAllEmployersAsync();
            ItemsSecondCombo = new ObservableCollection<object>(supervisor.Cast<object>());

            var address = await _mainViewModel.GetAllAddresses();
            ItemsThirdCombo = new ObservableCollection<object>(address.Cast<object>());

            // Fill Text Discriptions
            TextDiscrFirstCombo = "Office";
            TextDiscrSecondCombo = "Supervisor";
            TextDiscrThirdCombo = "Address";

            TextDiscrFirstTextBox = "Speciality";
            TextDiscrSecondTextBox = "NameEmployee";
            TextDiscrThirdTextBox = "Phone";

            currentMenuItem = EnumAdminMenuItems.Employers;
        }

        private async Task AddEmployer(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && !string.IsNullOrEmpty(ThirdTextBox))
            {
                try
                {
                    // Add Employer
                    Employer employer = new Employer
                    {
                        Speciality = FirstTextBox,
                        NameEmployee = SecondTextBox,
                        Phone = int.Parse(ThirdTextBox),
                        Office = (Office)SelectedFirstCombo,
                        Supervisor = (Employer)SelectedSecondCombo,
                        Address = (Address)SelectedThirdCombo
                    };
                    await _mainViewModel.AddEmployerAsync(employer);

                    // Update List of Employers 
                    var employers = await _mainViewModel.GetAllEmployersAsync();
                    ListItems = new ObservableCollection<object>(employers.Cast<object>());

                    // Clear
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Employer {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task UpdateEmployer(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && !string.IsNullOrEmpty(ThirdTextBox))
            {
                try
                {
                    Employer selectedemployer = (Employer)SelectedItems;
                    // Update Employer
                    Employer employer = new Employer
                    {
                        EmployerId = selectedemployer.EmployerId,
                        Speciality = FirstTextBox,
                        NameEmployee = SecondTextBox,
                        Phone = int.Parse(ThirdTextBox),
                        Office = (Office)SelectedFirstCombo,
                        Supervisor = (Employer)SelectedSecondCombo,
                        Address = (Address)SelectedThirdCombo
                    };
                    await _mainViewModel.AddEmployerAsync(employer);

                    // Update List of Employers 
                    var employers = await _mainViewModel.GetAllEmployersAsync();
                    ListItems = new ObservableCollection<object>(employers.Cast<object>());

                    // Clear
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Employer {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }


        #endregion

        #region Cars Methods
        private async Task OnCarsCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;
            VisibleSecondCombo = Visibility.Visible;

            VisibleDate = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;
            VisibleSecondTextBox = Visibility.Visible;
            VisibleThirdTextBox = Visibility.Visible;

            // Fill in List
            var cars =  _mainViewModel.Cars;
            ListItems = new ObservableCollection<object>(cars.Cast<object>());

            // Fill in Combo box addresses
            var offoces =  _mainViewModel.Offices;
            ItemsFirstCombo = new ObservableCollection<object>(offoces.Cast<object>());

            var clients = _mainViewModel.Clients;
            ItemsSecondCombo = new ObservableCollection<object>(clients.Cast<object>());


            // Fill Text Discriptions
            TextDiscrFirstCombo = "Office";
            TextDiscrSecondCombo = "Client";

            TextDiscrDate = "Date Reservace";

            TextDiscrFirstTextBox = "SPZ";
            TextDiscrSecondTextBox = "Car brand";
            TextDiscrThirdTextBox = "Symptoms";

            currentMenuItem = EnumAdminMenuItems.Cars;
        }

        private async Task UpdateCar(object param)
        {
            if (SelectedDate != null && SelectedFirstCombo != null && SelectedSecondCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && !string.IsNullOrEmpty(ThirdTextBox))
            {
                try
                {
                    // Update Car
                    Car selectedCar = (Car)SelectedItems;
                    Car car = new Car()
                    {
                        CarId = selectedCar.CarId,
                        SPZ = FirstTextBox,
                        CarBrand = SecondTextBox,
                        Symptoms = ThirdTextBox,
                        Reservation = new Reservation
                        {
                            DateReservace = SelectedDate,
                            Office = (Office)SelectedFirstCombo,
                            Client = (Client)SelectedSecondCombo
                        }
                    };
                    await _mainViewModel.UpdateCar(car);

                    // Update List of Cars 
                    await _mainViewModel.GetAllCars();
                    var cars = _mainViewModel.Cars;
                    ListItems = new ObservableCollection<object>(cars.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Client {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddCar(object param)
        {
            if (SelectedDate != null && SelectedFirstCombo != null && SelectedSecondCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(SecondTextBox) && !string.IsNullOrEmpty(ThirdTextBox))
            {
                try
                {
                    // Add Car
                    Car car = new Car
                    {
                        SPZ = FirstTextBox,
                        CarBrand = SecondTextBox,
                        Symptoms = ThirdTextBox,
                        Reservation = new Reservation 
                        {
                            DateReservace = SelectedDate,
                            Office = (Office)SelectedFirstCombo,
                            Client = (Client)SelectedSecondCombo
                        }
                    };

                    await _mainViewModel.AddCar(car);

                    // Update List of Car 
                    await _mainViewModel.GetAllCars();
                    var cars = _mainViewModel.Cars;
                    ListItems = new ObservableCollection<object>(cars.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Car {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteCar(object param)
        {
            try
            {
                Car car = (Car)SelectedItems;
                await _mainViewModel.DeleteCar(car.CarId);

                // Update List of Car 
                await _mainViewModel.GetAllCars();
                var cars = _mainViewModel.Cars;
                ListItems = new ObservableCollection<object>(cars.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete Car {ex.Message}");
            }
        }
        #endregion

        #region Reservations Methods
        private async Task OnReservationsCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;
            VisibleSecondCombo = Visibility.Visible;

            VisibleDate = Visibility.Visible;

            // Fill in List
            var reservations = await _mainViewModel.GetAllReservations();
            ListItems = new ObservableCollection<object>(reservations.Cast<object>());

            // Fill in Combo box addresses
            var offoces = _mainViewModel.Offices;
            ItemsFirstCombo = new ObservableCollection<object>(offoces.Cast<object>());

            var clients = _mainViewModel.Clients;
            ItemsSecondCombo = new ObservableCollection<object>(clients.Cast<object>());


            // Fill Text Discriptions
            TextDiscrFirstCombo = "Office";
            TextDiscrSecondCombo = "Client";

            TextDiscrDate = "Date Reservace";

            currentMenuItem = EnumAdminMenuItems.Reservations;
        }

        private async Task UpdateReservation(object param)
        {
            if (SelectedDate != null && SelectedFirstCombo != null && SelectedSecondCombo != null )
            {
                try
                {
                    // Update Car
                    Reservation selectedReservation = (Reservation)SelectedItems;
                    Reservation reservation = new Reservation
                    {
                        ReservationId = selectedReservation.ReservationId,
                        DateReservace = SelectedDate,
                        Office = (Office)SelectedFirstCombo,
                        Client = (Client)SelectedSecondCombo
                    };

                    await _mainViewModel.UpdateReservation(reservation);

                    // Update List of Reservations 
                    var reservatons = await _mainViewModel.GetAllReservations();
                    ListItems = new ObservableCollection<object>(reservatons.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Reservation {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddReservation(object param)
        {
            if (SelectedDate != null && SelectedFirstCombo != null && SelectedSecondCombo != null )
            {
                try
                {
                    // Add Reservation
                    Reservation reservation = new Reservation
                    {
                        DateReservace = SelectedDate,
                        Office = (Office)SelectedFirstCombo,
                        Client = (Client)SelectedSecondCombo
                    };

                    await _mainViewModel.InsertReservation(reservation);

                    // Update List of Reservations 
                    var reservatons = await _mainViewModel.GetAllReservations();
                    ListItems = new ObservableCollection<object>(reservatons.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Car {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteReservation(object param)
        {
            try
            {
                Reservation reservation = (Reservation)SelectedItems;
                await _mainViewModel.DeleteReservation(reservation.ReservationId);

                // Update List of Reservations 
                var reservatons = await _mainViewModel.GetAllReservations();
                ListItems = new ObservableCollection<object>(reservatons.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete Reservation {ex.Message}");
            }
        }
        #endregion

        #region Servis Offers Methods
        private async Task OnServisOffersCommand(object param)
        {

        }
        #endregion

        #region Offices Methods
        private async Task OnOfficesCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;

            // Fill in List
            var offices = _mainViewModel.Offices;
            ListItems = new ObservableCollection<object>(offices.Cast<object>());

            // Fill in Combo box addresses
            var addreses = await _mainViewModel.GetAllAddresses();
            ItemsFirstCombo = new ObservableCollection<object>(addreses.Cast<object>());
            

            // Fill Text Discriptions
            TextDiscrFirstCombo = "Address";

            TextDiscrFirstTextBox = "OfficeSize";

            currentMenuItem = EnumAdminMenuItems.Offices;
        }

        private async Task UpdateOffice(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && FirstTextBox.All(char.IsDigit))
            {
                try
                {
                    // Update Office
                    Office selectedOffice = (Office)SelectedItems;
                    Office office = new Office
                    {
                        OfficeId = selectedOffice.OfficeId,
                        Address = (Address)SelectedFirstCombo,
                        OfficeSize = int.Parse(FirstTextBox)
                    };

                    await _mainViewModel.UpdateOfficeAsync(office);

                    // Update List of Offices 
                    await _mainViewModel.FillinOutOfficesList();
                    var offices = _mainViewModel.Offices;
                    ListItems = new ObservableCollection<object>(offices.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Office {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddOffice(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && FirstTextBox.All(char.IsDigit))
            {
                try
                {
                    // Add Office
                    Office office = new Office
                    {
                        Address = (Address)SelectedFirstCombo,
                        OfficeSize = int.Parse(FirstTextBox)
                    };

                    await _mainViewModel.InsertOfficeAsync(office);

                    // Update List of Offices 
                    await _mainViewModel.FillinOutOfficesList();
                    var offices = _mainViewModel.Offices;
                    ListItems = new ObservableCollection<object>(offices.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Office {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteOffice(object param)
        {
            try
            {
                Office office = (Office)SelectedItems;
                await _mainViewModel.DeleteOfficeAsync(office.OfficeId);

                // Update List of Offices 
                await _mainViewModel.FillinOutOfficesList();
                var offices = _mainViewModel.Offices;
                ListItems = new ObservableCollection<object>(offices.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete Office {ex.Message}");
            }
        }
        #endregion

        #region Spair Parts Methods
        private async Task OnSpairPartsCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;
            VisibleSecondTextBox = Visibility.Visible;
            VisibleThirdTextBox = Visibility.Visible;

            // Fill in List
            var spareParts = _mainViewModel.SpareParts;
            ListItems = new ObservableCollection<object>(spareParts.Cast<object>());

            // Fill in Combo box SpairParts
            var offices =  _mainViewModel.Offices;
            ItemsFirstCombo = new ObservableCollection<object>(offices.Cast<object>());


            // Fill Text Discriptions
            TextDiscrFirstCombo = "Office";

            TextDiscrFirstTextBox = "Speciality";
            TextDiscrSecondTextBox = "Price";
            TextDiscrThirdTextBox = "Stock Availability 'Y' or 'N'"; 

            currentMenuItem = EnumAdminMenuItems.SpairParts;
        }

        private async Task UpdateSpairParts(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(ThirdTextBox) && ThirdTextBox.Length == 1 && !string.IsNullOrEmpty(SecondTextBox) && SecondTextBox.All(char.IsDigit))
            {
                try
                {
                    // Update SpairParts
                    SparePart selectedSparePart = (SparePart)SelectedItems;
                    SparePart spairPart = new SparePart
                    {
                        SparePartId = selectedSparePart.SparePartId,
                        Speciality = FirstTextBox,
                        Price = int.Parse(SecondTextBox),
                        StockAvailability = char.Parse(ThirdTextBox),
                        Office = (Office)SelectedFirstCombo
                    };

                    await _mainViewModel.UpdateSparePart(spairPart);

                    // Update List of SpairParts 
                    await _mainViewModel.GetAllSpareParts();
                    var spareParts = _mainViewModel.SpareParts;
                    ListItems = new ObservableCollection<object>(spareParts.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update SpairParts {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddSpairParts(object param)
        {
            if (SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && !string.IsNullOrEmpty(ThirdTextBox) && ThirdTextBox.Length == 1 && !string.IsNullOrEmpty(SecondTextBox) && SecondTextBox.All(char.IsDigit))
            {
                try
                {
                    // Add SparePart
                    SparePart spairPart = new SparePart
                    {
                        Speciality = FirstTextBox,
                        Price = int.Parse(SecondTextBox),
                        StockAvailability = char.Parse(ThirdTextBox),
                        Office = (Office)SelectedFirstCombo
                    };

                    await _mainViewModel.InsertSparePart(spairPart);

                    // Update List of SpairParts 
                    await _mainViewModel.GetAllSpareParts();
                    var spareParts = _mainViewModel.SpareParts;
                    ListItems = new ObservableCollection<object>(spareParts.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add SpairParts {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteSpairParts(object param)
        {
            try
            {
                SparePart sparePart = (SparePart)SelectedItems;
                await _mainViewModel.DeleteSparePart(sparePart.SparePartId);

                // Update List of SpairParts 
                await _mainViewModel.GetAllSpareParts();
                var spareParts = _mainViewModel.SpareParts;
                ListItems = new ObservableCollection<object>(spareParts.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete SpairParts {ex.Message}");
            }
        }
        #endregion

        #region Servis Spair Methods
        private async Task OnServisSpairCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;
            VisibleSecondCombo = Visibility.Visible;

            // Fill in List
            var serviceSpares = await _mainViewModel.GetAllServiceSparesAsync();
            ListItems = new ObservableCollection<object>(serviceSpares.Cast<object>());

            // Fill in Combo box ServisSpair
            var spareParts = _mainViewModel.SpareParts;
            ItemsFirstCombo = new ObservableCollection<object>(spareParts.Cast<object>());

            var serviceOffer = _mainViewModel.ServiceOffers;
            ItemsSecondCombo = new ObservableCollection<object>(serviceOffer.Cast<object>());


            // Fill Text Discriptions
            TextDiscrFirstCombo = "Spare Part";
            TextDiscrSecondCombo = "Service Offer";


            currentMenuItem = EnumAdminMenuItems.ServisSpair;
        }

        private async Task AddServiceSpare(object param)
        {
            if (SelectedFirstCombo != null && SelectedSecondCombo != null)
            {
                try
                {
                    // Add ServiceSpare
                    await _mainViewModel.AddServiceSpare((SparePart)SelectedFirstCombo, (ServiceOffer)SelectedSecondCombo);

                    // Update List of ServiceSpare 
                    var serviceSpars = await _mainViewModel.GetAllServiceSparesAsync();
                    ListItems = new ObservableCollection<object>(serviceSpars.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add ServiceSpare {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteServiceSpare(object param)
        {
            try
            {
                SparePart sparePart = (SparePart)SelectedFirstCombo;
                ServiceOffer serviceOffer = (ServiceOffer)SelectedSecondCombo;
                await _mainViewModel.RemoveServiceSpare(sparePart.SparePartId, serviceOffer.OfferId);

                // Update List of ServiceSpare 
                var serviceSpars = await _mainViewModel.GetAllServiceSparesAsync();
                ListItems = new ObservableCollection<object>(serviceSpars.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete ServiceSpare {ex.Message}");
            }
        }

        #endregion

        #region Bills Methods
        private async Task OnBillsCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;

            VisibleDate = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;

            // Fill in List
            var bills = await _mainViewModel.GetAllBills();
            ListItems = new ObservableCollection<object>(bills.Cast<object>());

            // Fill in Combo box 
            var serviceOffers = _mainViewModel.ServiceOffers;
            ItemsFirstCombo = new ObservableCollection<object>(serviceOffers.Cast<object>());


            // Fill Text Discriptions
            TextDiscrFirstCombo = "Service Offer";

            TextDiscrDate = "Date Bill";

            TextDiscrFirstTextBox = "Price";

            currentMenuItem = EnumAdminMenuItems.Bills;
        }

        private async Task UpdateBill(object param)
        {
            if (SelectedDate != null && SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && FirstTextBox.All(char.IsDigit))
            {
                try
                {
                    // Update Bill
                    Bill selectedBill = (Bill)SelectedItems;
                    Bill bill = new Bill
                    {
                        BillId = selectedBill.BillId,
                        Price = decimal.Parse(FirstTextBox),
                        ServiceOffer = (ServiceOffer)SelectedFirstCombo,
                        DateBill = SelectedDate
                    };

                    await _mainViewModel.UpdateBill(bill);

                    // Update List of Bill 
                    var bills = await _mainViewModel.GetAllBills();
                    ListItems = new ObservableCollection<object>(bills.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Bill {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddBill(object param)
        {
            if (SelectedDate != null && SelectedFirstCombo != null && !string.IsNullOrEmpty(FirstTextBox) && FirstTextBox.All(char.IsDigit))
            {
                try
                {
                    // Add Bill
                    Bill bill = new Bill
                    {
                        Price = decimal.Parse(FirstTextBox),
                        ServiceOffer = (ServiceOffer)SelectedFirstCombo,
                        DateBill = SelectedDate
                    };

                    await _mainViewModel.AddBill(bill);

                    // Update List of Bill 
                    var bills = await _mainViewModel.GetAllBills();
                    ListItems = new ObservableCollection<object>(bills.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Bill {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeleteBill(object param)
        {
            try
            {
                Bill bill = (Bill)SelectedItems;
                await _mainViewModel.DeleteBill(bill.BillId);

                // Update List of Bill 
                var bills = await _mainViewModel.GetAllBills();
                ListItems = new ObservableCollection<object>(bills.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete Bill {ex.Message}");
            }
        }
        #endregion

        #region Payments Methods
        private async Task OnPaymentsCommand(object param)
        {
            ChangeMenuOnFields();

            // Needed Fields
            VisibleFirstCombo = Visibility.Visible;
            VisibleSecondCombo = Visibility.Visible;
            VisibleThirdCombo = Visibility.Visible;

            VisibleFirstTextBox = Visibility.Visible;
            VisibleSecondTextBox = Visibility.Visible;

            // Fill in List
            var payments = await _mainViewModel.GetAllPayments();
            ListItems = new ObservableCollection<object>(payments.Cast<object>());

            // Fill in Combo box 
            var bills = await _mainViewModel.GetAllBills();
            ItemsFirstCombo = new ObservableCollection<object>(bills.Cast<object>());

            var clients =  _mainViewModel.Clients;
            ItemsSecondCombo = new ObservableCollection<object>(clients.Cast<object>());

            var paymentTypes = await _mainViewModel.GetAllPaymentTypes();
            ItemsThirdCombo = new ObservableCollection<object>(paymentTypes.Cast<object>());


            // Fill Text Discriptions
            TextDiscrFirstCombo = "Bill";
            TextDiscrSecondCombo = "Client";
            TextDiscrThirdCombo = "Payment Type";


            TextDiscrFirstTextBox = "Card";
            TextDiscrSecondTextBox = "Cash";

            currentMenuItem = EnumAdminMenuItems.Payments;
        }

        private async Task UpdatePayment(object param)
        {
            if (SelectedFirstCombo != null && SelectedSecondCombo != null && SelectedThirdCombo != null)
            {
                try
                {
                    // Update Payment
                    Payment selectedPayment = (Payment)SelectedItems;
                    Payment payment = new Payment
                    {
                        PaymentId = selectedPayment.PaymentId,
                        Bill = (Bill)SelectedFirstCombo,
                        Client = (Client)SelectedSecondCombo,
                        PaymentType = (PaymentType)SelectedThirdCombo,
                    };

                    if(!string.IsNullOrEmpty(FirstTextBox) && FirstTextBox.All(char.IsDigit))
                    {
                        payment.Card = new Card 
                        {
                            PaymentId = payment.PaymentId,
                            NumberCard = int.Parse(FirstTextBox) 
                        };
                    }
                    else if (!string.IsNullOrEmpty(SecondTextBox) && SecondTextBox.All(char.IsDigit))
                    {
                        payment.Cash = new Cash
                        {
                            PaymentId = payment.PaymentId,
                            Taken = int.Parse(SecondTextBox),
                            Given = int.Parse(SecondTextBox) - payment.Bill.Price
                        };
                    }
                    else
                    {
                        ErrorMessage = "Fill Card or cash numbers!";
                        return;
                    }

                    await _mainViewModel.AddPayment(payment);

                    // Update List of Payment 
                    var payments = await _mainViewModel.GetAllPayments();
                    ListItems = new ObservableCollection<object>(payments.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Update Payment {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task AddPayment(object param)
        {
            if (SelectedFirstCombo != null && SelectedSecondCombo != null && SelectedThirdCombo != null)
            {
                try
                {
                    // Update Payment
                    Payment selectedPayment = (Payment)SelectedItems;
                    Payment payment = new Payment
                    {
                        Bill = (Bill)SelectedFirstCombo,
                        Client = (Client)SelectedSecondCombo,
                        PaymentType = (PaymentType)SelectedThirdCombo,
                    };

                    if (!string.IsNullOrEmpty(FirstTextBox) && FirstTextBox.All(char.IsDigit))
                    {
                        payment.Card = new Card
                        {
                            PaymentId = payment.PaymentId,
                            NumberCard = int.Parse(FirstTextBox)
                        };
                    }
                    else if (!string.IsNullOrEmpty(SecondTextBox) && SecondTextBox.All(char.IsDigit))
                    {
                        payment.Cash = new Cash
                        {
                            PaymentId = payment.PaymentId,
                            Taken = int.Parse(SecondTextBox),
                            Given = int.Parse(SecondTextBox) - payment.Bill.Price
                        };
                    }
                    else
                    {
                        ErrorMessage = "Fill Card or cash numbers!";
                        return;
                    }

                    await _mainViewModel.AddPayment(payment);

                    // Update List of Payment 
                    var payments = await _mainViewModel.GetAllPayments();
                    ListItems = new ObservableCollection<object>(payments.Cast<object>());

                    // Clear 
                    ClearAllInputs();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Error] Add Payment {ex.Message}");
                }
            }
            else
            {
                ErrorMessage = "Fill all fields!";
            }

        }

        private async Task DeletePayment(object param)
        {
            try
            {
                Payment payment = (Payment)SelectedItems;
                await _mainViewModel.DeletePayment(payment.PaymentId);

                // Update List of Payment 
                var payments = await _mainViewModel.GetAllPayments();
                ListItems = new ObservableCollection<object>(payments.Cast<object>());

                // Clear 
                ClearAllInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Error] Delete Payment {ex.Message}");
            }
        }

        #endregion

    }
}
