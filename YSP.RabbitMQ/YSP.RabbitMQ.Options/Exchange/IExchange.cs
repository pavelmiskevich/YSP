using System;
using System.Collections.Generic;
using System.Text;
using YSP.RabbitMQ.Options.Exchange.Base;

namespace YSP.RabbitMQ.Options.Exchange
{
    public interface IExchange : IBase
    {
        //https://habr.com/ru/post/489086/

        /// <summary>
        /// Type of exchange
        /// </summary>
        string Type { get; }     
    }
}
