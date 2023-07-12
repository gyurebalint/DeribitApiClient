using DeribitApiClient.BackgroundService;
using DeribitApiClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeribitApiClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<DeribitWebsocketHost>();
            builder.Services.AddSingleton<IDeribitWebsocketClient, DeribitWebsocketClient>();
            builder.Services.AddOptions();
            builder.Services.Configure<DeribitConfigOptions>(
                builder.Configuration.GetSection(DeribitConfigOptions.Name));



            IHost host = builder.Build();
            host.Run();
        }
    }
}