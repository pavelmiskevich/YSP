using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfferService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Offer> CreateOffer(Offer newOffer)
        {
            await _unitOfWork.Offers.AddAsync(newOffer);
            await _unitOfWork.CommitAsync();
            return newOffer;
        }

        public async Task DeleteOffer(Offer offer)
        {
            _unitOfWork.Offers.Remove(offer);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Offer>> GetAllWithUser()
        {
            return await _unitOfWork.Offers
                .GetAllWithUserAsync();
        }

        public async Task<Offer> GetOfferById(int id)
        {
            return await _unitOfWork.Offers
                .GetByIdAsync(id);
        }

        public async Task<IEnumerable<Offer>> GetOffersByUserId(int userId)
        {
            return await _unitOfWork.Offers
                .GetAllWithUserByUserIdAsync(userId);
        }

        public async Task UpdateOffer(Offer offerToBeUpdated, Offer offer)
        {
            offerToBeUpdated.Name = offer.Name;
            offerToBeUpdated.UserId = offer.UserId;
            offerToBeUpdated.IsActive = offer.IsActive;

            await _unitOfWork.CommitAsync();
        }
    }
}
