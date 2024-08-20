using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Services;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<IPreferencesService, PreferencesService>();
            services.AddScoped<IPromoCodesService, PromoCodesService>();

            return services;
        }
    }
}
