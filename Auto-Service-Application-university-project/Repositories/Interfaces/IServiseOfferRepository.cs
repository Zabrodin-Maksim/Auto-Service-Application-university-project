using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IServiseOfferRepository
    {
        IEnumerable<ServiseOffer> GetAll();
        ServiseOffer GetByOfferId(int offerId);
        void Add(ServiseOffer serviseOffer, User currentUser);
        void Update(ServiseOffer serviseOffer, User currentUser);
        void Delete(int offerId, User currentUser);
    }
}
