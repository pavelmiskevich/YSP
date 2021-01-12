using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using YSP.Core.DTO;
using YSP.Search.Core.Classes.Static;
using YSP.Search.Core.Interfaces;
using YSP.Search.Core.Structures;

namespace YSP.Search.Core.Classes
{
    public class GoogleSearch : ISearch
    {
        //https://www.googleapis.com/customsearch/v1?key=INSERT_YOUR_API_KEY&cx=017576662512468239146:omuauf_lfve&q=lectures
        //https://www.googleapis.com/customsearch/v1?key=AIzaSyBVhC2rYmP6KUD7_kk3GigU_Q7XdMv1gZQ&cx=5a46945b96106c7b3&q=вечеринки
        //https://developers.google.com/api-client-library/dotnet/apis/customsearch/v1
        //https://developers.google.com/custom-search/v1/reference/rest/v1/Search?hl=ru

        public async Task<List<SearchResult>> GetResponseToList(QueryRegionDTO qrDTO, APICredentials apiCred, CancellationToken token, RequestMethodEnum requestMethod)
        {
            int position = 0;
            List<SearchResult> ret = new List<SearchResult>();

            //XDocument response = await XDocument.LoadAsync(await HttpClientStreamGETAsync(), LoadOptions.None, _сancellationToken);
            //TODO: доделать сериализациюс JSON для поиска Google
            //https://habr.com/ru/company/otus/blog/515880/
            //https://docs.microsoft.com/ru-ru/dotnet/standard/serialization/system-text-json-how-to
            var retSteam = await HttpClientHelper.HttpClientStreamGETAsync($"https://www.googleapis.com/customsearch/v1?key={apiCred.Key}&cx={apiCred.User}&" +
                $"q={qrDTO.QueryName}");

            var serializer = new Newtonsoft.Json.JsonSerializer();            

            using (var sr = new StreamReader(retSteam))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var json =  serializer.Deserialize(jsonTextReader);
                //https://dotnetcoretutorials.com/2019/09/11/how-to-parse-json-in-net-core/
                //var test = JsonConvert.DeserializeObject<GoogleSearchResult>(json.ToString());

                //https://docs.microsoft.com/ru-ru/dotnet/standard/serialization/system-text-json-how-to
                using (JsonDocument document = JsonDocument.Parse(json.ToString()))
                {
                    JsonElement root = document.RootElement;
                    JsonElement items = root.GetProperty("items");
                    foreach (JsonElement item in items.EnumerateArray())
                    {
                        //if (item.TryGetProperty("Grade", out JsonElement gradeElement))

                        int pos = ++position;
                        string kind = item.GetProperty("kind").ToString();
                        string title = item.GetProperty("title").ToString();
                        string htmlTitle = item.GetProperty("htmlTitle").ToString();
                        string link = item.GetProperty("link").ToString();
                        string displayLink = item.GetProperty("displayLink").ToString();
                        string snippet = item.GetProperty("snippet").ToString();
                        string htmlSnippet = item.GetProperty("htmlSnippet").ToString();
                        string cacheId = item.GetProperty("cacheId").ToString();
                        string formattedUrl = item.GetProperty("formattedUrl").ToString();
                        string htmlFormattedUrl = item.GetProperty("htmlFormattedUrl").ToString();
                        ret.Add(new SearchResult { Title = title, DisplayUrl = link, Position = position});
                        //ret.Add(new GoogleSearchResult(title, link, position));
                    }
                }

            }

            return ret;
            //return new List<ISearchResult>(ret.Cast<ISearchResult>());
        }
    }
}