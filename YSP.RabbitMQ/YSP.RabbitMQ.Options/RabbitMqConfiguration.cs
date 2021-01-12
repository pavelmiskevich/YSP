using System;

namespace YSP.RabbitMQ.Options
{
    /// <summary>
    /// Configuration RabbitMq
    /// </summary>
    public class RabbitMqConfiguration
    {
        /// <summary>
        /// Hostname
        /// </summary>
        public string Hostname { get; set; }
        /// <summary>
        /// Name of Queue if necessary
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// Name of Logs Queue if necessary
        /// </summary>
        public string LogsQueueName { get; set; }        
        /// <summary>
        /// Name of Exchange if necessary
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// Type of Exchange
        /// </summary>
        public string ExchangeType { get; set; }
        
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
