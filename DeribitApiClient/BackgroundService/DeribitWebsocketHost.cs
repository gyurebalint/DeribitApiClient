﻿using Microsoft.Extensions.Hosting;

namespace DeribitApiClient.BackgroundService
{
    public class DeribitWebsocketHost : IHostedService
    {
        private readonly IDeribitWebsocketClient _client;

        public DeribitWebsocketHost(IDeribitWebsocketClient client)
        {
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started...");
            await _client.RunStreamAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service shut down...");
            _client.Dispose();
            return Task.CompletedTask;
        }
    }
}
