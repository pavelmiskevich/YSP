using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class QueryService : IQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Query> CreateQuery(Query newQuery)
        {
            await _unitOfWork.Queries.AddAsync(newQuery);
            //TODO: подумать может переделать на событие            
            await _unitOfWork.CommitAsync();
            //TODO: проверить заполнение newQuery.Id
            await _unitOfWork.Schedules.AddAsync(new Schedule { QueryId = newQuery.Id });
            //TODO: посмотреть как генерировать Id на клиенте
            //https://habr.com/ru/post/349250/
            await _unitOfWork.CommitAsync();
            return newQuery;
        }

        public async Task DeleteQuery(Query query)
        {
            _unitOfWork.Queries.Remove(query);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Query>> GetAllWithSite()
        {
            return await _unitOfWork.Queries
                .GetAllWithSiteAsync();
        }

        public async Task<IEnumerable<Query>> GetQueriesBySiteId(int siteId)
        {
            return await _unitOfWork.Queries
                .GetAllWithSiteBySiteIdAsync(siteId);
        }

        public async Task<Query> GetQueryById(int id)
        {
            return await _unitOfWork.Queries
                .GetByIdAsync(id);
        }

        public async Task UpdateQuery(Query queryToBeUpdated, Query query)
        {
            queryToBeUpdated.Name = query.Name;
            queryToBeUpdated.LastCheck = query.LastCheck;
            queryToBeUpdated.SiteId = query.SiteId;
            queryToBeUpdated.IsActive = query.IsActive;

            await _unitOfWork.CommitAsync();
        }
    }
}
