﻿using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {

        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        // Visibilities
        private Visibility _visibleNumberCard;
        private Visibility _visibilityCashTaken;

        // List
        private ObservableCollection<Bill> _bills;
        private Bill _selectedBill;

        //Combo Box
        private ObservableCollection<PaymentType> _paymentTypes;
        private PaymentType _selectedPaymentType;

        // Fields
        private string _numberCard;
        private string _cashTaken;

        // Error message
        private string _errorMessage;
        #endregion

        #region Properties
        public Visibility VisibleNumberCard { get => _visibleNumberCard; set => SetProperty(ref _visibleNumberCard, value, nameof(VisibleNumberCard)); }
        public Visibility VisibilityCashTaken { get => _visibilityCashTaken; set => SetProperty(ref _visibilityCashTaken, value, nameof(VisibilityCashTaken)); }

        public ObservableCollection<Bill> Bills { get => _bills; set => SetProperty(ref _bills, value, nameof(Bills)); }
        public Bill SelectedBills { get => _selectedBill; set => SetProperty(ref _selectedBill, value, nameof(SelectedBills)); }

        public ObservableCollection<PaymentType> PaymentTypes { get => _paymentTypes; set => SetProperty(ref _paymentTypes, value, nameof(PaymentTypes)); }
        public PaymentType PaymentTypeSelected { get => _selectedPaymentType; 
            set
            {
                SetProperty(ref _selectedPaymentType, value, nameof(PaymentTypeSelected));
                if (_selectedPaymentType != null)
                {
                    if (_selectedPaymentType.TypeName == "card")
                    {
                        VisibilityCashTaken = Visibility.Collapsed;
                        VisibleNumberCard = Visibility.Visible;
                    }
                    else
                    {
                        VisibleNumberCard = Visibility.Collapsed;
                        VisibilityCashTaken = Visibility.Visible;
                    }
                }
            }
        }

        public string NumberCard
        {
            get => _numberCard;
            set
            {
                if (value.All(char.IsDigit))
                {
                    if (value.Length <= 10)
                    {
                        SetProperty(ref _numberCard, value, nameof(NumberCard));
                    }
                    else
                    {
                        ErrorMessage = "Maximum of 10 digits!";
                    }
                }
                else
                {
                    ErrorMessage = "Enter numbers only!";
                }
            }
        }
        public string CashTaken
        {
            get => _cashTaken;
            set
            {
                if (value.All(char.IsDigit))
                {
                    if (value.Length <= 10)
                    {
                        SetProperty(ref _cashTaken, value, nameof(CashTaken));
                    }
                    else
                    {
                        ErrorMessage = "Maximum of 10 digits!";
                    }
                }
                else
                {
                    ErrorMessage = "Enter numbers only!";
                }
            }
        }

        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }
        #endregion

        #region Commands
        public ICommand PaidCommand { get; }
        public ICommand ClearCommand { get; }
        #endregion

        public PaymentViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            FillinOutAllLists();

            

            #region Init Commands
            PaidCommand = new MyICommand(OnPaid);
            ClearCommand = new MyICommand(OnClear);
            #endregion
        }

        private async Task FillinOutAllLists()
        {
            Bills = await _mainViewModel.GetAllBills();
            PaymentTypes = await _mainViewModel.GetAllPaymentTypes();
        }

        private void OnPaid()
        {

        }

        private void OnClear() 
        {
            SelectedBills = null;

            NumberCard = "";
            CashTaken = "";
        }

        private bool CheckInputs()
        {
            return false;
        }
    }
}