using MassTransit;
using Otus.Teaching.Pcf.Contracts;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration
{
    public class GivingPromoCodeToCustomerRabbitMqGateway : IGivingPromoCodeToCustomerGateway
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public GivingPromoCodeToCustomerRabbitMqGateway(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task GivePromoCodeToCustomer(PromoCode promoCode)
        {
            var contractDto = new GivePromoCodeToCustomerContract()
            {
                PartnerId = promoCode.Partner.Id,
                BeginDate = promoCode.BeginDate.ToShortDateString(),
                EndDate = promoCode.EndDate.ToShortDateString(),
                PreferenceId = promoCode.PreferenceId,
                PromoCode = promoCode.Code,
                ServiceInfo = promoCode.ServiceInfo,
                PartnerManagerId = promoCode.PartnerManagerId
            };

            await _publishEndpoint.Publish(contractDto);
        }
    }
}
