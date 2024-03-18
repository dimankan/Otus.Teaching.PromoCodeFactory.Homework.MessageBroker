using MassTransit;
using Otus.Teaching.Pcf.Administration.Core.Employees;
using Otus.Teaching.Pcf.Contracts;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.RabbitMqConsumers
{
    public class NotifyAdminAboutPartnerManagerPromoCodeConsumer : IConsumer<NotifyAdminAboutPartnerManagerPromoCodeContract>
    {
        private readonly IEmployeesManager _employeesManager;

        public NotifyAdminAboutPartnerManagerPromoCodeConsumer(IEmployeesManager employeesManager)
        {
            _employeesManager = employeesManager;
        }

        public async Task Consume(ConsumeContext<NotifyAdminAboutPartnerManagerPromoCodeContract> context)
        {
            await _employeesManager.UpdateAppliedPromocodesAsync(context.Message.PartnerManagerId);
        }
    }
}
