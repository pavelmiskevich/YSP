using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using YSP.Core.DTO;

namespace YSP.Search.Core.Classes
{
    public abstract class BaseSearch
    {
        protected APICredentials _APICredentials { get; set; } // Обьект идентификации пользователь и токен
        protected QueryRegionDTO _queryRegionDTO { get; set; } //запрос с регионом
        protected Stream _responseStream { get; set; } //Ответ
        protected CancellationToken _сancellationToken { get; set; } //токен отмены
        protected RequestMethodEnum _requestMethod { get; set; } //Метод запроса
    }
}
