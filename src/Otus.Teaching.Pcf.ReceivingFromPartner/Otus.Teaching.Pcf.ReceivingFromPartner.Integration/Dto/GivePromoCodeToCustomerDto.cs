using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Messages;
using System;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Dto
{
    public class GivePromoCodeToCustomerDto : IRabbitMqMessage
    {
        public string ServiceInfo { get; set; }

        public Guid PartnerId { get; set; }

        public Guid PromoCodeId { get; set; }
        
        public string PromoCode { get; set; }

        public Guid PreferenceId { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
        
        public Guid? PartnerManagerId { get; set; }

        public GivePromoCodeToCustomerDto(PromoCode promoCode)
        {
            PartnerId = promoCode.Partner.Id;
            BeginDate = promoCode.BeginDate.ToShortDateString();
            EndDate = promoCode.EndDate.ToShortDateString();
            PreferenceId = promoCode.PreferenceId;
            PromoCode = promoCode.Code;
            ServiceInfo = promoCode.ServiceInfo;
            PartnerManagerId = promoCode.PartnerManagerId;
            PromoCodeId = promoCode.Id;
        }

        public GivePromoCodeToCustomerDto(){ }
    }
}