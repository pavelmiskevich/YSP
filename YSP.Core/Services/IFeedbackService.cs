using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllWithUser();
        Task<Feedback> GetFeedbackById(int id);
        Task<IEnumerable<Feedback>> GetFeedbacksByUserId(int userId);
        Task<Feedback> CreateFeedback(Feedback newFeedback);
        Task UpdateFeedback(Feedback feedbackToBeUpdated, Feedback feedback);
        Task DeleteFeedback(Feedback feedback);
    }
}
