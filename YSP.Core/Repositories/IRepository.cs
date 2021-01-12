using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace YSP.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region async
        ValueTask<TEntity> GetByIdAsync(int? id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion async
        #region sync
        ////TODO: проверить перевод IList и List<TEntity> yf IEnumerable<TEntity>
        //int Add(TEntity entity);
        //int Add(IList<TEntity> entities);
        //int Update(TEntity entity);
        //int Update(IList<TEntity> entities);
        //int Delete(int id, byte[] timestamp);
        //int Delete(TEntity entity);
        //TEntity GetOne(int? id);
        //List<TEntity> GetSome(Expression<Func<TEntity, bool>> where);
        //List<TEntity> GetAll();
        //List<TEntity> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending);
        //List<TEntity> ExecuteQuery(string sql);
        //List<TEntity> ExecuteQuery(string sql, object[] sqlParametersObjects);
        #endregion sync
    }
}
