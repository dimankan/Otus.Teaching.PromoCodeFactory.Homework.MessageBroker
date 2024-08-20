using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Abstractions.Services
{
    public interface IEmployeesService
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(Guid id);

        Task UpdateAppliedPromocodesAsync(Guid id);
    }
}
