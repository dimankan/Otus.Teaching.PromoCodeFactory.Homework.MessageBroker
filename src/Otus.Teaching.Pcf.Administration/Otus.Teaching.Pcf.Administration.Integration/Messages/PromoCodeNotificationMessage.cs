using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.Administration.Integration.Messages
{
    public class PromoCodeNotificationMessage : IRabbitMqMessage
    {
        public Guid? PartnerManagerId { get; set; }
    }
}
