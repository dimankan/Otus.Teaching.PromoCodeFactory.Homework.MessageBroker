using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await _employeeRepository.GetAllAsync();

        public async Task<Employee> GetByIdAsync(Guid id)
            => await _employeeRepository.GetByIdAsync(id);

        public async Task UpdateAppliedPromocodesAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id) ?? throw new EmployeeNotFoundException();

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
