
using DeribitApiClient.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace DeribitApiClient.BackgroundService
{
    public interface IDeribitWebsocketClient
    {
        //This being a POC I think I wil just create a websocket that auth, subscribes then receives messages 
        //in a using{} scope so the Dispose method is automatic. We ll see if I have time creating a normal api like:
        //Authenticate(), SendMEssage(), Subscribe(), ReceiveMessages() methods.
        Task RunStreamAsync(CancellationToken token);
    }
    public class DeribitWebsocketClient : IDeribitWebsocketClient
    {
        private readonly DeribitConfigOptions _options;
        private const string urlPrefix = "wss://";
        private const string urlSufffix = "/ws/api/v2";


        public DeribitWebsocketClient(IOptions<DeribitConfigOptions> options)
        {
            _options = options.Value;
        }

        public async Task RunStreamAsync(CancellationToken token)
        {
            var url = urlPrefix + _options.BaseUrl + urlSufffix;
            var uri = new Uri(url);

            using(var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(uri, token);
                Console.WriteLine("Connected to Deribit WebSocket");

                var authMessage = PrepareAuthenticationMessage();
                await SendMessageOverWebsocket(ws, authMessage, token);

                Console.WriteLine("User authenticated");
                await ReceiveMessageOverWebsocket(ws, token);
            }

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

        //Should be in some static file
        private string PrepareAuthenticationMessage()
        {
            var authMessage = new AuthenticationRequest()
            {
                id = 9929,
                jsonrpc = "2.0",
                method = "public/auth",
                _params = new Params()
                {
                    client_id = _options.ClientId,
                    client_secret = _options.ClientSecret,
                    grant_type = "client_credentials"
                }
            };

            var resultMessage = JsonSerializer.Serialize(authMessage);

            return resultMessage;
        }
        async void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("CTRL+C pressed, initiating graceful shutdown...");
            // TODO Stop the API client here
            e.Cancel = true;
        }

    }
}
