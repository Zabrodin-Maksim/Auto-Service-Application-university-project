using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAll();
        Card GetById(int id);
        void Add(Card card);
        void Update(Card card);
        void Delete(int id);
    }
}
