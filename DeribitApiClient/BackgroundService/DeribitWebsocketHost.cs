

using Microsoft.Extensions.Hosting;

namespace DeribitApiClient.WebSocketService
{
    internal class DeribitWebsocketHost : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service shut down...");

            return Task.CompletedTask;
        }
    }
}
