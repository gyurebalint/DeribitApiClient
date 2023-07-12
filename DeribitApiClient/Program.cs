using DeribitApiClient.BackgroundService;
using DeribitApiClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeribitApiClient
{
    public class Program
    {
        private static IHost host;

        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl+C to exit gracefully.");
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true; // Prevents the application from immediately terminating
                ShutDownGracefully();
            };

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<DeribitWebsocketHost>();
            builder.Services.AddSingleton<IDeribitWebsocketClient, DeribitWebsocketClient>();
            builder.Services.AddOptions();
            builder.Services.Configure<DeribitConfigOptions>(
                builder.Configuration.GetSection(DeribitConfigOptions.Name));

            host = builder.Build();
            host.Run();
        }

        private static void ShutDownGracefully()
        {
            host.StopAsync().GetAwaiter().GetResult();
            host.Dispose();
            Console.WriteLine("Exited gracefully.");
            Environment.Exit(0);
        }
    }
}