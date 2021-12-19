using Otus.Teaching.Pcf.GivingToCustomer.WebHost.ModelOperationsResult;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.EntityServices.PromoCodeServices
{
    public interface IPromoCodeService
    {
        public Task<PromoCodeServiceOperationResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request);
    }
}
