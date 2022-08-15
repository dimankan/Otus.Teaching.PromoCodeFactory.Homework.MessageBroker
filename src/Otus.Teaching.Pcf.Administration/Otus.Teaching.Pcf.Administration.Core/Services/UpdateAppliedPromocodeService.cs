using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Logic
{
    /// <summary>
    /// Сервис для апдейта промокода
    /// </summary>
    public class UpdateAppliedPromocodeService : IUpdateAppliedPromocodeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public UpdateAppliedPromocodeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> UpdateAppliedPromocodesAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return new NotFoundResult();

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);

            return new OkResult();
        }
    }
}
