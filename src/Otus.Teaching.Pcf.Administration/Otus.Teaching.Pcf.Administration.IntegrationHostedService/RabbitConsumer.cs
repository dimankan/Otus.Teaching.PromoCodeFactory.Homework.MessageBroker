using Otus.Teaching.Pcf.Administration.Core.Exceptions;
using Otus.Teaching.Pcf.Administration.IntegrationHostedService.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Otus.Teaching.Pcf.Administration.IntegrationHostedService
{
    public class RabbitConsumer
    {
        private readonly AmqpSettings _settings;

        public RabbitConsumer(AmqpSettings settings)
        {
            _settings = settings;
        }

        public void Consume<T>(string exchangeName, string queueName, string routingKey, IMessageHandler<T> handler)
        {
            var connection = GetRabbitConnection();
            var channel = connection.CreateModel();

            channel.BasicQos(0, 10, false);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body;

                var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body.ToArray()))
                    ?? throw new MessageNotRecievedException<T>();

                await handler.HandleAsync(message);

                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume(queueName, false, consumer);
        }

        public void DirectConsume<T>(string routingKey, IMessageHandler<T> handler, string consumerName)
            => Consume<T>($"exchange.direct.{typeof(T).Name}", $"queue.direct.{typeof(T).Name}.{consumerName}", routingKey, handler);

        public void FanoutConsume<T>(IMessageHandler<T> handler, string consumerName)
            => Consume<T>($"exchange.fanout.{typeof(T).Name}", $"queue.fanout.{typeof(T).Name}.{consumerName}", string.Empty, handler);

        private IConnection GetRabbitConnection()
            => new ConnectionFactory
            {
                HostName = _settings.Host,
                VirtualHost = _settings.VHost,
                UserName = _settings.Login,
                Password = _settings.Password
            }.CreateConnection();
    }
}
