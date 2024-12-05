﻿using Auto_Service_Application_university_project.Models;
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
        private int flagError;
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
                if (value.All(char.IsDigit))
                {
                    if (value.Length <= 9 && value.Length >= 1)
                    {
                        SetProperty(ref _pricePerHour, value, nameof(PricePerHour));
                    }else
                    {
                        ErrorMessage = "";
                        ErrorMessage = "Fill this field! Maximum 9 digits allowed.";
                    }
                }
                else
                {
                    ErrorMessage = "";
                    ErrorMessage = "Use only numbers!";
                }
            }
        }
        public string WorkingHour { get => _workingHour; set
            {
                if (value.All(char.IsDigit))
                {
                    if (value.Length <= 9 && value.Length >= 1)
                    {
                        SetProperty(ref _workingHour, value, nameof(WorkingHour));
                    }
                    else
                    {
                        ErrorMessage = "";
                        ErrorMessage = "Fill this field! Maximum 9 digits allowed.";
                    }
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

            // Init List Used
            UsedSpareParts = new ObservableCollection<SparePart>();

            // fill Lists
            FillinOutLists();

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
                case 1:
                    VisibilityDelete = Visibility.Visible;
                    VisibilityFinishWork = Visibility.Collapsed;
                    break;
            }

        }

        public async Task FillinOutLists()
        {
            // Admin
            if (_mainViewModel.authenticatedAdmin != null)
            {
                SpareParts = await _mainViewModel.GetSparePartsByOffice(_mainViewModel.authenticatedAdmin.Office.OfficeId);
                ServiceOffers = await _mainViewModel.FillinOutServiceOffersListByOffice(_mainViewModel.authenticatedAdmin.Office.OfficeId);
            }
            // Employer
            else
            {
                SpareParts = await _mainViewModel.GetSparePartsByOffice(_mainViewModel.authenticatedEmployer.Office.OfficeId);
                ServiceOffers = await _mainViewModel.FillinOutServiceOffersListByOffice(_mainViewModel.authenticatedEmployer.Office.OfficeId);
            }
        }


        private async Task OnFinishWork(object param)
        {

        }
        private async Task OnDelete(object param)
        {
            if (SelectedServiceOffer != null)
            {
                _mainViewModel.DeleteServiceOffer(SelectedServiceOffer.OfferId);

                FillinOutLists();
            }
        }

        #region Methods Work witth lists
        private void OnDeleteFromUsedList()
        {
            if (SelectedUsedSparePart != null)
            {
                UsedSpareParts.Remove(SelectedUsedSparePart);
            }
            else
            {
                ErrorMessage = "";
                ErrorMessage = "You stupit bitch? SELECT USED SPARE PART MATHER F*CKER";
            }
        }

        private void OnAddToUsedList()
        {
            
            if (SelectedSparePart != null)
            {
                UsedSpareParts.Add(SelectedSparePart);
            }
            else
            {
                ErrorMessage = "";
                ErrorMessage = "Ouuuu, you realy? SELECT USED SPARE PART MATHER F*CKER";
                if (flagError >= 3)
                {
                    ErrorMessage = "You realy, OK. Well, that's it, you're fired, idiot.";
                }
                flagError++;
            }
        }
        #endregion
    }
}
