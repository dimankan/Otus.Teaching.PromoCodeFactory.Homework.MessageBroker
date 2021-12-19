using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Integration.Messages.MessageServices
{
    public interface IRabbitMqMsgService
    {
        public Task ProcessRabbitMQMessage(IRabbitMqMessage message);

    }
}
