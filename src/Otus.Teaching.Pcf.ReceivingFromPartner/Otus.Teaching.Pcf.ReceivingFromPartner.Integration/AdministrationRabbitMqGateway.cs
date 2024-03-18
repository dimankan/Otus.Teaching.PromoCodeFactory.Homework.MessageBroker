using MassTransit;
using Otus.Teaching.Pcf.Contracts;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration
{
    public class AdministrationRabbitMqGateway : IAdministrationGateway
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public AdministrationRabbitMqGateway(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task NotifyAdminAboutPartnerManagerPromoCode(Guid partnerManagerId)
        {
            await _publishEndpoint.Publish(new NotifyAdminAboutPartnerManagerPromoCodeContract 
            { 
                PartnerManagerId = partnerManagerId
            });
        }
    }
}
