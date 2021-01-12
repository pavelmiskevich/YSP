using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<IEnumerable<Position>> GetAllWithQueryAsync();
        Task<Position> GetWithQueryByIdAsync(int id);
        Task<IEnumerable<Position>> GetAllWithQueryByQueryIdAsync(int queryId);
        Task<IEnumerable<Position>> GetAllWithQueryByDateAsync(DateTime date);
        Task<IEnumerable<Position>> GetAllWithQueryBySiteIdAndDateAsync(int siteId, DateTime date);
    }
}
