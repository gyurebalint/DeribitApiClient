using Microsoft.Extensions.Hosting;

namespace DeribitApiClient.BackgroundService
{
    public class DeribitWebsocketHost : IHostedService
    {
        private readonly IDeribitWebsocketClient _client;

        public DeribitWebsocketHost(IDeribitWebsocketClient client)
        {
            _client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started...");
            //client.RunStream();
            //Clean up resources: client.Dispose()

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            Console.WriteLine("Service shut down...");

            return Task.CompletedTask;
        }
    }
}
