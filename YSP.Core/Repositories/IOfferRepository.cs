using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Task<IEnumerable<Offer>> GetAllWithUserAsync();
        Task<Offer> GetWithUserByIdAsync(int id);
        Task<IEnumerable<Offer>> GetAllWithUserByUserIdAsync(int userId);
    }
}
