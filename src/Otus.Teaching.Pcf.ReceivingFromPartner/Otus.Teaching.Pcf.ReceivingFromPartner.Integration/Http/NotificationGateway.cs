﻿using System;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Http
{
    public class NotificationGateway
        : INotificationGateway
    {
        public Task SendNotificationToPartnerAsync(Guid partnerId, string message)
        {
            //Код, который вызывает сервис отправки уведомлений партнеру

            return Task.CompletedTask;
        }
    }
}