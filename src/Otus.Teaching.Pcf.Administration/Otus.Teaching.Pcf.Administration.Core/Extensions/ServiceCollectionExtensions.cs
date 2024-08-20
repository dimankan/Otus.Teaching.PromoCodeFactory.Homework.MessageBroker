using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;
using Otus.Teaching.Pcf.Administration.Core.Services;

namespace Otus.Teaching.Pcf.Administration.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesService, EmployeesService>();

            return services;
        }
    }
}
