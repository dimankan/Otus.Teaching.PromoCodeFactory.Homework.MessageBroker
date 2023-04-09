using System.Threading.Tasks;
using MassTransit;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Common.Dto;

namespace Otus.Teaching.Pcf.Administration.WebHost.Consumers;

public class GivePromoCodeToCustomerConsumer: IConsumer<GivePromoCodeToCustomerDto>
{
    private readonly IRepository<Employee> _employeeRepository;
    
    public GivePromoCodeToCustomerConsumer(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task Consume(ConsumeContext<GivePromoCodeToCustomerDto> context)
    {
        var employee = await _employeeRepository.GetByIdAsync(context.Message.PartnerManagerId);

        if (employee != null)
        {
            employee.AppliedPromocodesCount++;
            await _employeeRepository.UpdateAsync(employee);
        }
    }
}