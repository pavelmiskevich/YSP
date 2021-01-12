using System;
using System.Collections.Generic;
using System.Text;

namespace YSP.RabbitMQ.Options.Exchange
{
    /// <summary>
    /// Exchange
    /// </summary>
    public class Exchange : IExchange
    {
        public Exchange(
               string name,
               string type = ExchangeType.Direct,
               bool durable = true,
               bool autoDelete = false,
               IDictionary<string, object> arguments = null)
        {
            Name = name ??
                throw new ArgumentNullException(name, $"{name} must not be null");

            Type = type;
            IsDurable = durable;
            IsAutoDelete = autoDelete;
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public string Name { get; }
        public string Type { get; }
        public bool IsDurable { get; }
        public bool IsAutoDelete { get; }
        public IDictionary<string, object> Arguments { get; }

        /// <summary>
        /// Returns Exchange by default.
        /// Exchange by default is direct exchange without a name.
        /// If the default exchanger is used, the message
        /// will be routed to the queue
        /// with a name equal to the message routing key.
        /// </summary>
        /// <returns></returns>
        public static IExchange GetDefault()
        {
            return new Exchange("");
        }
    }
}
