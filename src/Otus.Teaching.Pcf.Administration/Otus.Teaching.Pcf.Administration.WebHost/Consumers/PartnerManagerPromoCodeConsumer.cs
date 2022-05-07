using MassTransit;
using Otus.Teaching.Pcf.Administration.WebHost.Services;
using Otus.Teaching.Pcf.Common;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.Consumers
{
    public class PartnerManagerPromoCodeConsumer : IConsumer<PartnerManagerIdMessage>
    {
        private readonly IPartnerPromoCodeService _partnerPromoCodeService;
        public PartnerManagerPromoCodeConsumer(IPartnerPromoCodeService partnerPromoCodeService)
        {
            _partnerPromoCodeService = partnerPromoCodeService;
        }

        public async Task Consume(ConsumeContext<PartnerManagerIdMessage> context)
        {
            await _partnerPromoCodeService.UpdateAppliedPromocodesAsync(context.Message);
        }
    }

}
