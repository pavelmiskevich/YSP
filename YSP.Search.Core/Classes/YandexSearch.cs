using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using YSP.Core.DTO;
using YSP.Search.Core.Classes.Static;
using YSP.Search.Core.Interfaces;
using YSP.Search.Core.Structures;

namespace YSP.Search.Core.Classes
{
    public class YandexSearch : ISearch
    {
        //static readonly HttpClient client = new HttpClient();


        [Obsolete]
        //private async Task<Stream> HttpClientStreamGETAsync()
        //{
        //    Stream responsestream = default;
        //    ServicePointManager.Expect100Continue = false;
            
        //    string completeUrl = $"https://yandex.ru/search/xml?user={_APICredentials.User}&key={_APICredentials.Key}&query={_queryRegionDTO.Name}&lr={_queryRegionDTO.Region.Id}&l10n=ru&sortby=rlv&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D100.docs-in-group%3D1";

        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync(completeUrl);
        //        //Получение ответа.
        //        response.EnsureSuccessStatusCode();
        //        responsestream = await response.Content.ReadAsStreamAsync();
        //        //responsestream = await client.GetStreamAsync(completeUrl);
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Console.WriteLine("\nException Caught!");
        //        Console.WriteLine("HttpRequestException Message :{0} ", e.Message);
        //    }
        //    return responsestream;
        //}

        public async Task<List<SearchResult>> GetResponseToList(QueryRegionDTO qrDTO, APICredentials apiCred, CancellationToken token, RequestMethodEnum requestMethod)
        {
            List<SearchResult> ret = new List<SearchResult>();

            //XDocument response = await XDocument.LoadAsync(await HttpClientStreamGETAsync(), LoadOptions.None, _сancellationToken);
            Stream retSteam = await HttpClientHelper.HttpClientStreamGETAsync($"https://yandex.ru/search/xml?user={apiCred.User}&key={apiCred.Key}&" +
                $"query={qrDTO.QueryName}&lr={qrDTO.Region.Id}&l10n=ru&sortby=rlv&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D100.docs-in-group%3D1");
            XDocument response = await XDocument.LoadAsync(retSteam, LoadOptions.None, token);

            //TODO: доделать получение ошибки превышения лимита
            //var groupError = from gr in response.Elements().
            //              Elements("response").
            //              Elements("error")
            //                 select gr;
            //if(groupError.ElementAt(0))

            var groupQuery = from gr in response.Elements().
                          Elements("response").
                          Elements("results").
                          Elements("grouping").
                          Elements("group")
                             select gr;

            //каждый элемент group преобразовывается в объект SearchResult
            for (int i = 0; i < groupQuery.Count(); i++)
            {
                int position = i + 1;
                string urlQuery = XMLHelper.GetValue(groupQuery.ElementAt(i), "url");
                string titleQuery = XMLHelper.GetValue(groupQuery.ElementAt(i), "title");
                string descriptionQuery = XMLHelper.GetValue(groupQuery.ElementAt(i), "headline");
                string indexedTimeQuery = XMLHelper.GetValue(groupQuery.ElementAt(i), "modtime");
                string cacheUrlQuery = XMLHelper.GetValue(groupQuery.ElementAt(i),
                                "saved-copy-url");
                string domain = XMLHelper.GetValue(groupQuery.ElementAt(i),
                                "domain");
                ret.Add(new SearchResult(position, urlQuery, cacheUrlQuery, titleQuery, descriptionQuery, indexedTimeQuery, domain));
                //ret.Add(new YaSearchResult(position, urlQuery, titleQuery, descriptionQuery, indexedTimeQuery, domain));
            }

            return ret;
            //return new List<ISearchResult>(ret.Cast<ISearchResult>());
        }
    }
}
