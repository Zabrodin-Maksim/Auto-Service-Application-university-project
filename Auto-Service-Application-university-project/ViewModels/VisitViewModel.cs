using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class VisitViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        // Visibilities
        private Visibility _visibilityFinishWork;
        private Visibility _visibilityDelete;

        #region For Lists

        private ObservableCollection<SparePart> _usedSpareParts;
        private SparePart _selectedUsedSparePart;

        private ObservableCollection<SparePart> _sparePartsinOffice;
        private SparePart _selectedSparePart; 

        private ObservableCollection<ServiceOffer> _serviceOffers;
        private ServiceOffer _selectedServiceOffer;
        #endregion

        // Fields
        private string _pricePerHour;
        private string _workingHour;

        // Error message
        private string _errorMessage;
        #endregion

        #region Properties
        // Visibilities
        public Visibility VisibilityFinishWork { get => _visibilityFinishWork; set => SetProperty(ref _visibilityFinishWork, value, nameof(VisibilityFinishWork)); }
        public Visibility VisibilityDelete { get => _visibilityDelete; set => SetProperty(ref _visibilityDelete, value, nameof(VisibilityDelete)); }

        public ObservableCollection<SparePart> UsedSpareParts { get => _usedSpareParts; set => SetProperty(ref _usedSpareParts, value, nameof(UsedSpareParts)); }
        public SparePart SelectedUsedSparePart { get => _selectedUsedSparePart; set => SetProperty(ref _selectedUsedSparePart, value, nameof(SelectedUsedSparePart)); }

        public ObservableCollection<SparePart> SpareParts { get => _sparePartsinOffice; set => SetProperty(ref _sparePartsinOffice, value, nameof(SpareParts)); }
        public SparePart SelectedSparePart { get => _selectedSparePart; set => SetProperty(ref _selectedSparePart, value, nameof(SelectedSparePart)); }

        public ObservableCollection<ServiceOffer> ServiceOffers { get => _serviceOffers; set => SetProperty(ref _serviceOffers, value, nameof(ServiceOffers)); }
        public ServiceOffer SelectedServiceOffer { get => _selectedServiceOffer; set => SetProperty(ref _selectedServiceOffer, value, nameof(SelectedServiceOffer)); }

        public string PricePerHour { get => _pricePerHour; set
            {
                if (value.All(char.IsDigit) && value.Length <= 9 && value.Length >= 1)
                {
                    SetProperty(ref _pricePerHour, value, nameof(PricePerHour));
                }
                else
                {
                    ErrorMessage = "";
                    ErrorMessage = "Use only numbers! Max 9 numbers";
                }
            }
        }
        public string WorkingHour { get => _workingHour; set
            {
                if (value.All(char.IsDigit))
                {
                    SetProperty(ref _workingHour, value, nameof(WorkingHour));
                }
                else
                {
                    ErrorMessage = "";
                    ErrorMessage = "Use only numbers!";
                }
            }
        }

        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }
        #endregion

        #region Commands
        public ICommand FinishWorkCommand { get; }
        public ICommand DeleteCommand { get; }

        // Work with Lists
        public ICommand DeleteFromUsedSparePartCommand { get; }
        public ICommand AddToUsedSparePartCommand { get; }
        #endregion

        public VisitViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            SetVisibilitiesByUserRole();

            #region fill Lists
            FillinOutLists();
            #endregion

            #region Init Commands
            FinishWorkCommand = new MyICommand<object>(async parametr => await OnFinishWork(parametr));
            DeleteCommand = new MyICommand<object>(async parametr => await OnDelete(parametr));

            DeleteFromUsedSparePartCommand = new MyICommand(OnDeleteFromUsedList);
            AddToUsedSparePartCommand = new MyICommand(OnAddToUsedList);
            #endregion
        }

        public void SetVisibilitiesByUserRole()
        {
            switch (_mainViewModel.authenticatedUser.RoleId)
            {
                case 2: 
                    VisibilityDelete = Visibility.Collapsed;
                    VisibilityFinishWork = Visibility.Visible;
                    break;
                case 3:
                    VisibilityDelete = Visibility.Visible;
                    VisibilityFinishWork = Visibility.Collapsed;
                    break;
            }

        }

        public async Task FillinOutLists()
        {
            SpareParts = await _mainViewModel.GetSparePartsByOffice(_mainViewModel.authenticatedEmployer.Office.OfficeId);
            ServiceOffers = await _mainViewModel.FillinOutServiceOffersListByOffice(_mainViewModel.authenticatedEmployer.Office.OfficeId);
        }


        private async Task OnFinishWork(object param)
        {

        }
        private async Task OnDelete(object param)
        {

        }

        #region Methods Work witth lists
        private void OnDeleteFromUsedList()
        {

        }

        private void OnAddToUsedList()
        {

        }
        #endregion
    }
}
