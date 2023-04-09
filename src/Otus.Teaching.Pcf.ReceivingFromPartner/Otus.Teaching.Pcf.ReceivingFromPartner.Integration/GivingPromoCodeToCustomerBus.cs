using System.Threading.Tasks;
using MassTransit;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Dto;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration;

public class GivingPromoCodeToCustomerBus : IGivingPromoCodeToCustomerGateway
{
    public async Task GivePromoCodeToCustomer(PromoCode promoCode)
    {
        var busControl = Bus.Factory.CreateUsingRabbitMq(Configure);

        await busControl.StartAsync();

        try
        {
            await busControl.Publish(new GivePromoCodeToCustomerDto
            {
                PartnerId = promoCode.Partner.Id,
                BeginDate = promoCode.BeginDate.ToShortDateString(),
                EndDate = promoCode.EndDate.ToShortDateString(),
                PreferenceId = promoCode.PreferenceId,
                PromoCode = promoCode.Code,
                ServiceInfo = promoCode.ServiceInfo,
                PartnerManagerId = promoCode.PartnerManagerId
            });
        }
        finally
        {
            await busControl.StopAsync();
        }
    }
    
    private static void Configure(IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Host("localhost",
            h =>
            {
                h.Username("rmuser");
                h.Password("rmpassword");
            });
    }
}