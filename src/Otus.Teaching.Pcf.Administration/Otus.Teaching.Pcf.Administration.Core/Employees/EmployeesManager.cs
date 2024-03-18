using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Employees
{
    public class EmployeesManager : IEmployeesManager
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesManager(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id) => await _employeeRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await _employeeRepository.GetAllAsync();

        public async Task UpdateAppliedPromocodesAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                throw new ArgumentException("Пользователь не найден", nameof(id));
            }

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
