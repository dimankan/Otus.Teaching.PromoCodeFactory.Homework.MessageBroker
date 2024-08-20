using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways
{
    public interface IPromoCodeSenderGateway
    {
        Task SendPromoCode(PromoCode promoCode, Guid? partnerManagerId);
    }
}
