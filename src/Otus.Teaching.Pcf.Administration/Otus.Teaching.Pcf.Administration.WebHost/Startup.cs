using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.DataAccess;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using MassTransit;
using Otus.Teaching.Pcf.Administration.WebHost.Services;
using Otus.Teaching.Pcf.Administration.WebHost.Consumers;

namespace Otus.Teaching.Pcf.Administration.WebHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddMvcOptions(x=> 
                x.SuppressAsyncSuffixInActionNames = false);
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbInitializer, EfDbInitializer>();
            services.AddDbContext<DataContext>(x =>
            {
                //x.UseSqlite("Filename=PromocodeFactoryAdministrationDb.sqlite");
                x.UseNpgsql(Configuration.GetConnectionString("PromocodeFactoryAdministrationDb"));
                x.UseSnakeCaseNamingConvention();
                x.UseLazyLoadingProxies();
            });

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory Administration API Doc";
                options.Version = "1.0";
            });
            services.AddScoped<IPartnerPromoCodeService, PartnerPromoCodeSerivce>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PartnerManagerPromoCodeConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration.GetValue<string>("RabbitMQ:RabbitMqHost"), "/",
                        h =>
                        {
                            h.Username(Configuration.GetValue<string>("RabbitMQ:RabbitMqHostSettings:UserName"));
                            h.Password(Configuration.GetValue<string>("RabbitMQ:RabbitMqHostSettings:Password"));
                        }
                );
                    cfg.ReceiveEndpoint(Configuration.GetValue<string>("RabbitMQ:Queues:AdministrationGateway"), e =>
                    {
                        e.UseMessageRetry(r =>
                        {
                            r.Incremental(5, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                        });
                        e.ConfigureConsumer<PartnerManagerPromoCodeConsumer>(context);
                    });

                });
                
            });            
            services.AddOptions<MassTransitHostOptions>().Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocExpansion = "list";
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            dbInitializer.InitializeDb();
        }
    }
}