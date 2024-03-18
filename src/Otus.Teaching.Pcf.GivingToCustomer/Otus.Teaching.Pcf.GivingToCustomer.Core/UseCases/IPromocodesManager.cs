using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.UseCases
{
    public interface IPromocodesManager
    {
        Task<IEnumerable<PromoCode>> GetPromocodesAsync();

        Task GivePromoCodesToCustomersWithPreferenceAsync(Guid preferenceId, PromoCode promoCode);
    }
}
