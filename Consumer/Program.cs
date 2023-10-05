// See https://aka.ms/new-console-template for more information

using Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient<ITypedClient, TypedClient>(config =>
                {
                    config.Timeout = TimeSpan.FromSeconds(2);
                });

                services.AddMassTransit(configurator =>
                {
                    configurator.AddConsumer<TestConsumer>();

                    configurator.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("rabbitmq://localhost", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });
            })
            .Build();
        
        await host.RunAsync();
    }
}
