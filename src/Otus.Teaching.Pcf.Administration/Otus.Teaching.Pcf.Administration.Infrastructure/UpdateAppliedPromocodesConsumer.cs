using MassTransit;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Dto;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Infrastructure
{
    public class UpdateAppliedPromocodesConsumer : IConsumer<PartnerManagerDto>
    {
        public Task Consume(ConsumeContext<PartnerManagerDto> context)
        {
            Console.Write(context.Message.Id);

            return Task.CompletedTask;
        }
    }
}
