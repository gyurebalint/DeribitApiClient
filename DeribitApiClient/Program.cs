using DeribitApiClient.BackgroundService;
using DeribitApiClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeribitApiClient
{
    public class Program
    {

        public void Main(string[] args)
        {
            Console.WriteLine("exited gracefully");
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