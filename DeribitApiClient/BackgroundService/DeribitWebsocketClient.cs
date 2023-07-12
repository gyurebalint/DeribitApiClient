using DeribitApiClient.Models;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;
using System.Text;

namespace DeribitApiClient.BackgroundService
{
    public class DeribitWebsocketClient : IDeribitWebsocketClient
    {
        private readonly DeribitConfigOptions _options;
        private const string urlPrefix = "wss://";
        private const string urlSufffix = "/ws/api/v2";
        public CancellationTokenSource cts = new CancellationTokenSource();

        public DeribitWebsocketClient(IOptions<DeribitConfigOptions> options)
        {
            _options = options.Value;
        }

        public async Task RunStreamAsync()
        {
            var url = urlPrefix + _options.BaseUrl + urlSufffix;
            var uri = new Uri(url);

            using(var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(uri, cts.Token);
                Console.WriteLine("Connected to Deribit WebSocket");

                var authMessage = CreateRequests.AuthenticationRequest(_options);
                await SendMessageOverWebsocket(ws, authMessage, cts.Token);

                Console.WriteLine("Subscribe to channels");
                await SubscribeToChannels(ws, cts.Token);

                Console.WriteLine("User authenticated");
                await ReceiveMessageOverWebsocket(ws, cts.Token);
            }

        }

        private async Task SubscribeToChannels(ClientWebSocket ws, CancellationToken token)
        {
            var message = CreateRequests.SubscribeRequest(_options);
            await SendMessageOverWebsocket(ws, message, token);
        }

        private async Task ReceiveMessageOverWebsocket(ClientWebSocket ws, CancellationToken token)
        {
            while (ws.State == WebSocketState.Open)
            {
                byte[] buffer = new byte[1024];
                ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer);
                WebSocketReceiveResult result = await ws.ReceiveAsync(buffer, token);
                var messageResult = Encoding.UTF8.GetString(buffer, 0, result.Count);

                Console.WriteLine(messageResult);
            }
        }
        private async Task SendMessageOverWebsocket(ClientWebSocket ws, string message, CancellationToken token)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            ArraySegment<byte> bytesSegment = new ArraySegment<byte>(buffer);
            await ws.SendAsync(bytesSegment, WebSocketMessageType.Text, true, token);
        }

        public void Dispose()
        {
            //This is where I would have disposed of the ClientWebSocket singleton instance, which is not needed now since
            // we are using a using{} statemtn which is a try,catch,finally block where it automatically disposes the instance
        }
    }
}
