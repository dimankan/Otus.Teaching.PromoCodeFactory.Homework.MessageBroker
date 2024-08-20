using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;
using Otus.Teaching.Pcf.Administration.Core.Domain;
using Otus.Teaching.Pcf.Administration.IntegrationHostedService.Abstractions;

namespace Otus.Teaching.Pcf.Administration.IntegrationHostedService
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
            if (!message.PartnerManagerId.HasValue || message.PartnerManagerId == Guid.Empty)
                return;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var employeesService = scopedServices.GetRequiredService<IEmployeesService>();

                await employeesService.UpdateAppliedPromocodesAsync(message.PartnerManagerId.Value);
            }
        }
    }
}
