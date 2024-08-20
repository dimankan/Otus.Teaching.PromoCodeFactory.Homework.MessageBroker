using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.Abstractions;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService
{
    public class SendPromoCodeMessageHandler : IMessageHandler<SendPromoCodeMessage>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SendPromoCodeMessageHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleAsync(SendPromoCodeMessage message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var promoCodesService = scopedServices.GetRequiredService<IPromoCodesService>();

                await promoCodesService.GivePromoCodesToCustomersWithPreferenceAsync(message.PromoCode);
            }
        }
    }
}
