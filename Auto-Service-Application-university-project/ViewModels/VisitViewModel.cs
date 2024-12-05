using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Auto_Service_Application_university_project.ViewModels
{
    public class VisitViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        // For Lists
        
        private ObservableCollection<SparePart> _usedSpareParts;
        private SparePart _selectedUsedSparePart;

        private ObservableCollection<SparePart> _sparePartsinOffice;
        private SparePart _selectedSparePart; 

        private ObservableCollection<ServiceOffer> _serviceOffers;
        private ServiceOffer _selectedServiceOffer;


        private int _pricePerHour;
        private int _workingHour;
        #endregion

        #region Properties
        public ObservableCollection<SparePart> UsedSpareParts { get => _usedSpareParts; set => SetProperty(ref _usedSpareParts, value, nameof(UsedSpareParts)); }
        public SparePart SelectedUsedSparePart { get => _selectedUsedSparePart; set => SetProperty(ref _selectedUsedSparePart, value, nameof(SelectedUsedSparePart)); }

        public ObservableCollection<SparePart> SpareParts { get => _sparePartsinOffice; set => SetProperty(ref _sparePartsinOffice, value, nameof(SpareParts)); }
        public SparePart SelectedSparePart { get => _selectedSparePart; set => SetProperty(ref _selectedSparePart, value, nameof(SelectedSparePart)); }

        public ObservableCollection<ServiceOffer> ServiceOffers { get => _serviceOffers; set => SetProperty(ref _serviceOffers, value, nameof(ServiceOffers)); }
        public ServiceOffer SelectedServiceOffer { get => _selectedServiceOffer; set => SetProperty(ref _selectedServiceOffer, value, nameof(SelectedServiceOffer)); }

        public int PricePerHour { get => _pricePerHour; set => SetProperty(ref _pricePerHour, value, nameof(PricePerHour)); }
        public int WorkingHour { get => _workingHour; set => SetProperty(ref _workingHour, value, nameof(WorkingHour)); }


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

            
            SpareParts = _mainViewModel.SparePartsByOffice;

            ServiceOffers = _mainViewModel.ServiceOffersByOffice;

            #region Init Commands
            FinishWorkCommand = new MyICommand<object>(async parametr => await OnFinishWork(parametr));
            DeleteCommand = new MyICommand<object>(async parametr => await OnDelete(parametr));

            DeleteFromUsedSparePartCommand = new MyICommand(OnDeleteFromUsedList);
            AddToUsedSparePartCommand = new MyICommand(OnAddToUsedList);
            #endregion
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
