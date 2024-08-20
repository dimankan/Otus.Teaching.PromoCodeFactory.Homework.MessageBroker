using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services
{
    public interface IPromoCodesService
    {
        Task<IEnumerable<PromoCode>> GetAllAsync();

        Task GivePromoCodesToCustomersWithPreferenceAsync(PromoCode promoCode);
    }
}
