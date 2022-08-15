using MassTransit;
using Otus.Teaching.Pcf.Administration.Core.Logic;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Dto;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Infrastructure
{
    public class UpdateAppliedPromocodesConsumer : IConsumer<PartnerManagerDto>
    {
        private readonly IUpdateAppliedPromocodeService _service;

        public UpdateAppliedPromocodesConsumer(IUpdateAppliedPromocodeService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<PartnerManagerDto> context)
        {
            await _service.UpdateAppliedPromocodesAsync(context.Message.Id);

        }
    }
}
