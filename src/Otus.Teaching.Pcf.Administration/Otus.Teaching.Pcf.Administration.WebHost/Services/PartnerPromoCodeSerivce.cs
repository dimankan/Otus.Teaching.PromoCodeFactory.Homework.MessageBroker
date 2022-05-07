using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Common;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.Services
{
    public class PartnerPromoCodeSerivce : IPartnerPromoCodeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public PartnerPromoCodeSerivce(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task UpdateAppliedPromocodesAsync(PartnerManagerIdMessage message)
        {
            var employee = await _employeeRepository.GetByIdAsync(message.PartnerManagerId.Value);

            if (employee == null)
                throw new System.Exception("Менеджер не найден");

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
