using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Otus.Teaching.Pcf.Administration.Integration.RabbitMQ.Abstractions;
using Otus.Teaching.Pcf.Administration.Integration.Messages;
using Otus.Teaching.Pcf.Administration.Integration.RabbitMQ.Configuration;
using Otus.Teaching.Pcf.Administration.Integration.Messages.MessageServices;

namespace Otus.Teaching.Pcf.Administration.Integration.RabbitMQ.Consumers
{
    public class RabbitMqNotificationConsumer<T>
        : BackgroundService, IConsumer
        where T: IRabbitMqMessage
    {
        private readonly RabbitMqConfig _rabbitMqConfiguration;
        
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IRabbitMqMsgService _rabbitMqMsgService;
        private readonly IServiceScope _scope;

        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;

        private readonly string _queueName;

        public RabbitMqNotificationConsumer(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMqConfig> options)
        {
            _rabbitMqConfiguration = options.Value;

            _serviceScopeFactory = serviceScopeFactory;
            _scope = _serviceScopeFactory.CreateScope();
            _rabbitMqMsgService = _scope.ServiceProvider.GetRequiredService<IRabbitMqMsgService>();

            _factory = new ConnectionFactory() { HostName = _rabbitMqConfiguration.Host, UserName = _rabbitMqConfiguration.UserName, Password = _rabbitMqConfiguration.Password };
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();

            _queueName = _rabbitMqConfiguration.QueueName;
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public override void Dispose()
        {
            _scope.Dispose();
            _channel.Close();
            _conn.Close();
            base.Dispose();
        }

        public void SubscribeNotification(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);

                T rabbitMqMessage = JsonConvert.DeserializeObject<T>(message);

                if (rabbitMqMessage != null)
                {
                    Task.Run(async () =>
                    {
                        await _rabbitMqMsgService.ProcessRabbitMQMessage(rabbitMqMessage);
                    });
                }

            };
            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                this.Dispose();
                return Task.CompletedTask;
            }

            SubscribeNotification(_channel);


            return Task.CompletedTask;
        }
    }
}
