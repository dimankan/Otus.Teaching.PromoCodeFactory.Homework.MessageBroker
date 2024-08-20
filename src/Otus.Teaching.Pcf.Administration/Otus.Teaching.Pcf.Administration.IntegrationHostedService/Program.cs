using Microsoft.EntityFrameworkCore;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Extensions;
using Otus.Teaching.Pcf.Administration.DataAccess;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;

namespace Otus.Teaching.Pcf.Administration.IntegrationHostedService
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
                x.UseNpgsql(builder.Configuration.GetConnectionString("PromocodeFactoryAdministrationDb"));
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