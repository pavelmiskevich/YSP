using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllWithUser();
        Task<Offer> GetOfferById(int id);
        Task<IEnumerable<Offer>> GetOffersByUserId(int userId);
        Task<Offer> CreateOffer(Offer newOffer);
        Task UpdateOffer(Offer offerToBeUpdated, Offer offer);
        Task DeleteOffer(Offer offer);
    }
}
