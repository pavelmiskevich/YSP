using System;
using System.Collections.Generic;
using System.Text;

namespace YSP.RabbitMQ.Options.Exchange
{
    /// <summary>
    /// Type of exchange
    /// </summary>
    public static class ExchangeType
    {
        /// <summary>
        /// The Direct exchange type routes messages with a routing key equal to the routing key declared by the binding queue
        /// </summary>
        public const string Direct = "direct";
        /// <summary>
        /// The Topic exchange type routes messages to queues whose routing key matches all, or a portion of a routing key
        /// </summary>
        public const string Topic = "topic";
        /// <summary>
        /// The Fanout exchange type routes messages to all bound queues indiscriminately.  If a routing key is provided, it will simply be ignored
        /// </summary>
        public const string Fanout = "fanout";
        /// <summary>
        /// The Headers exchange type routes messages based upon a matching of message headers to the expected headers specified by the binding queue
        /// </summary>
        public const string Header = "headers";
    }
}
