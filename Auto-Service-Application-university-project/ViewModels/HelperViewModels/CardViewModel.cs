using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.ViewModels.HelperViewModels
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
