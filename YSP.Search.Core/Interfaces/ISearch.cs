using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core.DTO;
using YSP.Search.Core.Classes;
using YSP.Search.Core.Structures;

namespace YSP.Search.Core.Interfaces
{
    public interface ISearch
    {
        //private Stream ResponseStreamGET();
        Task<List<SearchResult>> GetResponseToList(QueryRegionDTO qrDTO, APICredentials APICred, CancellationToken token, RequestMethodEnum requestMethod);
    }
}
