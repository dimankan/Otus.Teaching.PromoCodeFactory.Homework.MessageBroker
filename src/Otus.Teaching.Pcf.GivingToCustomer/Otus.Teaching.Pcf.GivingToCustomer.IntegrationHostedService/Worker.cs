using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AmqpSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, AmqpSettings settings, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _settings = settings;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            StartPromoCodeMessageListening();
        }

        private void StartPromoCodeMessageListening()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var dbContext = scopedServices.GetRequiredService<DataContext>();
                var dbInitalizer = scopedServices.GetRequiredService<IDbInitializer>();

                var promoCodesService = scopedServices.GetRequiredService<IPromoCodesService>();

                var consumer = new RabbitConsumer(_settings);
                var sendPromoCodeHandler = new SendPromoCodeMessageHandler(scopedServices.GetRequiredService<IServiceScopeFactory>());

                dbInitalizer.InitializeDb();
                consumer.FanoutConsume(sendPromoCodeHandler, "giving_to_customer_consumer");
            }

            _logger.LogInformation("Send promocode listening started");
        }
    }
}
