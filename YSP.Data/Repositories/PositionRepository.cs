using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<Position>> GetAllWithQueryAsync()
        {
            return await YSPDbContext.Positions
                .Include(i => i.Query)
                .ToListAsync();
        }

        public async Task<Position> GetWithQueryByIdAsync(int id)
        {
            return await YSPDbContext.Positions
                .Include(i => i.Query)
                .SingleOrDefaultAsync(p => p.Id == id); ;
        }

        public async Task<IEnumerable<Position>> GetAllWithQueryByQueryIdAsync(int queryId)
        {
            return await YSPDbContext.Positions
                .Include(i => i.Query)
                .Where(p => p.QueryId == queryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetAllWithQueryByDateAsync(DateTime date)
        {
            return await YSPDbContext.Positions
                .Include(i => i.Query)
                .Where(p => p.AddDate.Date == date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetAllWithQueryBySiteIdAndDateAsync(int siteId, DateTime date)
        {
            return await YSPDbContext.Positions
                .Include(i => i.Query)
                .Where(p => p.AddDate.Date == date && p.Query.Site.Id == siteId)
                .ToListAsync();
        }
    }
}
