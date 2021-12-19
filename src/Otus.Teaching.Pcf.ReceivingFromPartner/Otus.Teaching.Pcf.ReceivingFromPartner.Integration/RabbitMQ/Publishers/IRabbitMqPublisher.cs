using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.RabbitMQ.Publishers
{
    public interface IRabbitMqPublisher<T>
    {
        public void PublishMessage(IRabbitMqMessage message);
    }
}
