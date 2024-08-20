using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Amqp
{
    public class AmqpPromoCodeSenderGateway : IPromoCodeSenderGateway
    {
        private readonly RabbitProducer _rabbitProducer;

        public AmqpPromoCodeSenderGateway(RabbitProducer rabbitProducer)
        {
            _rabbitProducer = rabbitProducer;
        }

        public Task SendPromoCode(PromoCode promoCode, Guid? partnerManagerId)
        {
            var message = new SendPromoCodeMessage
            { 
                PromoCode = promoCode,
                PartnerManagerId = partnerManagerId 
            };

            _rabbitProducer.FanoutProduce(message);

            return Task.CompletedTask;
        }
    }
}
