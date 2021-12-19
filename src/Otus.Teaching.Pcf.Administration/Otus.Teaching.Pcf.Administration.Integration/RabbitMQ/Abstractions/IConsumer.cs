using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.Administration.Integration.RabbitMQ.Abstractions
{
    public interface IConsumer
    {
        public void SubscribeNotification(IModel channel);
    }
}
