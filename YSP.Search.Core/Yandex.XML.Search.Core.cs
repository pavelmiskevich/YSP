using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using YSP.Core.DTO;

namespace YSP.Search.Core
{
    public enum RequestMethodEnum { POST, GET }

    public struct YaSearchResult
    {
        public int Position;
        //url
        public string DisplayUrl,
        //saved-copy-url
        CacheUrl,
        //title
        Title,
        //headline
        Description,
        //modtime
        IndexedTime,
        Domain;

        public YaSearchResult(int position, string url,
                   string cacheUrl,
                   string title,
                   string description,
                   string indexedTime,
            string domain)
        {
            this.Position = position;
            this.DisplayUrl = url;
            this.CacheUrl = cacheUrl;
            this.Title = title;
            this.Description = description;
            this.IndexedTime = indexedTime;
            this.Domain = domain;
        }
    }

    public class apiCredentials // Пользователь и Логин к API
    {
        public string User { get; set; }
        public string Key { get; set; }

    }

    [Obsolete]
    public class YandexSearchQuery
    {
        static readonly HttpClient client = new HttpClient();

        private apiCredentials _APICredentials { get; set; } // Обьект идентификации пользователь и токен
        private QueryRegionDTO _queryRegionDTO { get; set; } //запрос с регионом
        private Stream _responseStream { get; set; } //Ответ
        private CancellationToken _сancellationToken { get; set; } //токен отмены
        private RequestMethodEnum _requestMethod { get; set; } //Метод запроса
        public YandexSearchQuery(QueryRegionDTO qrDTO, apiCredentials APICred, CancellationToken token, RequestMethodEnum? requestMethod = RequestMethodEnum.GET)
        {
            _queryRegionDTO = qrDTO;
            _APICredentials = APICred;
            _сancellationToken = token;
            _requestMethod = (RequestMethodEnum)requestMethod;
            if (_requestMethod == RequestMethodEnum.GET)
            {
                //this._responseStream = await HttpClientStreamGETAsync2();
            }
            else if (_requestMethod == RequestMethodEnum.POST)
            {
                //this._responseStream = ResponseStreamPOST();

            }
        }

        private async Task<Stream> HttpClientStreamGETAsync()
        {
            Stream responsestream = default;
            ServicePointManager.Expect100Continue = false;
            
            string completeUrl = $"https://yandex.ru/search/xml?user={_APICredentials.User}&key={_APICredentials.Key}&query={_queryRegionDTO.QueryName}lr={_queryRegionDTO.Region.Id}&l10n=ru&sortby=tm.order%3Dascending&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D100.docs-in-group%3D1";            
            
            try
            {
                HttpResponseMessage response = await client.GetAsync(completeUrl);
                //Получение ответа.
                response.EnsureSuccessStatusCode();
                //TODO: тут можно сетить this._responseStream напрямую и ничего не возвращать!!!
                responsestream = await response.Content.ReadAsStreamAsync();
                //responsestream = await client.GetStreamAsync(completeUrl);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("HttpRequestException Message :{0} ", e.Message);
            }
            return responsestream;
        }

        public async Task<List<YaSearchResult>> GetResponseToList()
        {
            //Лист структур YaSearchResult, который метод в итоге возвращает.
            List<YaSearchResult> ret = new List<YaSearchResult>();

            //XmlReader xmlReader = XmlReader.Create(this._responseStream);
            //XDocument response = await XDocument.LoadAsync(this._responseStream, LoadOptions.None, _сancellationToken);
            XDocument response = await XDocument.LoadAsync(await HttpClientStreamGETAsync(), LoadOptions.None, _сancellationToken);
            //из полученного XML'я выдираем все элементы с именем "group" - это результаты поиска

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
                string urlQuery = GetValue(groupQuery.ElementAt(i), "url");
                string titleQuery = GetValue(groupQuery.ElementAt(i), "title");
                string descriptionQuery = GetValue(groupQuery.ElementAt(i), "headline");
                string indexedTimeQuery = GetValue(groupQuery.ElementAt(i), "modtime");
                string cacheUrlQuery = GetValue(groupQuery.ElementAt(i),
                                "saved-copy-url");
                string domain = GetValue(groupQuery.ElementAt(i),
                                "domain");
                ret.Add(new YaSearchResult(position, urlQuery, cacheUrlQuery, titleQuery, descriptionQuery, indexedTimeQuery, domain));
                //ret.Add(new YaSearchResult(position, urlQuery, titleQuery, descriptionQuery, indexedTimeQuery, domain));
            }

            return ret;
        }

        public static string GetValue(XElement group, string name)
        {
            return group.Element("doc").Element(name) == null ? string.Empty : group.Element("doc").Element(name).Value;
            //try
            //{
            //    return group.Element("doc").Element(name).Value;
            //}
            ////это если в результате нету элемента с каким то именем,
            ////то будет вместо значащей строчки возвращаться пустая.
            //catch
            //{
            //    return string.Empty;
            //}
        }
    }
}
