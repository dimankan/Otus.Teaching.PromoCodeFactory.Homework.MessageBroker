using RabbitMQ.Client;

namespace Otus.Teaching.Pcf.GivingToCustomer.Integration.RabbitMQ.Abstractions
{
    public interface IConsumer
    {
        public void SubscribeNotification(IModel channel);
    }
}
