using MassTransit;
using Otus.Teaching.Pcf.Contracts;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.Core.UseCases;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.RabbitMqConsumers
{
    public class GivePromoCodeToCustomerConsumer : IConsumer<GivePromoCodeToCustomerContract>
    {
        private readonly IPromocodesManager _promocodesManager;

        public GivePromoCodeToCustomerConsumer(IPromocodesManager promocodesManager)
        {
            _promocodesManager = promocodesManager;
        }

        public async Task Consume(ConsumeContext<GivePromoCodeToCustomerContract> context)
        {
            var request = context.Message;

            var promoCode = new PromoCode
            {
                Id = request.PromoCodeId,
                PartnerId = request.PartnerId,
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Parse(request.BeginDate),
                EndDate = DateTime.Parse(request.EndDate)
            };

            await _promocodesManager.GivePromoCodesToCustomersWithPreferenceAsync(request.PreferenceId, promoCode);
        }
    }
}
