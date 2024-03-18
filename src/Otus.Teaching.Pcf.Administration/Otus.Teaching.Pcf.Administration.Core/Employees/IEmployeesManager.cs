using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Employees
{
    public interface IEmployeesManager
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(Guid id);

        Task UpdateAppliedPromocodesAsync(Guid id);
    }
}
