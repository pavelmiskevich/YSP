using System;
using System.Collections.Generic;
using System.Text;

namespace YSP.RabbitMQ.Options.Exchange.Base
{
    public interface IBase
    {
        /// <summary>
        /// Name of the exchange or queue
        /// The name must be unique
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Durable (the queue will survive a broker restart)
        /// </summary>
        bool IsDurable { get; }

        /// <summary>
        /// Auto-delete (queue that has had at least one consumer is deleted when last consumer unsubscribes)
        /// </summary>
        bool IsAutoDelete { get; }

        /// <summary>
        /// Arguments (optional; used by plugins and broker-specific features such as message TTL, queue length limit, etc)
        /// </summary>
        IDictionary<string, object> Arguments { get; }
    }
}
