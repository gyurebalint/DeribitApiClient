using System.Text.Json.Serialization;

namespace DeribitApiClient.Models
{
    public class Params
    {
        public string grant_type { get; set; } = "";
        public string client_id { get; set; } = "";
        public string client_secret { get; set; } = "";
        public List<string> channels { get; set; } = new List<string>();
    }
    public class WebsocketRequest
    {
        public string jsonrpc { get; set; } = "";
        public int id { get; set; } = 0;
        public string method { get; set; } = "";
        [JsonPropertyName("params")]
        public Params _params { get; set; }= new Params();
    }
}