using Otus.Teaching.Pcf.GivingToCustomer.Integration.Messages;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers
{
    public class GivePromoCodeRequestMapper
    {
        public static GivePromoCodeRequest MapMessageToRequest(GivePromoCodeMessage message)
        {
            return new GivePromoCodeRequest()
            {
                ServiceInfo = message.ServiceInfo,
                PartnerId = message.PartnerId,
                PromoCodeId = message.PromoCodeId,
                PromoCode = message.PromoCode,
                PreferenceId = message.PreferenceId,
                BeginDate = message.BeginDate,
                EndDate = message.EndDate
            };
        }
    }
}
