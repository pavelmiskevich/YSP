using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllWithQuery();
        Task<Position> GetPositionById(int id);
        Task<IEnumerable<Position>> GetPositionsByQueryId(int queryId);
        Task<Position> CreatePosition(Position newPosition);
        Task<IEnumerable<Position>> CreateRangePosition(IEnumerable<Position> newPositions);
        Task UpdatePosition(Position positionToBeUpdated, Position position);
        Task DeletePosition(Position position);
        Task<IEnumerable<Position>> GetPositionsToday();
        Task<IEnumerable<Position>> GetPositionsByDate(DateTime date);
        Task<IEnumerable<Position>> GetPositionsBySiteIdToday(int siteId);
        Task<IEnumerable<Position>> GetPositionsBySiteIdAndDate(int siteId, DateTime date);
    }
}
