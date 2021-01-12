using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithFeedbacksAsync();
        Task<IEnumerable<User>> GetAllWithOffersAsync();
        Task<IEnumerable<User>> GetAllWithSitesAsync();
        Task<User> GetWithFeedbacksByIdAsync(int id);
        Task<User> GetWithOffersByIdAsync(int id);
        Task<User> GetWithSitesByIdAsync(int id);

        ValueTask<int> GetAllYandexLimitAsync();
        ValueTask<int> GetYandexLimitByIdAsync(int id);
        ValueTask<int> GetAllGoogleLimitAsync();
        ValueTask<int> GetGoogleLimitByIdAsync(int id);
    }
}
