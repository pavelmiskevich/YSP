using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Service for position manadge
        /// </summary>
        /// <param name="unitOfWork">Pattenr entity Unit Of Work</param>
        public PositionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        public async Task<Position> CreatePosition(Position newPosition)
        {
            await _unitOfWork.Positions.AddAsync(newPosition);
            await _unitOfWork.CommitAsync();
            return newPosition;
        }
        /// <summary>
        /// Add range positions
        /// </summary>
        /// <param name="newPositions">IEnumerable of positions</param>
        /// <returns>new added range positions</returns>
        public async Task<IEnumerable<Position>> CreateRangePosition(IEnumerable<Position> newPositions)
        {
            await _unitOfWork.Positions.AddRangeAsync(newPositions);
            await _unitOfWork.CommitAsync();
            return newPositions;
        }
        /// <summary>
        /// Delete position
        /// </summary>
        /// <param name="position">Existing position</param>
        /// <returns></returns>
        public async Task DeletePosition(Position position)
        {
            _unitOfWork.Positions.Remove(position);
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// Get all positions with query
        /// </summary>
        /// <returns>IEnumerable positions</returns>
        public async Task<IEnumerable<Position>> GetAllWithQuery()
        {
            return await _unitOfWork.Positions
                .GetAllWithQueryAsync();
        }
        /// <summary>
        /// Get positions by id
        /// </summary>
        /// <param name="id">position id</param>
        /// <returns>Existing position</returns>
        public async Task<Position> GetPositionById(int id)
        {
            return await _unitOfWork.Positions
                .GetByIdAsync(id);
        }
        /// <summary>
        /// Get all positions by query id
        /// </summary>
        /// <param name="queryId">query id</param>
        /// <returns>IEnumerable positions</returns>
        public async Task<IEnumerable<Position>> GetPositionsByQueryId(int queryId)
        {
            //TODO: подумать, может быть стоит ограничить выборку несколькими месяцами
            return await _unitOfWork.Positions
                .GetAllWithQueryByQueryIdAsync(queryId);
        }
        /// <summary>
        /// Update position
        /// </summary>
        /// <param name="positionToBeUpdated">position to be update</param>
        /// <param name="position">new position</param>
        /// <returns>no data</returns>
        public async Task UpdatePosition(Position positionToBeUpdated, Position position)
        {
            positionToBeUpdated.Pos = position.Pos;
            positionToBeUpdated.QueryId = position.QueryId;
            positionToBeUpdated.IsActive = position.IsActive;

            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// Get all positions on today
        /// </summary>
        /// <returns>IEnumerable positions</returns>
        public async Task<IEnumerable<Position>> GetPositionsToday()
        {
            return await GetPositionsByDate(DateTime.Now.Date);
        }
        /// <summary>
        /// Get all positions by date
        /// </summary>
        /// <param name="date">positions date</param>
        /// <returns>IEnumerable positions</returns>
        public async Task<IEnumerable<Position>> GetPositionsByDate(DateTime date)
        {
            return await _unitOfWork.Positions
                .GetAllWithQueryByDateAsync(date);
        }
        /// <summary>
        /// Get all positions by site id on today
        /// </summary>
        /// <param name="siteId">site id</param>
        /// <returns>IEnumerable positions</returns>
        public async Task<IEnumerable<Position>> GetPositionsBySiteIdToday(int siteId)
        {
            return await GetPositionsBySiteIdAndDate(siteId, DateTime.Now.Date);
        }
        /// <summary>
        /// Get all positions by site id  and date
        /// </summary>
        /// <param name="siteId">site id</param>
        /// <param name="date">positions date</param>
        /// <returns>IEnumerable positions</returns>
        public async Task<IEnumerable<Position>> GetPositionsBySiteIdAndDate(int siteId, DateTime date)
        {
            return await _unitOfWork.Positions
                .GetAllWithQueryBySiteIdAndDateAsync(siteId, date);
        }
    }
}
