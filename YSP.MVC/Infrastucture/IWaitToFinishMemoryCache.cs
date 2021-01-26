using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSP.Api.Infrastucture
{
    public interface IWaitToFinishMemoryCache<TItem>
    {
        Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem);
    }
}
