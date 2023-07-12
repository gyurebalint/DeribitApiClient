using DeribitApiClient.WebSocketService;
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

            IHost host = builder.Build();
            host.Run();
        }
    }
}