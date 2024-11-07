using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.ViewModels.HelperViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private readonly ICardRepository _cardRepository;

        public ObservableCollection<Card> Cards { get; set; }

        public CardViewModel(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
            Cards = new ObservableCollection<Card>(_cardRepository.GetAll());
        }
    }
}
