using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.Administration.Integration.RabbitMQ.Configuration
{
    public class RabbitMqConfig
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string QueueName { get; set; }

    }
}
