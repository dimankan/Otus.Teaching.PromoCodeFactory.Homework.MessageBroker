using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.RabbitMQ.Configuration;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Messages;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.RabbitMQ.Publishers
{
    public class RabbitMQPublisher<T>
        : IRabbitMqPublisher<T>
        where T : RabbitMqConfig
    {
        private readonly RabbitMqConfig _rabbitMqConfiguration;

        public RabbitMQPublisher(IOptions<T> options)
        {
            _rabbitMqConfiguration = options.Value;
        }

        public void PublishMessage(IRabbitMqMessage message)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = _rabbitMqConfiguration.Host, UserName = _rabbitMqConfiguration.UserName, Password = _rabbitMqConfiguration.Password };
            using (IConnection conn = factory.CreateConnection())
            using (IModel channel = conn.CreateModel())
            {
                string queueName = _rabbitMqConfiguration.QueueName;
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }
    }
}
