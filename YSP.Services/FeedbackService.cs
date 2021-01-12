using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Feedback> CreateFeedback(Feedback newFeedback)
        {
            await _unitOfWork.Feedbacks.AddAsync(newFeedback);
            await _unitOfWork.CommitAsync();
            return newFeedback;
        }

        public async Task DeleteFeedback(Feedback feedback)
        {
            _unitOfWork.Feedbacks.Remove(feedback);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Feedback>> GetAllWithUser()
        {
            return await _unitOfWork.Feedbacks
                .GetAllWithUserAsync();
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            return await _unitOfWork.Feedbacks
                .GetByIdAsync(id);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserId(int userId)
        {
            return await _unitOfWork.Feedbacks
                .GetAllWithUserByUserIdAsync(userId);
        }

        public async Task UpdateFeedback(Feedback feedbackToBeUpdated, Feedback feedback)
        {
            feedbackToBeUpdated.Name = feedback.Name;
            feedbackToBeUpdated.UserId = feedback.UserId;
            feedbackToBeUpdated.IsActive = feedback.IsActive;

            await _unitOfWork.CommitAsync();
        }
    }
}
