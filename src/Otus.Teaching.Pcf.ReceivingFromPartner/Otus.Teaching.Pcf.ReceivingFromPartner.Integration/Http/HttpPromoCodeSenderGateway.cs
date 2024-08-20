using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Http
{
    public class HttpPromoCodeSenderGateway : IPromoCodeSenderGateway
    {
        private readonly IGivingPromoCodeToCustomerGateway _givingPromoCodeToCustomerGateway;
        private readonly IAdministrationGateway _administrationGateway;

        public HttpPromoCodeSenderGateway(
            IGivingPromoCodeToCustomerGateway givingPromoCodeToCustomerGateway,
            IAdministrationGateway administrationGateway)
        {
            _givingPromoCodeToCustomerGateway = givingPromoCodeToCustomerGateway;
            _administrationGateway = administrationGateway;
        }

        public async Task SendPromoCode(PromoCode promoCode, Guid? partnerManagerId)
        {
            await _givingPromoCodeToCustomerGateway.GivePromoCodeToCustomer(promoCode);

            if (partnerManagerId.HasValue)
            {
                await _administrationGateway.NotifyAdminAboutPartnerManagerPromoCode(partnerManagerId.Value);
            }
        }
    }
}
