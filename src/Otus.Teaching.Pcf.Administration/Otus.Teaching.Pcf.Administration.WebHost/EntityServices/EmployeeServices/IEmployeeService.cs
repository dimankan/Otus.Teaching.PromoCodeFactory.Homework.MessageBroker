using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.ModelOperationsResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Integration.EntityServices.EmployeeServices
{
    public interface IEmployeeService
    {
        public Task<EmplServiceOperationResult> UpdateAppliedPromocodesAsync(Guid id);
    }
}
