using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Otus.Teaching.Pcf.Administration.WebHost.BackgroundService;
using Otus.Teaching.Pcf.GivingToCustomer.Integration;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<GivePromoCodeToCustomerConsumer>();
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            Configure(cfg);
                            RegisterEndPoints(cfg, context);
                        });
                    });
                    services.AddHostedService<MasstransitService>();
                });

        private static void Configure(IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.Host("localhost", "/",
                h =>
                {
                    h.Username("rmuser");
                    h.Password("rmpassword");
                });
        }

        private static void RegisterEndPoints(
            IRabbitMqBusFactoryConfigurator configurator, 
            IBusRegistrationContext context)
        {
            configurator.ReceiveEndpoint("give_promocode_to_customer_update_count", 
                e =>
                {
                    e.ConfigureConsumer<GivePromoCodeToCustomerConsumer>(context);
                });
        }
    }
}