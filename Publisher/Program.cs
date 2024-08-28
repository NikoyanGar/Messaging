using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Publisher
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
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

                    services.AddHostedService<MessageType1Publisher>();
                    services.AddHostedService<MessageType2Publisher>();
                });
    }
}
