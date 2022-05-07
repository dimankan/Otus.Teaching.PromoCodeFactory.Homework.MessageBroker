using Otus.Teaching.Pcf.Common;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Services
{
    public interface IPromoCodeService
    {
        public Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeToCustomerMessage message);
    }
}
