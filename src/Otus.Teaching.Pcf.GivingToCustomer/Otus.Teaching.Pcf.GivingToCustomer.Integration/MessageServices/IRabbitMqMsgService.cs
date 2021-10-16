using Otus.Teaching.Pcf.GivingToCustomer.Integration.Messages;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Integration.EntityServices.MessageServices
{
    public interface IRabbitMqMsgService
    {
        public Task ProcessRabbitMQMessage(IRabbitMQMessage message);

    }
}
