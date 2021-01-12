using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<User>> GetAllWithFeedbacksAsync()
        {
            return await YSPDbContext.Users
                .Include(i => i.Feedbacks)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithOffersAsync()
        {
            return await YSPDbContext.Users
                .Include(i => i.Offers)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<User>> GetAllWithSitesAsync()
        {
            return await YSPDbContext.Users
                .Include(i => i.Sites)
                .ToListAsync();
        }

        //TODO: добавил asnync, ощущение, что пропущено в источнике
        public async Task<User> GetWithFeedbacksByIdAsync(int id)
        {
            return await YSPDbContext.Users
                .Include(i => i.Feedbacks)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        //TODO: добавил asnync, ощущение, что пропущено в источнике
        public async Task<User> GetWithOffersByIdAsync(int id)
        {
            return await YSPDbContext.Users
                .Include(i => i.Offers)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        //TODO: добавил asnync, ощущение, что пропущено в источнике
        public async Task<User> GetWithSitesByIdAsync(int id)
        {
            return await YSPDbContext.Users
                .Include(i => i.Sites)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async ValueTask<int> GetAllYandexLimitAsync()
        {
            return await YSPDbContext.Users
                .SumAsync(x => x.YandexLimit);
        }        

        public async ValueTask<int> GetYandexLimitByIdAsync(int id)
        {
            return await YSPDbContext.Users
                .Where(u => u.Id == id)
                .Select(u => u.YandexLimit)
                .FirstOrDefaultAsync();
        }

        public async ValueTask<int> GetAllGoogleLimitAsync()
        {
            return await YSPDbContext.Users
                .SumAsync(x => x.GoogleLimit);
        }

        public async ValueTask<int> GetGoogleLimitByIdAsync(int id)
        {
            return await YSPDbContext.Users
                .Where(u => u.Id == id)
                .Select(u => u.GoogleLimit)
                .FirstOrDefaultAsync();
        }
    }
}
