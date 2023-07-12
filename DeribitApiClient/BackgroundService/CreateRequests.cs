using DeribitApiClient.Models;
using System.Text.Json;

namespace DeribitApiClient.BackgroundService
{
    public static class CreateRequests
    {
        public static string AuthenticationRequest(DeribitConfigOptions options)
        {
            var authMessage = new WebsocketRequest()
            {
                id = 9929,
                jsonrpc = "2.0",
                method = "public/auth",
                _params = new Params()
                {
                    client_id = options.ClientId,
                    client_secret = options.ClientSecret,
                    grant_type = "client_credentials"
                }
            };

            var resultMessage = JsonSerializer.Serialize(authMessage);

            return resultMessage;
        }

        public static string SubscribeRequest(DeribitConfigOptions options)
        {
            var subscribeMessage = new WebsocketRequest()
            {
                id = 9929,
                jsonrpc = "2.0",
                method = "private/subscribe",
                _params = new Params()
                {
                    channels = options.SubscribeTo,
                }
            };

            var resultMessage = JsonSerializer.Serialize(subscribeMessage);

            return resultMessage;
        }
    }
}
