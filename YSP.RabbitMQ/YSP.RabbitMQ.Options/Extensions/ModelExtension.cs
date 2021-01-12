using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using YSP.RabbitMQ.Options.Exchange;

namespace YSP.RabbitMQ.Options.Extensions
{
    public static class Extension
    {
        /// <summary>
        /// Extension method QueueDeclare 4 Rabbit MQ
        /// </summary>
        /// <param name="myInterface">Extension Interface</param>
        /// <param name="i">Interface Exchange</param>
        public static void QueueDeclare(this IModel exInterface, IQueue i)
        {
            exInterface.QueueDeclare(queue: i.Name, durable: i.IsDurable, exclusive: i.IsExclusive, autoDelete: i.IsAutoDelete, arguments: i.Arguments);
        }

        /// <summary>
        /// Extension method ExchangeDeclare 4 Rabbit MQ
        /// </summary>
        /// <param name="myInterface">Extension Interface</param>
        /// <param name="i">Interface Exchange</param>
        public static void ExchangeDeclare(this IModel exInterface, IExchange i)
        {
            exInterface.ExchangeDeclare(exchange: i.Name, type: i.Type, durable: i.IsDurable, autoDelete: i.IsAutoDelete, arguments: i.Arguments);
        }
    }
}
