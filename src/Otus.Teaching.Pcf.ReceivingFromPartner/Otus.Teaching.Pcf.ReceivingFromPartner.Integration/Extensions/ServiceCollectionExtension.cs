using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Amqp;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Http;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Settings;
using System;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            var integrationType = configuration.GetSection("IntegrationType").Value.ToUpper();

            return integrationType switch
            {
                "AMQP" => services.AddAmqpIntegration(configuration),
                "HTTP" => services.AddHttpIntegration(configuration),
                _ => throw new InvalidOperationException("В файле конфигурации указан неверный тип интеграции"),
            };
        }

        private static IServiceCollection AddAmqpIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("AmqpSettings").Get<AmqpSettings>();

            services.AddSingleton(settings);

            services.AddScoped(typeof(RabbitProducer));

            services.AddScoped<IPromoCodeSenderGateway, AmqpPromoCodeSenderGateway>();

            return services;
        }

        private static IServiceCollection AddHttpIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("HttpSettings").Get<HttpSettings>();

            services.AddHttpClient<IGivingPromoCodeToCustomerGateway, GivingPromoCodeToCustomerGateway>(c =>
            {
                c.BaseAddress = new Uri(settings.GivingToCustomerApiUrl);
            });

            services.AddHttpClient<IAdministrationGateway, AdministrationGateway>(c =>
            {
                c.BaseAddress = new Uri(settings.AdministrationApiUrl);
            });

            services.AddScoped<IPromoCodeSenderGateway, HttpPromoCodeSenderGateway>();

            return services;
        }
    }
}
