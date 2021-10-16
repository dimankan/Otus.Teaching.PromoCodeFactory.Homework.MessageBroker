using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.Integration.Messages;
using Otus.Teaching.Pcf.Administration.Integration.Messages.MessageServices;
using Otus.Teaching.Pcf.Administration.Integration.RabbitMQ.Abstractions;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.ModelOperationsResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Integration.EntityServices.EmployeeServices
{
    public class EmployeeService : IEmployeeService, IRabbitMqMsgService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task ProcessRabbitMQMessage(IRabbitMqMessage message)
        {
            PromoCodeNotificationMessage promoCodeNotificationMessage = message as PromoCodeNotificationMessage;
            await UpdateAppliedPromocodesAsync(promoCodeNotificationMessage.PartnerManagerId.Value);
        }

        public async Task<EmplServiceOperationResult> UpdateAppliedPromocodesAsync(Guid id)
        {
            EmplServiceOperationResult emplServiceOperationResult = new EmplServiceOperationResult();

            var employee = await _employeeRepository.GetByIdAsync(id);

            emplServiceOperationResult.IsEmplFound = employee != null;

            if (!emplServiceOperationResult.IsEmplFound)
            {
                return emplServiceOperationResult;
            }

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);

            emplServiceOperationResult.Ok = true;

            return emplServiceOperationResult;
        }
    }
}
