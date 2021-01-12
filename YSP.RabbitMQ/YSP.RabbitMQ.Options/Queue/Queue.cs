using System;
using System.Collections.Generic;
using System.Text;

namespace YSP.RabbitMQ.Options.Exchange
{
    /// <summary>
    /// Queue
    /// </summary>
    public class Queue : IQueue
    {
        public Queue(
               string name,
               bool durable = true,
               bool exclusive = false,
               bool autoDelete = false,
               IDictionary<string, object> arguments = null)
        {
            Name = name ??
                throw new ArgumentNullException(name, $"{name} must not be null");

            IsDurable = durable;
            IsExclusive = exclusive;
            IsAutoDelete = autoDelete;
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public string Name { get; }
        public bool IsDurable { get; }
        public bool IsExclusive { get; }
        public bool IsAutoDelete { get; }
        public IDictionary<string, object> Arguments { get; }        

        /// <summary>
        /// Returns Exchange by default.
        /// </summary>        
        public static IQueue GetDefault()
        {
            return new Queue("");
        }
    }
}
