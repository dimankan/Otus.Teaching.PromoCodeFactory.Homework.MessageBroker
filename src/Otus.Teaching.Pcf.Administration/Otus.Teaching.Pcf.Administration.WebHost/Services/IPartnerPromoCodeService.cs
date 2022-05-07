using Otus.Teaching.Pcf.Common;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.Services
{
    public interface IPartnerPromoCodeService
    {
        public Task UpdateAppliedPromocodesAsync(PartnerManagerIdMessage message);
    }
}
