
using Consumer.Consumers;
using MassTransit;

namespace Consumer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageQueues(this IServiceCollection services, IConfiguration configuration)
        {
            var messageQueueOptions = new MessageQueueOptions();
            configuration.GetSection(MessageQueueOptions.Section).Bind(messageQueueOptions);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageType1Consumer>(/*typeof(eConsumerDefinition)*/);
                x.AddConsumer<MessageType2Consumer>();

                if (messageQueueOptions.UseAzureBus)
                {
                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                        cfg.Host(messageQueueOptions.ConnectionString);
                    });
                }
                else
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                }
            });

            // OPTIONAL, but can be used to configure the bus options
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });

            return services;
        }
    }
}
