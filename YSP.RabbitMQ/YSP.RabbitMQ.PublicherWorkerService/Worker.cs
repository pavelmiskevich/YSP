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
using YSP.Core.DTO;
using YSP.Core.Models;
using YSP.Operations;
using YSP.RabbitMQ.Options;
using YSP.RabbitMQ.Options.Exchange;
using YSP.RabbitMQ.Options.Extensions;
using ExchangeType = YSP.RabbitMQ.Options.Exchange.ExchangeType;

namespace YSP.RabbitMQ.PublicherWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly RabbitMqConfiguration _options;

        private readonly CheckScheduleOperation _checkScheduleOperation;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, RabbitMqConfiguration options, CheckScheduleOperation checkScheduleOperation)
        {
            _logger = logger;
             _configuration = configuration;
            _options = options;
            //_scheduleOperations = scheduleOperations;
            _checkScheduleOperation = checkScheduleOperation;
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("Timed Background Service is starting.");
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory();
            _configuration.GetSection("RabbitMqConnection").Bind(connectionFactory);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var conn = connectionFactory.CreateConnection())
                using (var channel = conn.CreateModel())
                {
                    Console.WriteLine("Connected!");

                    //channel.ExchangeDeclare(_options.ExchangeName, _options.ExchangeType, true);
                    channel.ExchangeDeclare(new Exchange(_options.ExchangeName, ExchangeType.Fanout));
                    if (!string.IsNullOrEmpty(_options.QueueName))
                    {
                        //channel.QueueDeclare(_options.QueueName, true, false, false, null);
                        channel.QueueDeclare(new Queue(_options.QueueName));
                        channel.QueueBind(_options.QueueName, _options.ExchangeName, "");
                    }
                    if (!string.IsNullOrEmpty(_options.LogsQueueName))
                    {
                        channel.QueueDeclare(new Queue(_options.LogsQueueName));
                        channel.QueueBind(_options.LogsQueueName, _options.ExchangeName, "");
                    }


                    //QueryRegionDTO qrDTO = new QueryRegionDTO { Id = 1, QueryName = "QueryName", Region = new Region() { Name = "Москва" }, ScheduleId = 3, Url = "ya.ru" };
                    //TODO: публикация расписаний для обработки
                    IEnumerable<QueryRegionDTO> queryRegionDTOs = await _checkScheduleOperation.GetQueries4Check(stoppingToken);
                    foreach (var qrDTO in queryRegionDTOs)
                    {
                        var json = JsonConvert.SerializeObject(qrDTO);
                        var body = Encoding.UTF8.GetBytes(json);

                        //channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                        channel.BasicPublish(exchange: _options.ExchangeName, routingKey: "", basicProperties: null, body: body);
                    }
                    //TODO: публикация раписаний для новых запросов
                }

                await Task.Delay(1000, stoppingToken);
                //await Task.Delay(3_600_000, stoppingToken);
            }
        }
    }
}
