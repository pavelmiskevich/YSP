using System;
using System.Collections.Generic;
using System.Text;
using YSP.RabbitMQ.Options.Exchange.Base;

namespace YSP.RabbitMQ.Options.Exchange
{
    public interface IQueue : IBase
    {
        /// <summary>
        /// Exclusive (used by only one connection and the queue will be deleted when that connection closes)
        /// </summary>
        bool IsExclusive { get; }
    }
}
