using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using YSP.Core;
using YSP.Core.DTO;
using YSP.Core.Services;
using YSP.Operations;
using YSP.RabbitMQ.Options;
using YSP.RabbitMQ.Options.Exchange;
using YSP.RabbitMQ.Options.Extensions;

namespace YSP.RabbitMQ.SubscriberWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly RabbitMqConfiguration _options;

        private readonly ScheduleOperations _scheduleOperations;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, RabbitMqConfiguration options, ScheduleOperations scheduleOperations)
        {
            _logger = logger;
            _configuration = configuration;
            _options = options;
            _scheduleOperations = scheduleOperations;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory();
            _configuration.GetSection("RabbitMqConnection").Bind(connectionFactory);

            //https://www.rabbitmq.com/dotnet-api-guide.html
            var conn = connectionFactory.CreateConnection();
            connectionFactory.AutomaticRecoveryEnabled = true;
            connectionFactory.DispatchConsumersAsync = true;
            var channel = conn.CreateModel();
            //channel.QueueDeclare(_options.QueueName, true, false, false, null);
            channel.QueueDeclare(new Queue(_options.QueueName));
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...

                //ch.BasicAck(ea.DeliveryTag, false);

                var result = await SendRequestAsync(ea, stoppingToken);
                if (result)
                    channel.BasicAck(ea.DeliveryTag, true);

                //channel.BasicAck(ea.DeliveryTag, false);

                await Task.Yield();

            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            string tag = channel.BasicConsume(_options.QueueName, false, consumer);
            // ensure we get a delivery
            //bool waitRes = latch.WaitOne(2000);

            //using (var conn = connectionFactory.CreateConnection())
            //using (var channel = conn.CreateModel())
            //{
            //    Console.WriteLine("Connected!");

            //    #region for Publisher
            //    //channel.ExchangeDeclare(_options.ExchangeName, _options.ExchangeType);
            //    //if (!string.IsNullOrEmpty(_options.QueueName))
            //    //{
            //    //    channel.QueueDeclare(_options.QueueName, true, false, false, null);
            //    //    channel.QueueBind(_options.QueueName, _options.ExchangeName, "");
            //    //}                    
            //    //if(!string.IsNullOrEmpty(_options.LogsQueueName))
            //    //{
            //    //    channel.QueueDeclare(_options.LogsQueueName, true, false, false, null);
            //    //    channel.QueueBind(_options.LogsQueueName, _options.ExchangeName, "");
            //    //}
            //    #endregion for Publisher

            //    channel.QueueDeclare(_options.QueueName, true, false, false, null);
            //    var consumer = new EventingBasicConsumer(channel);                
            //    consumer.Received += async (s, a) =>
            //    {
            //        var result = await SendRequestAsync(a, stoppingToken);
            //        if (!result)
            //            channel.BasicAck(a.DeliveryTag, false);
            //        //Thread.Sleep(5000);
            //        Console.WriteLine($"Subscribe!");
            //        //Console.WriteLine($"Message received! {Encoding.UTF8.GetString(a.Body.ToArray())}");
            //    };
            //    channel.BasicConsume(_options.QueueName, false, consumer);
            //    //TODO: сделать подтверждение успешного выполнения команды
            //    //https://stackoverflow.com/questions/32602744/rabbitmq-basicack-makes-next-message-unack
            //}

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        private async ValueTask<bool> SendRequestAsync(BasicDeliverEventArgs a, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(a.Body.ToArray());
            //TODO: подумать над асинхронной десериализацией
            //https://docs.microsoft.com/ru-ru/dotnet/standard/serialization/system-text-json-how-to
            var qrDTO = JsonConvert.DeserializeObject<QueryRegionDTO>(body);

            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Message received! {qrDTO.QueryName} {qrDTO.Region.Name} {qrDTO.Url}");

            return await _scheduleOperations.CheckSingle(qrDTO, token);

            //return await Task.Run(() =>
            //{
            //    //Thread.Sleep(5000);                
            //    //Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Message received! {Encoding.UTF8.GetString(a.Body.ToArray())}");
            //    //return true;
            //});
        }
    }
}
