using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<Feedback>> GetAllWithUserAsync()
        {
            return await YSPDbContext.Feedbacks
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task<Feedback> GetWithUserByIdAsync(int id)
        {
            return await YSPDbContext.Feedbacks
                .Include(i => i.User)
                .SingleOrDefaultAsync(f => f.Id == id); ;
        }

        public async Task<IEnumerable<Feedback>> GetAllWithUserByUserIdAsync(int userId)
        {
            return await YSPDbContext.Feedbacks
                .Include(i => i.User)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }       
    }
}
