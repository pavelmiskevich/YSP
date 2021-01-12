using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace YSP.Search.Core.Classes.Static
{
    static class HttpClientHelper
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Stream> HttpClientStreamGETAsync(string completeUrl)
        {
            Stream responsestream = default;
            ServicePointManager.Expect100Continue = false;

            //string completeUrl = $"https://yandex.ru/search/xml?user={_APICredentials.User}&key={_APICredentials.Key}&query={_queryRegionDTO.Name}&lr={_queryRegionDTO.Region.Id}&l10n=ru&sortby=rlv&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D100.docs-in-group%3D1";

            try
            {
                HttpResponseMessage response = await client.GetAsync(completeUrl);
                response.EnsureSuccessStatusCode();                
                responsestream = await response.Content.ReadAsStreamAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("HttpRequestException Message :{0} ", e.Message);
            }
            return responsestream;
        }
    }
}
