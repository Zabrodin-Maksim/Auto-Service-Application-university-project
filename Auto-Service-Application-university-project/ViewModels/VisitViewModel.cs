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
        // TODO: Заменить тип на SparePart
        private ObservableCollection<Client> _usedSpareParts;
        private Client _selectedUsedSparePart;


        private int _pricePerHour;
        private int _workingHour;
        #endregion

        #region Properties
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
