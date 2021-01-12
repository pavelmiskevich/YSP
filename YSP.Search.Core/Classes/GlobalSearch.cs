using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core.DTO;
using YSP.Search.Core.Interfaces;
using YSP.Search.Core.Structures;

namespace YSP.Search.Core.Classes
{
    public class GlobalSearch : BaseSearch
    {
        private ISearch _search;        
        
        public GlobalSearch(ISearch search, QueryRegionDTO qrDTO, APICredentials APICred, CancellationToken token, RequestMethodEnum? requestMethod = RequestMethodEnum.GET)
        {
            _search = search;

            _queryRegionDTO = qrDTO;
            _APICredentials = APICred;
            _сancellationToken = token;
            _requestMethod = (RequestMethodEnum)requestMethod;
            //if (_requestMethod == RequestMethodEnum.GET)
            //{
            //    //this._responseStream = await HttpClientStreamGETAsync2();
            //}
            //else if (_requestMethod == RequestMethodEnum.POST)
            //{
            //    //this._responseStream = ResponseStreamPOST();

            //}
        }

        public void SetSearch(ISearch search)
        {
            _search = search;
        }

        public async Task<List<SearchResult>> GetResponseToList()
        {
            return await _search.GetResponseToList(_queryRegionDTO, _APICredentials, _сancellationToken, _requestMethod);
        }
    }
}
