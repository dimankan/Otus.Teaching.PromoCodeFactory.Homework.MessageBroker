using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Extensions;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            
            var settings = builder.Configuration.GetSection("AmqpSettings").Get<AmqpSettings>()!;
            builder.Services.AddSingleton(settings);

            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseNpgsql(builder.Configuration.GetConnectionString("PromocodeFactoryGivingToCustomerDb"));
            });

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            
            builder.Services.AddScoped<IDbInitializer, EfDbInitializer>();

            builder.Services.AddCoreServices();

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}