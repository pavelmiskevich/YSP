using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<Offer>> GetAllWithUserAsync()
        {
            return await YSPDbContext.Offers
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task<Offer> GetWithUserByIdAsync(int id)
        {
            return await YSPDbContext.Offers
                .Include(i => i.User)
                .SingleOrDefaultAsync(o => o.Id == id); ;
        }

        public async Task<IEnumerable<Offer>> GetAllWithUserByUserIdAsync(int userId)
        {
            return await YSPDbContext.Offers
                .Include(i => i.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }       
    }
}
