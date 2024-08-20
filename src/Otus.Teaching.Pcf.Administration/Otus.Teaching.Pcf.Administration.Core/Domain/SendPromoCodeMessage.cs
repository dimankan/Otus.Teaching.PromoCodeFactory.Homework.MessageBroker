using System;

namespace Otus.Teaching.Pcf.Administration.Core.Domain
{
    public class SendPromoCodeMessage
    {
        public PromoCode PromoCode { get; set; }
        public Guid? PartnerManagerId { get; set; }
    }

    public class PromoCode
        : BaseEntity
    {
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid PartnerId { get; set; }

        public Guid PreferenceId { get; set; }
    }
}
