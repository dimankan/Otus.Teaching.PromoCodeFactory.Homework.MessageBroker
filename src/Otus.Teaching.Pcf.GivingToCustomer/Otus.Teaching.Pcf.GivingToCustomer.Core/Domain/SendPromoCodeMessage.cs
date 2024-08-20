using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Domain
{
    public class SendPromoCodeMessage
    {
        public PromoCode PromoCode { get; set; }
        public Guid? PartnerManagerId { get; set; }
    }
}
