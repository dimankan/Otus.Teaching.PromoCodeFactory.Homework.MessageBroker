using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Settings;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Amqp
{
    public class RabbitProducer
    {
        private readonly AmqpSettings _settings;

        public RabbitProducer(AmqpSettings settings)
        {
            _settings = settings;
        }

        public void Produce<T>(T content, string exchangeType, string exchangeName, string routingKey)
        {
            using var connection = GetRabbitConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, exchangeType);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(content));

            channel.BasicPublish(exchangeName, routingKey, null, body);
        }

        public void DirectProduce<T>(T content, string routingKey)
            => Produce(content, "direct", $"exchange.direct.{typeof(T).Name}", routingKey);

        public void FanoutProduce<T>(T content)
            => Produce(content, "fanout", $"exchange.fanout.{typeof(T).Name}", string.Empty);

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
