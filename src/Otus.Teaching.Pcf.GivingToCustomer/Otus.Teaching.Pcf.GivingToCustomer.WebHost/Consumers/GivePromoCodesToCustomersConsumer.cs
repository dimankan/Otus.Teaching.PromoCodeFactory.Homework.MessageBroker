using System.Threading.Tasks;
using MassTransit;
using Otus.Teaching.Pcf.Common;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Services;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Consumers
{
	public class GivePromoCodesToCustomersConsumer : IConsumer<GivePromoCodeToCustomerMessage>
    {
        private readonly IPromoCodeService _promoCodeService;
        public GivePromoCodesToCustomersConsumer(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        public async Task Consume(ConsumeContext<GivePromoCodeToCustomerMessage> context)
        {
            await _promoCodeService.GivePromoCodesToCustomersWithPreferenceAsync(context.Message);
        }
    }
}
