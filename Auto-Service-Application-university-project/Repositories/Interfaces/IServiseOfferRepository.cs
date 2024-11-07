using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface IServiseOfferRepository
    {
        IEnumerable<ServiseOffer> GetAll();
        ServiseOffer GetByOfferId(int offerId);
        void Add(ServiseOffer serviseOffer, User currentUser);
        void Update(ServiseOffer serviseOffer, User currentUser);
        void Delete(int offerId, User currentUser);
    }
}
